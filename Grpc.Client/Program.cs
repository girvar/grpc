// See https://aka.ms/new-console-template for more information
using Grpc.Contracts;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using static Grpc.Api.Pincode;
namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            PincodeServiceTest();
            //await GreetServiceTest();
            //await CustomerServiceTest();
            //Console.ReadLine();
        }
        private static void PincodeServiceTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:9001");
            var client = new PincodeClient(channel);

            var input = new PincodeRequest { City = "Pune" };
            Console.WriteLine($"Invoking PincodeService for city {input.City}...");
            var reply = client.GetPincode(input);
            DisplayCity(reply);
        }

        private static void DisplayCity(PincodeResponse response)
        {
            Console.WriteLine($"Result from PincodeService city: {response.City} -> Pincode: {response.Pincode}");
            Console.WriteLine($"Weather Report: " +
                $"Forecast: {response.WeatherResponse.WeatherReport.Forecast}, " +
                $"High: {response.WeatherResponse.WeatherReport.High}, " +
                $"Low: {response.WeatherResponse.WeatherReport.Low} ");
        }



        private static async Task CustomerServiceTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var customerClient = new Customers.CustomersClient(channel);
            var clientRequested = new CustomerLookupModel { UserId = 1 };

            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress} ");
                }
            }
        }

        private static async Task GreetServiceTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new Greeter.GreeterClient(channel);

            var input = new HelloRequest { Name = "GS" };
            var reply = await client.SayHelloAsync(input);
            Console.WriteLine(reply.Message);
        }

    }
}