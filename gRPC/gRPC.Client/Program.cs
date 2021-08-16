using System;
using Comm;
using Grpc.Core;

namespace gRPC.Client
{
	class Program
	{
		const string ServerHost = "localhost";
		const int ServerPort = 50008;

		static void Main(string[] args)
		{
			try
			{
				var channel = new Channel($"{ServerHost}:{ServerPort}", ChannelCredentials.Insecure);
				channel.ConnectAsync().ContinueWith((task) =>
				{
					if (task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion) {
						Console.WriteLine($"Client connected to {ServerHost}:{ServerPort}!");					
					}
				});

				var request = new PersonRequest()
				{
					Person = new Person()
					{
						Name = "Federico",
						LastName = "Croci",
						Email = "name@host.com"
					}
				};

				var client = new PersonService.PersonServiceClient(channel);
				var response = client.AddPerson(request);

				Console.WriteLine($"Result: {response.Result}");

				channel.ShutdownAsync().Wait();
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Client error: {ex.Message}");
			}
		}
	}
}
