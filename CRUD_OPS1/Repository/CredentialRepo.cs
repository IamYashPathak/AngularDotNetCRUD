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
            string query = "select * from Credentials where email = @email and password = @password";
            var parameters = new DynamicParameters();
            parameters.Add("email",cred.email,DbType.String);
            parameters.Add("password", cred.password, DbType.String);

            using (var connection = this._dbContext.CreateConnection()) {
                var creds = await connection.QueryFirstOrDefaultAsync<Credentials>(query,parameters);

                return creds;
            }
        }

        public async Task<string> DeleteById(string email)
        {
            string query = "delete from Credentials where email = @email";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("email",email,DbType.String);
            using (var conn = this._dbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { email });
                response = "Employee Deleted successfully";
            }

            return response;
        }

        public void InsertNewCredentials(string email, string password) {
            string query = "insert into Credentials(email,password) values (@email, @password)";

            var parameters = new DynamicParameters();
            parameters.Add("email", email,DbType.String);
            parameters.Add("password", password, DbType.String);

            using (var conn = this._dbContext.CreateConnection()) {
                conn.Execute(query,parameters);
            }
        }
    }
}
