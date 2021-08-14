using System;
using System.IO;

namespace gRPC.Server
{
	class Program
	{
		const int Port = 50008;
		static void Main(string[] args)
		{
			Grpc.Core.Server server = null;
			try
			{
				server = new Grpc.Core.Server()
				{
					Ports = { new Grpc.Core.ServerPort("localhost", Port,Grpc.Core.ServerCredentials.Insecure)
					}
				};
				server.Start();
				Console.WriteLine($"Server started on port {Port}!");
				Console.ReadKey();
			}
			catch (IOException ex)
			{
				Console.WriteLine($"Server error:{ex.Message}");

			}
			finally {
				if(server != null) server.ShutdownAsync().Wait();
				Console.WriteLine("Server stopped!");
			}
		}
	}
}
