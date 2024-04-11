namespace ET.Domain.Interface.ICore;
public interface IJwtTokenGenerator
{
    string GenerateToken(Usuario user);
}
