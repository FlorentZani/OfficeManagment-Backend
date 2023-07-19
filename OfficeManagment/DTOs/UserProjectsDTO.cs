using OfficeManagment.Model;

namespace OfficeManagment.DTOs
{
    public class UserProjectsDTO 
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public Guid PositionId { get; set; }
        public int WorkingHours { get; set; }
        public string ProgrammingLanguage { get; set; }
        
    }
}
