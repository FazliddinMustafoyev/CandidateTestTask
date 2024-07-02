using CandidateTestTask.Domain.Entities;
using CandidateTestTask.DataAcces.Repositories;

namespace CandidateTestTask.DataAcces.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    Task<bool> SaveAsync();
}