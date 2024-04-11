namespace ET.DataAccess.Conexion;
public class Sql : Base
{
    public Sql(IConfiguration _configuration) : base(_configuration)
    {
        _dapperConfiguration = _configuration;
    }

    public static string ObtenerConexion()
    {
        string connectionStringName = "ConnectionStrings:DefaultConnection";

        var connectionString = _dapperConfiguration[connectionStringName];

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException("No Existe Conexion");

        return connectionString;
    }
}