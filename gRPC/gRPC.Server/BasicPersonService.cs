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
	}
}
