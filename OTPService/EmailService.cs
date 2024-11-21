using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPService
{
    public class EmailService : IEmailService
    {
        public EmailService() { }

        public void send_email(string email_address, string email_body)
        {
            // assume this is done
            Console.WriteLine(email_body);
        }
    }

    public interface IEmailService
    {
        void send_email(string email_address, string email_body);
    }
}
