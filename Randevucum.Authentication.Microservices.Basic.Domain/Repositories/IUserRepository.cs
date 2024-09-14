using Randevucum.Authentication.Microservices.Basic.Domain.Entities;
using Randevucum.Authentication.Microservices.Basic.Domain.ValueObjects;

namespace Randevucum.Authentication.Microservices.Basic.Domain.Repositories;

public interface IUserRepository
{
    User GetById(Guid id);
    User GetByEmail(Email email);
    void Add(User user);
    void Update(User user);
    void Delete(Guid id);
}