using BookStore.API.Models;
using BookStore.API.Repository;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignUpModel signUpModel)
        {
            if (signUpModel is null)
            {
                throw new ArgumentNullException(nameof(signUpModel));
            }

            var result = await _accountRepository.SignUpAsync(signUpModel);

            if (result.Succeeded)
            {
                var defalut = new DefaultformApi()
                {
                    data = result,
                    message = "Sign Up Succesfully",
                    error = "No error 200"
                };
                return Ok(defalut);
            }
            AddErrors(result);
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] SignInModel signInModel)
        {
            RecurringJob.AddOrUpdate(()=> sendmessage("aliW"),Cron.Monthly(day:1,hour:12));
           // BackgroundJob.Schedule(()=>sendmessage("aliW"),delay:TimeSpan.FromSeconds(3));
            var result = await _accountRepository.LoginAsync(signInModel);
            var defalut = new DefaultformApi()
            {
                data = new { token= result },
                message = "login done Succesfully",
                error = "No error 200"
            };
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(defalut);
        }
        [ApiExplorerSettings(IgnoreApi =true)]
        public void sendmessage(String email)
        {
            Console.Write("dfnmfmf");
        }
    }
}
