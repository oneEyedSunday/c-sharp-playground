using System;
using System.Linq;
using System.Threading.Channels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NativeChannels
{
    public static class DeMultiplexer
    {
        public static IList<ChannelReader<T>> Split<T>(ChannelReader<T> aChannel, short chunks)
        {
            var outputChannels = new Channel<T>[chunks];

            for (short i = 0; i < chunks; i++)
                outputChannels[i] = Channel.CreateUnbounded<T>();

            async void WriteAndComplete()
            {
                short index = 0;

                await foreach (var item in aChannel.ReadAllAsync())
                {
                    await outputChannels[index].Writer.WriteAsync(item);
                    // increment and wrap
                    index = (short)((index + 1) % chunks);
                }

                foreach (var channel in outputChannels)
                    channel.Writer.Complete();
            };

            Task.Run(WriteAndComplete);

            return outputChannels.Select(channel => channel.Reader).ToArray();
        }
    }
}
