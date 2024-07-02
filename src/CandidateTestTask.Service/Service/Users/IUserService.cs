using CandidateTestTask.Service.Configurations;
using CandidateTestTask.Service.DTOs.Auth;
using CandidateTestTask.Service.DTOs.Users;

public interface IUserService
{
    Task<UserViewModel> CreateAsync(UserCreateModel model);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<LoginViewModel> LoginAsync(LoginCreateModel login);
}