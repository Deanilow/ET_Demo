using ET.Domain.Interface.IRepository;
using ET.Domain.Interface.IService;

namespace ET.Domain.Services
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly IAutenticacionRepository _IAutenticacionRepository;
        private readonly IPasswordService _PasswordService;

        public AutenticacionService(IAutenticacionRepository AutenticacionRepository, IPasswordService passwordService)
        {
            _IAutenticacionRepository = AutenticacionRepository;
            _PasswordService = passwordService;
        }
        public async Task<Response<AuthenticationResult>> FindUsuario(string Email, string Password)
        {
            var usuario = await _IAutenticacionRepository.FindUsuarioByEmail(Email);

            if (usuario is null) return new Response<AuthenticationResult>("Correo No Existe");

            var isBase64String = this._PasswordService.IsBase64String(Password);

            if (!isBase64String) return new Response<AuthenticationResult>("Credenciales Incorrecta");

            if (usuario != null)
            {
                var isPasswordValid = this._PasswordService.VerifyPasswordHash(this._PasswordService.Base64Decode(Password), Convert.FromBase64String(usuario.PasswordHash), Convert.FromBase64String(usuario.PasswordSalt));

                if (!isPasswordValid) return new Response<AuthenticationResult>("Credenciales Incorrecta");
            }

            return new Response<AuthenticationResult>(new AuthenticationResult(usuario));
        }
    }
}
