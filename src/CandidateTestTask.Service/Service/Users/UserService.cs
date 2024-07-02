using AutoMapper;
using CandidateTestTask.DataAcces.UnitOfWorks;
using CandidateTestTask.Domain.Entities;
using CandidateTestTask.Service.Configurations;
using CandidateTestTask.Service.DTOs.Auth;
using CandidateTestTask.Service.DTOs.Users;
using CandidateTestTask.Service.Exceptions;
using CandidateTestTask.Service.Extensions;
using CandidateTestTask.Service.Helpers;
using CandidateTestTask.Service.Validators.Auth;
using CandidateTestTask.Service.Validators.Users;
using Microsoft.EntityFrameworkCore;

public class UserService  : IUserService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly UserCreateModelValidator createModelValidator;
    private readonly UserUpdateModelValidator updateModelValidator;
    private readonly LoginCreateModelValidator loginValidator;
    public UserService(
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        UserCreateModelValidator  createModelValidator, 
        UserUpdateModelValidator updateModelValidator, 
        LoginCreateModelValidator loginValidator)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.createModelValidator = createModelValidator;
        this.loginValidator = loginValidator;
        this.updateModelValidator = updateModelValidator;
    }
    public async Task<UserViewModel> CreateAsync(UserCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email);

        if (existUser is not null)
            return await UpdateAsync(existUser.Id, mapper.Map<UserUpdateModel>(model));

        var mappedUser = mapper.Map<User>(model);
        mappedUser.Password = PasswordHasher.Hash(model.Password);
        var createdUser = await unitOfWork.Users.InsertAsync(mappedUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(createdUser);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException("User is not found");

        mapper.Map(model, existUser);
        existUser.UpdatedAt = DateTime.UtcNow;
        var updateModel = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(updateModel);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Id == id)
            ?? throw new NotFoundException("User is not found");

        existUser.DeletedAt = DateTime.UtcNow;
        await unitOfWork.Users.DeleteAsync(existUser);
        return true;
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id)
            ?? throw new NotFoundException("User is not found");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users.
            SelectAsQueryable(isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(p =>
             p.FirstName.ToLower().Contains(search.ToLower()) ||
             p.LastName.ToLower().Contains(search.ToLower()));

        var paginateUsers = await users.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<UserViewModel>>(paginateUsers);
    }
    public async Task<LoginViewModel> LoginAsync(LoginCreateModel login)
    {
        await loginValidator.EnsureValidatedAsync(login);
        var existUser = await unitOfWork.Users.
            SelectAsync(expression: user => user.Email == login.Email)
            ?? throw new ArgumentIsNotValidException("Email or Password is not valid");

        if (!PasswordHasher.Verify(login.Password, existUser.Password))
            throw new ArgumentIsNotValidException("Email or Password is not valid");

        var loginViewModel = new LoginViewModel
        {
            User = mapper.Map<UserViewModel>(existUser),
            Token = AuthHelper.GenerateToken(existUser)
        };
        return loginViewModel;
    }
}