namespace ET.DataAccess.Repository
{
    public class ComplejoDeportivoRepository : Base, IComplejoDeportivoRepository
    {
        private readonly int _commandTimeout = 8000000;
        public ComplejoDeportivoRepository(IConfiguration configuration) : base(configuration) { }

        public async Task Delete(Guid Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                await dbConnection.QueryAsync<ComplejoDeportivo>(
                    "[dbo].[DeleteComplejoDeportivo]",
                    new
                    {
                        Id
                    },
                    commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout
                );
            }
        }

        public async Task<ComplejoDeportivo> FindById(Guid Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<ComplejoDeportivo, SedeOlimpica, ComplejoDeportivo>(
                    "[dbo].[FindComplejoDeportivo]",
                    (complejo, sedeOlimpica) =>
                    {
                        complejo.SedeOlimpica = sedeOlimpica;
                        return complejo;
                    },
                    new
                    {
                        Id,
                    },
                     splitOn: "Id",
                    commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout
                );

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<ComplejoDeportivo>> GetAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(Sql.ObtenerConexion()))
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<ComplejoDeportivo, SedeOlimpica, ComplejoDeportivo>(
                    "[dbo].[GetComplejoDeportivo]",
                    (complejo, sedeOlimpica) =>
                    {
                        complejo.SedeOlimpica = sedeOlimpica;
                        return complejo;
                    },
                    new
                    {
                        
                    },
                     splitOn: "Id",
                    commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout
                );

                return result.ToList();
            }
        }

        public async Task<ComplejoDeportivo> Insert(ComplejoDeportivo ComplejoDeportivo)
        {
            using (SqlConnection connection = new SqlConnection(Sql.ObtenerConexion()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Nombre", ComplejoDeportivo.Nombre);
                parameters.Add("@IdSedeOlimpica", ComplejoDeportivo.SedeOlimpica.Id);
                parameters.Add("@Localizacion", ComplejoDeportivo.Localizacion);
                parameters.Add("@JefeOrganizacion", ComplejoDeportivo.JefeOrganizacion);
                parameters.Add("@usuarioCreador", ComplejoDeportivo.UsuarioCreador);
                parameters.Add("@FechaCreacion", ComplejoDeportivo.FechaCreacion);
                parameters.Add("@Eliminado", ComplejoDeportivo.Eliminado);

                parameters.Add("@idComplejoDeportivoReturn", dbType: DbType.Guid, direction: ParameterDirection.Output, size: 5215585);
                parameters.Add("@codErrorReturn", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                parameters.Add("@desErrorReturn", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                string procedure = "[dbo].[InsertComplejoDeportivo]";

                await connection.OpenAsync();
                var resultExecuteAsync = await connection.ExecuteAsync(
                                                 sql: procedure,
                                                 commandTimeout: _commandTimeout,
                                                 param: parameters,
                                                 commandType: CommandType.StoredProcedure);

                Guid idComplejoDeportivoReturn = parameters.Get<Guid>("@idComplejoDeportivoReturn");
                int codErrorReturn = parameters.Get<int>("@codErrorReturn");
                string desErrorReturn = parameters.Get<string>("@desErrorReturn");

                ComplejoDeportivo.Id = idComplejoDeportivoReturn;

                if (codErrorReturn != 0) throw new Exception(desErrorReturn);

                return ComplejoDeportivo;
            }
        }

        public async Task Update(ComplejoDeportivo ComplejoDeportivo)
        {
            using (SqlConnection connection = new SqlConnection(Sql.ObtenerConexion()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", ComplejoDeportivo.Id);
                parameters.Add("@Nombre", ComplejoDeportivo.Nombre);
                parameters.Add("@IdSedeOlimpica", ComplejoDeportivo.SedeOlimpica.Id);
                parameters.Add("@Localizacion", ComplejoDeportivo.Localizacion);
                parameters.Add("@JefeOrganizacion", ComplejoDeportivo.JefeOrganizacion);
                parameters.Add("@UsuarioModificador", ComplejoDeportivo.UsuarioModificador);
                parameters.Add("@FechaModificacion", ComplejoDeportivo.FechaModificacion);

                parameters.Add("@codErrorReturn", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                parameters.Add("@desErrorReturn", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                string procedure = "[dbo].[UpdateComplejoDeportivo]";

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
