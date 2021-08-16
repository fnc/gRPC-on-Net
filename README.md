# grpcNet
 A project for learning gRPC.
 This is a gRPC implementation on .net 5.

 Dependencies:
 Google.Protobuf : protobu runtime
 GRPC.Tools: Proto compiler
 GRPC.Core: .net implementation of gRPC.


 Proto file:
     This file is used to define the messages( similar to models or DTOs) used for communication and the services available for this messages( the services could be thought as procedures).
     The service definition is the interface only, the implementation is not done here.
     In .net case this file should be compiled to c# code. 

 Unary communication:

 1 request, 1 response.

 Server streaming:

 1 or no request, n responses

 Responses are streams

 The request is optional, the server could send push notifications without needing a request.
 
 uses: Dividing a big object into smaller ones like fragmentation.

 Client Streaming:

 n Requests, 1 or no Response.

 uses: instead of sending a request object with a list of objects the client can send multiple objects.