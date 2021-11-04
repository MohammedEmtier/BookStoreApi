using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookStore.API.Repository
{
    public interface IEmailSender
    {
        Task sendemail(String email, String subject,String body,IList<IFormFile> attachments=null);
    }
}
