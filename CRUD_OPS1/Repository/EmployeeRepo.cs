using CRUD_OPS1.Model;
using CRUD_OPS1.Model.Data;
using Dapper;
using System.Data;

namespace CRUD_OPS1.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDBContext context;
        public EmployeeRepo(DapperDBContext context)
        {
               this.context = context;
        }

        public async Task<string> DeleteById(int id)
        {
            string query = "delete from Employee where userId = @id";
            string response = string.Empty;

            using (var conn = this.context.CreateConnection()) {
                await conn.ExecuteAsync(query,new { id });
                response = "Employee Deleted successfully";
            }

            return response;
        }

        public async Task<List<Employee>> GetAll()
        {
            string query = "select * from Employee";
            using (var connection = this.context.CreateConnection())
            {
                var empList = await connection.QueryAsync<Employee>(query);
                return empList.ToList();
            }
        }

        public async Task<Employee> GetById(int id)
        {
            string query = "select * from Employee where userId = @id";
            using (var connection = this.context.CreateConnection()) {
                var emp = await connection.QueryFirstOrDefaultAsync<Employee>(query,new { id });
                return (Employee)emp;
            }
        }

        public async Task<string> Insert(Employee emp)
        {
            string response = string.Empty;
            string query = "insert into Employee(fName,lName,email,phNo,address,pincode,roleId) values(@fName,@lName,@email,@phNo,@address,@pincode,@roleId)";
            var parameters = new DynamicParameters();
            //parameters.Add("id", emp.userId, DbType.Int32);
            parameters.Add("fName", emp.fName, DbType.String);
            parameters.Add("lName", emp.lName, DbType.String);
            parameters.Add("email", emp.email, DbType.String);
            parameters.Add("phNo", emp.phNo, DbType.String);
            parameters.Add("address", emp.address, DbType.String);
            parameters.Add("pincode", emp.pincode, DbType.String);
            parameters.Add("roleId", emp.roleId, DbType.Int32);

            using (var conn = this.context.CreateConnection()) {
                await conn.ExecuteAsync(query, parameters);
                response = "Employee inserted successfully";
            }

                return response;
        }

        public async Task<string> Update(Employee emp, int id)
        {
            string response = string.Empty;
            string query = "update Employee set fName = @fName , lName = @lName, email = @email, phNo = @phNo, Address = @address, pincode = @pincode, roleId = @rId where userId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("fName", emp.fName, DbType.String);
            parameters.Add("lName", emp.lName, DbType.String);
            parameters.Add("email", emp.email, DbType.String);
            parameters.Add("phNo", emp.phNo, DbType.String);
            parameters.Add("address", emp.address, DbType.String);
            parameters.Add("pincode", emp.pincode, DbType.Int32);
            parameters.Add("rId", emp.roleId, DbType.Int32);
            parameters.Add("id", emp.userId, DbType.Int32);

            using (var conn = this.context.CreateConnection()) {
                await conn.ExecuteAsync(query,parameters);
                response = "Employee detials updated successfully";
            }
            return response;
        }
    }
}
