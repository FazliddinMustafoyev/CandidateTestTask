﻿using CandidateTestTask.Domain.Commons;
namespace CandidateTestTask.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string GitHubUrl { get; set; }
    public string LinkedInUrl { get; set; }
    public string Description { get; set; }
    public string BetterTimeToCall { get; set; }
}