using Grpc.Core;
using Grpc.Net.Client;
using Grpc_Demo2;
using System;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        static async Task Main()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(
                new HelloRequest { Name = "World" });
            Console.WriteLine(response);


            using var streamingCall = client.SayHelloStream(
              new HelloRequest { Name = "World" });

            await foreach (var reply in streamingCall.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(reply.Message);
            }

        }
    }
}
