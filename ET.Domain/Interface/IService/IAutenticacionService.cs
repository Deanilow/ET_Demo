namespace ET.Domain.Interface.IService
{
    public interface IAutenticacionService
    {
        Task<Response<AuthenticationResult>> FindUsuario(string Email, string Password);
    }
}
