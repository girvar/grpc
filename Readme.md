# Introduction 
This is a sample solution demonstrating how to use gRPC in a .NET Core application with the contracts exposed in a separate NuGet package.

## Build & Run
### Grpc.Contract
cd [Grpc.Contracts](Grpc.Contracts) & run `dotnet build`. This will copy the nuget package to local [packages](packages) folder.

### Grpc.Api
cd [Grpc.Api](Grpc.Api) & run `dotnet run` to start the gRPC server.

### Grpc.Client
cd [Grpc.Client](Grpc.Client) & run `dotnet run` to interact with the gRPC server.

## Solution Structure & settings
The solution consists of the following projects:

### Grpc.Contract.csproj
This project contains the contract definitions for the gRPC service. The contract file `pincode.proto` is located in the `Protos\grpc\contracts` folder.

#### Note Grpc.Contract.csproj Properties
<ItemGroup>
  <Protobuf Include="Protos\grpc\contracts\pincode.proto"/>
  <Content Include="Protos\grpc\contracts\pincode.proto" Pack="true" Visible="true" />
</ItemGroup>

### Grpc.Api
This project implements the gRPC server. It references the `Grpc.Contracts` NuGet package to access the contract definitions. The gRPC service definition file `pincodeservice.proto` is located in the `Protos` folder.

#### Note Grpc.Api.csproj Properties
<Protobuf Include="Protos\pincodeservice.proto" GrpcServices="Server" AdditionalImportDirs="$(PkgGrpc_Contracts)\content\Protos" />
<PackageReference Include="Grpc.Contracts" Version="1.0.0" GeneratePathProperty="true" />

#### Note pincodeservice.proto Properties
Build Action: Protobuf compiler
gRPC Stub Classess: Server only

### Grpc.Client
This project implements the gRPC client. It also references the `Grpc.Contracts` NuGet package to access the contract definitions. The gRPC service definition file `pincodeservice.proto` is located in the `Protos` folder.

#### Note Grpc.Client.csproj Properties
<Protobuf Include="Protos\pincodeservice.proto" GrpcServices="Client" AdditionalImportDirs="$(PkgGrpc_Contracts)\content\Protos" />
<PackageReference Include="Grpc.Contracts" Version="1.0.0" GeneratePathProperty="true" />

#### Note pincodeservice.proto Properties
Build Action: Protobuf compiler
gRPC Stub Classess: Client only