using BookStore.API.Models;
using BookStore.API.Repository;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public AccountController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _accountRepository = accountRepository;
            this.userManager = userManager;
            this.emailSender = emailSender;
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
            var email = await userManager.FindByEmailAsync(signUpModel.Email);
            if (email == null)
            {
                if (signUpModel is null)
                {
                    throw new ArgumentNullException(nameof(signUpModel));
                }

                var result = await _accountRepository.SignUpAsync(signUpModel);
              await emailSender.sendemail(signUpModel.Email, "BookStore Register", "Please confirm your account by clicking here Your Register is Done");
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
            var user = userManager.ErrorDescriber.DuplicateEmail(signUpModel.Email);
            return StatusCode(400, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);
            var defalut = new DefaultformApi()
            {
                data = new { token = result },
                message = "login done Succesfully",
                error = "No error 200"
            };
            var email = await userManager.FindByEmailAsync(signInModel.Email);
            if (email != null)
            {
                //RecurringJob.AddOrUpdate(() => sendmessage("aliW"), Cron.Monthly(day: 1, hour: 12));
                // BackgroundJob.Schedule(()=>sendmessage("aliW"),delay:TimeSpan.FromSeconds(3));


                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized();
                }

                return Ok(defalut);
            }
            var user = userManager.ErrorDescriber.InvalidEmail(signInModel.Email);
            return BadRequest(user);

        }
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public void sendmessage(String email)
        //{
        //    Console.Write("dfnmfmf");
        //}
    }
}
