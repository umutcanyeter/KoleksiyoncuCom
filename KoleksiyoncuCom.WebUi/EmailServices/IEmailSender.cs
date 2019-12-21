using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoleksiyoncuCom.WebUi.EmailServices
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
