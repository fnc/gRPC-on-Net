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