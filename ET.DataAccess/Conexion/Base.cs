namespace ET.DataAccess.Conexion;
public class Base
{
    protected static IConfiguration _dapperConfiguration;

    public IConfiguration DapperConfiguration
    {
        get { return _dapperConfiguration; }
    }

    public Base(IConfiguration _configuration)
    {
        _dapperConfiguration = _configuration;
    }
}
