using Microsoft.AspNetCore.Cors;
using Uni.SoapService.Contracts;

namespace Uni.SoapService.Service
{
    [EnableCors]
    public class CatService : ICatService
    {
        public Task<string> SayMeow()
        {
            return Task.FromResult("Meow meow");
        }

        public async Task<string> GetCatNameByColor(string color)
        {
            if (color == "white")
            {
                return "Belushka";
            }
            else if (color == "black")
            {
                return "Cvetushka";
            }

            return "The cat name missing";
        }
    }
}
