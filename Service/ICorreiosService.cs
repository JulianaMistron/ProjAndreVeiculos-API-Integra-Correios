using Service.Responses;

namespace Service
{
    public interface ICorreiosService
    {
        Task<CorreiosResponse> ObterCepAsync(string cep);
    }
}
