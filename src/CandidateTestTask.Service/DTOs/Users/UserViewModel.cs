using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CandidateTestTask.Service.DTOs.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string GitHubUrl { get; set; }
    public string LinkedInUrl { get; set; }
    public string Description { get; set; }
    public string BetterTimeToCall { get; set; }
}
