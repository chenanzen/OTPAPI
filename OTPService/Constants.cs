using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPService
{
    public enum STATUS_EMAIL
    {
        STATUS_EMAIL_OK,
        STATUS_EMAIL_FAIL,
        STATUS_EMAIL_INVALID
    }

    public enum STATUS_OTP
    {
        STATUS_OTP_OK,
        STATUS_OTP_NOTOK,
        STATUS_OTP_FAIL,
        STATUS_OTP_TIMEOUT
    }
}
