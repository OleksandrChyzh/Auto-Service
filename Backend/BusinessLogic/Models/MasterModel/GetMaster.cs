using BusinessLogic.Models.User;

namespace BusinessLogic.Models.MasterModel
{
    public class GetMaster 
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
    }

}
