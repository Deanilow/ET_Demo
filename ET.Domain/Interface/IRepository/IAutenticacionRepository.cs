namespace ET.Domain.Interface.IRepository;
public interface IAutenticacionRepository
{
    Task<Usuario> FindUsuarioByEmail(string Email);
}
