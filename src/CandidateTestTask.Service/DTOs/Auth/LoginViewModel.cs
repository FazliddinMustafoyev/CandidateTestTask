using CandidateTestTask.Service.DTOs.Users;

namespace CandidateTestTask.Service.DTOs.Auth;

public class LoginViewModel
{
    public UserViewModel User { get; set; }
    public string Token { get; set; }
}