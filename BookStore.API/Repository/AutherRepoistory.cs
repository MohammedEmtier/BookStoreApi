using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repository
{
    public class AutherRepoistory : IAutherRepoistory<Auther>
    {
        private readonly IMapper mapper;
        private readonly BookStoreContext context;

        public AutherRepoistory(IMapper mapper, BookStoreContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task AddAsync(Auther entity)
        {

            context.Auther.Add(entity);
            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            Auther auther = new Auther()
            {
                Id = (byte)id
            };
            context.Auther.Remove(auther);
            await context.SaveChangesAsync();

        }

        public async Task<List<Auther>> GetAllAsync()
        {

            var auther = await context.Auther.ToListAsync();
            return mapper.Map<List<Auther>>(auther);

        }

        public async Task<Auther> GetByIdAsync(int id)
        {

            var auther = await context.Auther.FindAsync((byte)id);
            return mapper.Map<Auther>(auther);
        }

        public async Task UpdateAsync(int id, Auther Model)
        {
            var auther = await context.Auther.FindAsync((byte)id);
            if (auther != null)
            {
                auther.AutherName = Model.AutherName;

            }
            /*Auther auther1 = new Auther
            {
                Id = (byte)id,
                AutherName = Model.AutherName
            };*/
            //context.Auther.Update(auther1);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePatchAsync(int id, JsonPatchDocument Model)
        {
            var auther = await context.Auther.FindAsync((byte)id);
            if (auther != null)
            {
                Model.ApplyTo(auther);
                await context.SaveChangesAsync();

            }
        }


    }
}
