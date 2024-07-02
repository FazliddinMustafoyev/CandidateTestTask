using CandidateTestTask.Domain.Entities;
using CandidateTestTask.DataAcces.DbContexts;
using CandidateTestTask.DataAcces.Repositories;

namespace CandidateTestTask.DataAcces.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public IRepository<User> Users { get; }

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Users = new Repository<User>(context);
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}