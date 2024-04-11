namespace ET.DataAccess.Repository
{
    public class SedeOlimpicaRepository : Base, ISedeOlimpicaRepository
    {
        private readonly int _commandTimeout = 8000000;
        public SedeOlimpicaRepository(IConfiguration configuration) : base(configuration) { }

        public async Task Delete(Guid Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                await dbConnection.QueryAsync<SedeOlimpica>(
                    "[dbo].[DeleteSedeOlimpica]",
                    new
                    {
                        Id
                    },
                    commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout
                );
            }
        }

        public async Task<SedeOlimpica> FindById(Guid Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<SedeOlimpica>(
                    "[dbo].[FindSedeOlimpica]",
                    new
                    {
                        Id
                    },
                    commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout
                );
                return result.First();
            }
        }

        public async Task<IEnumerable<SedeOlimpica>> GetAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<SedeOlimpica>(
                    "[dbo].[GetSedeOlimpica]",
                    new
                    {

                    },
                    commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout
                );

                return result.ToList();
            }
        }

        public async Task<SedeOlimpica> Insert(SedeOlimpica SedeOlimpica)
        {
            using (SqlConnection connection = new SqlConnection(Sql.ObtenerConexion()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Nombre", SedeOlimpica.Nombre);
                parameters.Add("@Presupuesto", SedeOlimpica.Presupuesto);
                parameters.Add("@usuarioCreador", SedeOlimpica.UsuarioCreador);
                parameters.Add("@FechaCreacion", SedeOlimpica.FechaCreacion);
                parameters.Add("@Eliminado", SedeOlimpica.Eliminado);

                parameters.Add("@idSedeOlimpicaReturn", dbType: DbType.Guid, direction: ParameterDirection.Output, size: 5215585);
                parameters.Add("@codErrorReturn", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                parameters.Add("@desErrorReturn", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                string procedure = "[dbo].[InsertSedeOlimpica]";

                await connection.OpenAsync();
                var resultExecuteAsync = await connection.ExecuteAsync(
                                                 sql: procedure,
                                                 commandTimeout: _commandTimeout,
                                                 param: parameters,
                                                 commandType: CommandType.StoredProcedure);

                Guid idSedeOlimpicaReturn = parameters.Get<Guid>("@idSedeOlimpicaReturn");
                int codErrorReturn = parameters.Get<int>("@codErrorReturn");
                string desErrorReturn = parameters.Get<string>("@desErrorReturn");

                SedeOlimpica.Id = idSedeOlimpicaReturn;

                if (codErrorReturn != 0) throw new Exception(desErrorReturn);

                return SedeOlimpica;
            }
        }

        public async Task Update(SedeOlimpica SedeOlimpica)
        {
            using (SqlConnection connection = new SqlConnection(Sql.ObtenerConexion()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", SedeOlimpica.Id);
                parameters.Add("@Nombre", SedeOlimpica.Nombre);
                parameters.Add("@Presupuesto", SedeOlimpica.Presupuesto);
                parameters.Add("@UsuarioModificador", SedeOlimpica.UsuarioModificador);
                parameters.Add("@FechaModificacion", SedeOlimpica.FechaModificacion);

                parameters.Add("@codErrorReturn", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                parameters.Add("@desErrorReturn", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                string procedure = "[dbo].[UpdateSedeOlimpica]";

                await connection.OpenAsync();
                var resultExecuteAsync = await connection.ExecuteAsync(
                                                 sql: procedure,
                                                 commandTimeout: _commandTimeout,
                                                 param: parameters,
                                                 commandType: CommandType.StoredProcedure);

                int codErrorReturn = parameters.Get<int>("@codErrorReturn");
                string desErrorReturn = parameters.Get<string>("@desErrorReturn");

                if (codErrorReturn != 0) throw new Exception(desErrorReturn);
            }
        }
    }
}
