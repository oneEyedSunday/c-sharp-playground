using System;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NativeChannels
{
    public static class Multiplexer
    {
        public static ChannelReader<T> Merge<T>(params ChannelReader<T>[] inputs)
        {
            var output = Channel.CreateUnbounded<T>();


            Task.Run(async () => {
              async Task Redirect(ChannelReader<T> input)
                {
                    await foreach (var item in input.ReadAllAsync())
                        await output.Writer.WriteAsync(item);
                }

                await Task.WhenAll(inputs.Select(i => Redirect(i)).ToArray());
                output.Writer.Complete();
            });

            return output;
        }
    }
}
