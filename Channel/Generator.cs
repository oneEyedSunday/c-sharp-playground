using System;
using System.Threading.Tasks;
using System.Threading.Channels;


namespace ChannelPlayGround
{
    public class Generator<T>
    {
        public static ChannelReader<T> GenerateReaderFrom(T[] messages)
        {
            Channel<T> aChannel = Channel.CreateBounded<T>(messages.Length);
            var rnd = new Random();

            Task.Run(async () =>
            {
                for (short i = 0; i < messages.Length; i++)
                {
                    Console.WriteLine("[+] #{0}, {1}", i + 1, messages[i]);
                    await aChannel.Writer.WriteAsync(messages[i]);
                    await Task.Delay(TimeSpan.FromSeconds(rnd.Next(i * 30)));
                }
                aChannel.Writer.Complete();
            });


            return aChannel.Reader;
        }
    }
}
