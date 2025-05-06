namespace WebApplication1.Models
{
    public class UpdateMatriculationDataRequest
    {
        public int MorningTesters { get; set; }
        public int EveningTesters { get; set; }
        public int ModeratorId { get; set; }
        public string CoordinatorName { get; set; } = null!;
        public string CoordinatorEmail { get; set; } = null!;
        public string CoordinatorPhone { get; set; } = null!;
        public int LaboratoryRooms { get; set; }
        public List<string> AccompanyingTeachers { get; set; } = new();
    }
}