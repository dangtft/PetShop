namespace PetShopMVC.Interfaces
{
    public interface IApiService
    {
        Task<string> GetTokenAsync(string email, string password);
    }
}
