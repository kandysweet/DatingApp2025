using System;
using API.Entities;

namespace API.Interfaces;

public interface IMembersRepository
{
    void Update(Member member);
    Task<bool> SaveAllAsync();
    Task<IReadOnlyList<Member>> GetAllAsync();
    Task<Member?> GetMemberAsync(string id);
    Task<IReadOnlyList<Photo>> GetPhotosAsync(string memberId);
    
}
