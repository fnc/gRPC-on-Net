using System;
using System.Threading.Tasks;
using Comm;
using Grpc.Core;

namespace gRPC.Client
{
    class Program
    {
        const string ServerHost = "localhost";
        const int ServerPort = 50008;

        static async Task Main(string[] args)
        {
            try
            {
                var channel = new Channel($"{ServerHost}:{ServerPort}", ChannelCredentials.Insecure);
                await channel.ConnectAsync().ContinueWith((task) =>
                {
                    if (task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                    {
                        Console.WriteLine($"Client connected to {ServerHost}:{ServerPort}!");
                    }
                });

                //Cretes the client with the provided channel
                var client = new PersonService.PersonServiceClient(channel);

                var request = new PersonRequest()
                {
                    Person = new Person()
                    {
                        Name = "Federico",
                        LastName = "Croci",
                        Email = "name@host.com"
                    }
                };

                //This is an unary communication. 
                var response = client.AddPerson(request);
                Console.WriteLine($"Result: {response.Result}");


                //This is a server streaming communication.
                var personResponse = client.AddPersonClones(request);

                while (await personResponse.ResponseStream.MoveNext())
                {
                    Console.WriteLine($"Result: {personResponse.ResponseStream.Current.Result}");
                }

                //This is a client streaming communication.
                var peopleStream = client.AddPeople();
                for (int i = 1; i <= 5; i++)
                {
                    request = new PersonRequest()
                    {
                        Person = new Person()
                        {
                            Name = $"Sir {i}ington",
                            LastName = "Ramdinton",
                            Email = $"{i}ington.ramdinton@host.com"
                        }
                    };
                    await peopleStream.RequestStream.WriteAsync(request);
                }

                await peopleStream.RequestStream.CompleteAsync();

                var peopleAddResponse = await peopleStream.ResponseAsync;

                Console.WriteLine($"Result: {peopleAddResponse.Result}");

                //This is a Bidirectional streaming communication.
                var peopleClonesStream = client.AddPeopleClones();
                for (int i = 1; i <= 5; i++)
                {
                    request = new PersonRequest()
                    {
                        Person = new Person()
                        {
                            Name = $"Sir {i}ington",
                            LastName = "of Bidiretionalinworth",
                            Email = $"{i}ington.bidir@host.com"
                        }
                    };
                    await peopleClonesStream.RequestStream.WriteAsync(request);
                    Console.WriteLine($"Person {request.Person.Name} {request.Person.LastName} was sent.");
                    for (int j = 0; j < 10; j++)
                    {
                        await peopleClonesStream.ResponseStream.MoveNext();
                        Console.WriteLine($"Result: {peopleClonesStream.ResponseStream.Current.Result}");
                    }
                }

                await peopleClonesStream.RequestStream.CompleteAsync();

                channel.ShutdownAsync().Wait();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
