using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateTestTask.Service.DTOs.Auth;
using CandidateTestTask.Service.DTOs.Users;

namespace CandidateTestTask.Test;

public class UserCreateDtoTest
{
    [Theory]
    [InlineData("Fazliddin", "Mustafoyev", "14:00 to 18:00", "21O92oo1", "+998932313101", "info", "https://github.com/FazliddinMustafoyev", "https://www.linkedin.com/in/fazliddin-mustafoyev-6252b1238/")]
    [InlineData("Qahhor", "Xurramov", "10:00 to 12:00", "Q123456", "+998963242002", "information", "https://github.com/FazliddinMustafoyev", "https://www.linkedin.com/in/fazliddin-mustafoyev-6252b1238/")]
    [InlineData("Normadjon", "G'offorov", "8:00 to 14:00", "Nn123456", "+9901230011", "desc", "https://github.com/FazliddinMustafoyev", "https://www.linkedin.com/in/fazliddin-mustafoyev-6252b1238/")]

    public void UserCreatereturnTrue(string name, string lastname, string time,
         string password, string number, string description, string url, string lurl)
    {
        UserCreateModel dto = new()
        {
            FirstName = name,
            LastName = lastname,
            BetterTimeToCall = time,
            Password = password,
            PhoneNumber = number,
            Description = description,
            GitHubUrl = url,
            LinkedInUrl = lurl

        };
        try
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), true);
            Assert.True(true);
        }
        catch (Exception ex)
        {

            Assert.Fail(ex.Message);
        }
    }
    [Theory]
    [InlineData("Fazliddin", "Mustafoyev", "14:00 to 18:00", "21O92oo1", "+998932313101", "info", "https://github.com/FazliddinMustafoyev", "https://www.linkedin.com/in/fazliddin-mustafoyev-6252b1238/")]
    [InlineData("Qahhor", "Xurramov", "10:00 to 12:00", "Q123456", "+998963242002", "information", "https://github.com/FazliddinMustafoyev", "https://www.linkedin.com/in/fazliddin-mustafoyev-6252b1238/")]
    [InlineData("Normadjon", "G'offorov", "8:00 to 14:00", "Nn123456", "+9901230011", "desc", "https://github.com/FazliddinMustafoyev", "https://www.linkedin.com/in/fazliddin-mustafoyev-6252b1238/")]
    public void CandidateRegisterReturnFalse(string name, string lastname, string time,
        string password, string number, string description, string url, string lurl )
    {
        UserCreateModel dto = new()
        {
            FirstName= name,
            LastName= lastname,
            BetterTimeToCall = time,
            Password= password,
            PhoneNumber = number,
            Description= description,
            GitHubUrl = url,
            LinkedInUrl= lurl

        };
        try
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), false);
            Assert.False(false);
        }
        catch (Exception ex)
        {

            Assert.Fail(ex.Message);
        }
    }
}
