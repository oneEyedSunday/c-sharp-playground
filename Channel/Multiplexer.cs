using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelPlayGround
{
    public static class Multiplexer
    {
        public static ChannelReader<T> Merge<T>(ChannelReader<T> first, ChannelReader<T> second)
        {
            var output = Channel.CreateUnbounded<T>();


            Task.Run(async () => {
                await foreach (var item in first.ReadAllAsync())
                    await output.Writer.WriteAsync(item);
            });


            Task.Run(async () =>
            {
                await foreach (var item in second.ReadAllAsync())
                    await output.Writer.WriteAsync(item);
            });

            return output;
        }
    }
}
