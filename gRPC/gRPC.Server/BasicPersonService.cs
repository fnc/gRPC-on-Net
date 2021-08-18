using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comm;
using Grpc.Core;
using static Comm.PersonService;

namespace gRPC.Server
{
	public class BasicPersonService : PersonServiceBase
	{
		public override Task<PersonResponse> AddPerson(PersonRequest request, ServerCallContext context)
		{
			string message = $"{request.Person.Name} {request.Person.LastName}({request.Person.Email}) was added successfully.";

			var response = new PersonResponse() { Result = message };

			return Task.FromResult(response);
		}

        public override async Task AddPersonClones(PersonRequest request, IServerStreamWriter<PersonResponse> responseStream, ServerCallContext context)
        {
			Console.WriteLine($"Add person clones request received: {request.ToString()}");

			for (int i = 1; i <= 10; i++)
			{
				var message = $"{request.Person.Name} {request.Person.LastName}({request.Person.Email}) was added successfully {i} time(s).";

				var response = new PersonResponse() { Result = message };

				await responseStream.WriteAsync(response);
			}
        }

        public override async Task<PeopleResponse> AddPeople(IAsyncStreamReader<PersonRequest> requestStream, ServerCallContext context)
        {
			var counter = 0;
			Console.WriteLine($"Add people requested:");
			while (await requestStream.MoveNext())
            {
				var person = requestStream.Current.Person;

				Console.WriteLine($"{person.Name} {person.LastName}({person.Email}) was received.");

				counter++;
            }
            return new PeopleResponse() { Result = $"{counter} people were added" };
        }

        public override async Task AddPeopleClones(IAsyncStreamReader<PersonRequest> requestStream, IServerStreamWriter<PeopleResponse> responseStream, ServerCallContext context)
        {
			Console.WriteLine($"Add people  clones requested:");
			while (await requestStream.MoveNext())
			{
				var person = requestStream.Current.Person;

				Console.WriteLine($"{person.Name} {person.LastName}({person.Email}) was received.");

				for (int i = 1; i <= 10; i++)
				{
					var message = $"{person.Name} {person.LastName}({person.Email}) was added successfully {i} time(s).";

					var response = new PeopleResponse() { Result = message };

					await responseStream.WriteAsync(response);
				}
			}
		}
    }
}
