using CRUD_OPS1.Model;
using CRUD_OPS1.Model.Data;
using Dapper;
using System.Data;

namespace CRUD_OPS1.Repository
{
    public class CredentialRepo : ICredentialRepo
    {
        private readonly DapperDBContext _dbContext;
        public CredentialRepo(DapperDBContext context)
        {
               this._dbContext = context;
        }
        public async Task<Credentials> ValidateCredentials(Credentials cred)
        {
            string query = "select * from Credentials where userId = @id and password = @password";
            var parameters = new DynamicParameters();
            parameters.Add("id",cred.userId,DbType.Int32);
            parameters.Add("password", cred.password, DbType.String);

            using (var connection = this._dbContext.CreateConnection()) {
                var creds = await connection.QueryFirstOrDefaultAsync<Credentials>(query,parameters);

                return creds;
            }
        }

        public async Task<string> DeleteById(int id)
        {
            string query = "delete from Credentials where userId = @id";
            string response = string.Empty;

            using (var conn = this._dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { id });
                response = "Employee Deleted successfully";
            }

            return response;
        }
    }
}
