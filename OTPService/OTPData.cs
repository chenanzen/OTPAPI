using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPService
{
    public class OTPData : IOTPData
    {
        private readonly TimeSpan _OTPValidTime;
        private readonly int _MAXTRY;
        private readonly List<string> _ALLOWED_DOMAIN;
        private readonly Dictionary<string, OTP> _OTPs;
        
        public OTPData() 
        {
            _OTPs = new Dictionary<string, OTP>();
            _MAXTRY = 10;
            _ALLOWED_DOMAIN = new List<string>() { ".dso.org.sg" };
            _OTPValidTime = new TimeSpan(0, 1, 0);
        }

        public OTP AddOrUpdateOTP(string key, string value)
        {
            var newOTP = new OTP(key, value);
            _OTPs[key] = newOTP;

            return newOTP;
        }

        public OTP GetOTP(string key)
        {
            return _OTPs[key];
        }

        public List<string> GetOTPAllowedDomain()
        {
            return _ALLOWED_DOMAIN;
        }

        public int GetOTPMaxTry()
        {
            return _MAXTRY;
        }

        public TimeSpan GetOTPValidTime()
        {
            return _OTPValidTime;
        }
    }

    public interface IOTPData
    {
        OTP AddOrUpdateOTP(string key, string value);
        OTP GetOTP(string key);
        int GetOTPMaxTry();
        List<string> GetOTPAllowedDomain();
        TimeSpan GetOTPValidTime();
    }


}
