namespace ET.DataAccess.Repository
{
    public class AutenticacionRepository : Base, IAutenticacionRepository
    {
        public AutenticacionRepository(IConfiguration configuration) : base(configuration) { }
        public async Task<Usuario> FindUsuarioByEmail(string Email)
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Usuario>(
                    "[dbo].[FindUsuarioByEmail]",
                    new
                    {
                        Email
                    },
                    commandType: CommandType.StoredProcedure, commandTimeout: 200
                );
                return result.FirstOrDefault();
            }
        }
    }
}