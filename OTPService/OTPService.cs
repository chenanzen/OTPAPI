using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPService
{
    public class OTPService : IOTPService
    {
        private readonly IOTPData _data;
        public OTPService(IOTPData data)
        {
            _data = data;
        }

        public OTP? GenerateOTP(string email)
        {
            OTP? result = null;

            Random generator = new Random();
            String otpValue = generator.Next(0, 1000000).ToString("D6");

            System.Net.Mail.MailAddress? mailAddress = null;
            try
            {
                mailAddress = new System.Net.Mail.MailAddress(email);
            }
            catch 
            {
                // invalid email
                mailAddress = null;
            }

            if (mailAddress != null)
            {
                // must be dso domain
                var host = mailAddress.Host;
                var allowedDomain = _data.GetOTPAllowedDomain();
                var isAllowedDomain = allowedDomain.Any(d => host.EndsWith(d));
                if (isAllowedDomain)
                {
                    result = _data.AddOrUpdateOTP(email, otpValue);
                }
            }

            return result;
        }

        public STATUS_OTP VerifyOTP(string email, string otp)
        {
            var currentTime = DateTime.Now;
            var foundOTP = _data.GetOTP(email);

            if (foundOTP == null)
            {
                // OTP not found
                return STATUS_OTP.STATUS_OTP_FAIL;
            }
            else
            {
                var elapseTime = currentTime - foundOTP.GeneratedDateTime;
                // check timing 
                if (elapseTime > _data.GetOTPValidTime())
                {
                    return STATUS_OTP.STATUS_OTP_TIMEOUT;
                }
                else
                {
                    if (foundOTP.NumOfTry >= _data.GetOTPMaxTry())
                    {
                        // Fail after 10 tries
                        return STATUS_OTP.STATUS_OTP_FAIL;
                    }
                    else
                    {
                        // not yet max tries. continue validate OTP value
                        if (foundOTP.OTPValue != otp)
                        {
                            // otp value incorrect
                            foundOTP.NumOfTry++;
                            return STATUS_OTP.STATUS_OTP_NOTOK;
                        }
                        else
                        {
                            return STATUS_OTP.STATUS_OTP_OK;

                        }
                    }
                }
            }
        }
    }

    public interface IOTPService
    {
        OTP? GenerateOTP(string email);
        STATUS_OTP VerifyOTP(string email, string otp);
    }
}
