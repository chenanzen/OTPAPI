using OTPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPConsole
{
    public class Email_OTP_Module
    {
        private readonly IOTPData _OTPData;
        private readonly IOTPService _OTPService;
        private readonly IEmailService _EmailService;
        private string _email;

        public Email_OTP_Module()
        {
            _OTPData = new OTPData();
            _OTPService = new OTPService.OTPService(_OTPData);
            _EmailService = new EmailService();
            _email = string.Empty;
        }

        public STATUS_EMAIL generate_OTP_email(string user_email)
        {
            var generatedOTP = _OTPService.GenerateOTP(user_email);
            if (generatedOTP == null) return STATUS_EMAIL.STATUS_EMAIL_INVALID;
            else
            {
                var email_body = $"Your OTP Code is {generatedOTP.OTPValue}. The code is valid for 1 minute.";
                try
                {
                    _EmailService.send_email(user_email, email_body);
                    _email = user_email;
                    return STATUS_EMAIL.STATUS_EMAIL_OK;
                }
                catch
                {
                    // fail to send email
                    return STATUS_EMAIL.STATUS_EMAIL_FAIL;
                }
            }
        }

        public STATUS_OTP check_OTP()
        {
            Console.WriteLine("Please enter your OTP in 1 minutes.");
            string otp = Console.ReadLine() ?? string.Empty;
            var result = _OTPService.VerifyOTP(_email, otp);

            return result;
        }
    }
}
