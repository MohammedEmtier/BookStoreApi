using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore.API.Repository
{
    public interface IAutherRepoistory<Entity>
    {
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task AddAsync(Entity entity);
        Task UpdateAsync(int id, Entity bookModel);
        Task UpdatePatchAsync(int id, JsonPatchDocument bookModel);
        Task DeleteAsync(int id);
    }
}
