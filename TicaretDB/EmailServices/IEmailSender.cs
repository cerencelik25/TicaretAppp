using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicaretDB.EmailServices
{
    public interface IEmailSender
    {
        //smtp
        //api => sendgrid
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
