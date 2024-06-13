using CRUD_OPS1.Model;

namespace CRUD_OPS1.Repository
{
    public interface ICredentialRepo
    {
        Task<Credentials> ValidateCredentials(Credentials cred);

        void InsertNewCredentials(string email, string password);

        Task<string> DeleteById(string email);
    }
}
