namespace CRUD_OPS1.Dtos
{
    public class EmployeeSignUpDTO
    {
        public int userId { get; set; }
        public string fName { get; set; }

        public string lName { get; set; }

        public string email { get; set; }

        public long phNo { get; set; }

        public string address { get; set; }

        public int pincode { get; set; }

        public int roleId { get; set; }

        public string password { get; set; }
    }
}
