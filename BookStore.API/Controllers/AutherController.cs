using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutherController : ControllerBase
    {
        private readonly IAutherRepoistory<Auther> autherRepoistory;
        private readonly IStringLocalizer<AutherController> stringLocalizer;

        public AutherController(IAutherRepoistory<Auther> autherRepoistory, IStringLocalizer<AutherController> stringLocalizer)
        {
            this.autherRepoistory = autherRepoistory;
            this.stringLocalizer = stringLocalizer;
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAuther([FromForm] Auther autherModel, [FromForm] int id)
        {
            await autherRepoistory.UpdateAsync(id, autherModel);
            DefaultformApi formDefault = new DefaultformApi()
            {
                data = "The data of Auther Updated Done",
                error = "no error 200",
                message = "The data of Auther Updated Done",
            };
            return Ok(formDefault);
        }
        [HttpPatch("updatepatch/{id}")]
        public async Task<IActionResult> UpdatePatcherAuther([FromForm] JsonPatchDocument autherModel, [FromRoute] int id)
        {

            await autherRepoistory.UpdatePatchAsync(id, autherModel);
            DefaultformApi formDefault = new DefaultformApi()
            {
                data = "The data of Auther Updated Done",
                error = "no error",
                message = "The data of Auther Updated Done",
            };
            return Ok(formDefault);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAuther()
        {
           var auther= await autherRepoistory.GetAllAsync();
            var value = stringLocalizer["Welcome"];

            DefaultformApi formDefault = new DefaultformApi()
            {
                data = new { Authers=auther },
                error = value,
                message = "The data of Autheres return Done",
            };
            return Ok(formDefault);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAuther([FromForm]Auther autherModel)
        {
            await autherRepoistory.AddAsync(autherModel);
            DefaultformApi formDefault = new DefaultformApi()
            {
                data="The data of Auther added Done",
                error="no error 200",
                message= "The data of Auther added Done",
            };
            return Ok(formDefault);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getAutherbyId([FromRoute]int id)
        {
           Auther auther= await autherRepoistory.GetByIdAsync(id);
            DefaultformApi formDefault = new DefaultformApi()
            {
                data = auther,
                error = "no error 200",
                message = "The data of Auther return Done",
            };
            return Ok(formDefault);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveAuther([FromForm] int id)
        {
            await autherRepoistory.DeleteAsync(id);
            DefaultformApi formDefault = new DefaultformApi()
            {
                data = "The data of Auther Removed Done",
                error = "no error 200",
                message = "The data of Auther Removed Done",
            };
            return Ok(formDefault);
        }
    }
}