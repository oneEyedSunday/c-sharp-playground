using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelPlayGround
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Channel<string> aChannel = Channel.CreateUnbounded<string>();

            Action ReadFromChannelAction = async () =>
            {
                while (await aChannel.Reader.WaitToReadAsync())
                    Console.WriteLine(await aChannel.Reader.ReadAsync());
            };

            Action WriteRandomStuffToChannelAction = async () =>
            {
                var rnd = new Random();
                for (short i = 0; i < 5; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(rnd.Next(5)));
                    await aChannel.Writer.WriteAsync($"Some message from the Law {i}");
                }

                aChannel.Writer.Complete();
            };

            var consumer = Task.Run(ReadFromChannelAction);

            var producer = Task.Run(WriteRandomStuffToChannelAction);

            await Task.WhenAll(producer, consumer);

            var jerseyProcessing = Generator<string>.GenerateReaderFrom(new string[] {
                "Nelson Semedo", "Miralem Pjanic"
            });
            await foreach (string item in jerseyProcessing.ReadAllAsync())
                Console.WriteLine("Received Jersey Order For {0}", item);
        }
    }
}
