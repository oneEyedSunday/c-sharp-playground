﻿using System;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Threading;

namespace NativeChannels
{
    public class Generator<T>
    {
        public static ChannelReader<T> GenerateReaderFrom(T[] messages, CancellationToken cToken = default)
        {
            Channel<T> aChannel = Channel.CreateBounded<T>(messages.Length);
            var rnd = new Random();

            Task.Run(async () =>
            {
                for (short i = 0; i < messages.Length; i++)
                {
                    if (cToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Stopping Comms....");
                        break;
                    }
                    await aChannel.Writer.WriteAsync(messages[i]);
                    await Task.Delay(TimeSpan.FromSeconds(rnd.Next(3)));
                }
                aChannel.Writer.Complete();
            });


            return aChannel.Reader;
        }
    }
}
