﻿using System;
using System.Threading.Channels;
using System.Threading.Tasks;

using ChannelPlayGround.Models;

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

            //var consumer = Task.Run(ReadFromChannelAction);

            //var producer = Task.Run(WriteRandomStuffToChannelAction);

            //await Task.WhenAll(producer, consumer);

            var jerseyProcessing = Generator<string>.GenerateReaderFrom(new string[] {
                "Nelson Semedo", "Miralem Pjanic"
            });

            var eSportsProcessor = Generator<ESportTopic>.GenerateReaderFrom(new ESportTopic[]
            {
                new ESportTopic { Target = "CoD", Theme = "GamePlay Demo" },
                new ESportTopic { Penetration = 390, Target = "James Bond: 007" },
                new ESportTopic { Penetration = 4, Target = "Shadow of Mordor" },
                new ESportTopic { Target = "FarCry 25" },
                new ESportTopic { Penetration = 4075, Target = "Borderlands 5", }
            });

            var processor2 = Generator<ESportTopic>.GenerateReaderFrom(new ESportTopic[] {
                new ESportTopic { Penetration = 755, Target = "Wanted" },
                new ESportTopic { Target = "Football Manager 2020", Theme = "Unveiling" },
                new ESportTopic { Penetration = 94, Target = "Castlevania", Theme = "[Unknown]" }
            });

            var consolidatedChannel = Multiplexer.Merge(new[] { processor2, eSportsProcessor });


            await foreach (var item in consolidatedChannel.ReadAllAsync())
            {
                Console.WriteLine("[🎮 + ✍️ ] New trend alert {0}", item.Target);
            }

        }
    }
}
