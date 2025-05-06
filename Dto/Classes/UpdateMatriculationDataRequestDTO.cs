namespace WebApplication1.Dto.Classes
{
    public class UpdateMatriculationDataRequestDTO
    {
        public int MorningTesters { get; set; }
        public int EveningTesters { get; set; }
        public string Region { get; set; } = null!;
        public string CoordinatorName { get; set; } = null!;
        public string CoordinatorEmail { get; set; } = null!;
        public string CoordinatorPhone { get; set; } = null!;
        public int LaboratoryRooms { get; set; }
        public List<string> AccompanyingTeachers { get; set; } = new();
    }
    public class UserCredentialsResult
    {
        public int InstitutionId { get; set; }
        public int UserId { get; set; }
    }
}