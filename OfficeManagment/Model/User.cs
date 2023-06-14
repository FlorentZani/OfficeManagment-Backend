using System.ComponentModel.DataAnnotations;

namespace OfficeManagment.Model
{
    public class User : BaseClass
    {
        public string Name { get; set; }    
        public string LastName { get; set; }
        public List<UserRole> Role { get; set; }
        public List<UserProjects> Project { get;set; }

    }
}
