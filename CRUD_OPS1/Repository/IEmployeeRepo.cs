using CRUD_OPS1.Model;

namespace CRUD_OPS1.Repository
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAll();

        Task<Employee> GetById(int id);

        Task<string> Insert(Employee emp);

        Task<string> Update(Employee emp);

        Task<string> DeleteById(int id);
    }
}
