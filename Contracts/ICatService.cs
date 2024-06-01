using System.ServiceModel;

namespace Uni.SoapService.Contracts
{
    [ServiceContract]
    public interface ICatService
    {
        [OperationContract]
Task<string> SayMeow();

        [OperationContract]
        Task<string> GetCatNameByColor(string color);
    }
}
