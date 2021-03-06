﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace DataStreamClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/stream").Build();
            await connection.StartAsync(tokenSource.Token);

            var channel = await connection.StreamAsChannelAsync<string>("Jokes", 3000, tokenSource.Token);

            while (await channel.WaitToReadAsync(tokenSource.Token))
            {
                while (channel.TryRead(out var joke))
                {
                    Console.WriteLine(joke);
                    Console.WriteLine();
                }
            }
        }
    }
}
