using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SoapClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:5000/CatService");

            var client = new CatServiceClient(binding, endpoint);

            try
            {
                var meow = await client.Meow();
                Console.WriteLine($"Meow result: {meow}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                await client.CloseAsync();
            }
        }
    }
}