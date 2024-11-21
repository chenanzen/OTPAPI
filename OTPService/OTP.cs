using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPService
{
    public class OTP
    {
        public string OTPKey { get; set; }
        public string OTPValue { get; set; }
        public DateTime GeneratedDateTime { get; set; }
        public int NumOfTry { get; set; }

        public OTP() 
        {
            OTPKey = string.Empty;
            OTPValue = string.Empty;
            GeneratedDateTime = DateTime.MinValue;
            NumOfTry = 0;
        }

        public OTP(string oTPKey, string oTPValue)
        {
            OTPKey = oTPKey;
            OTPValue = oTPValue;
            GeneratedDateTime = DateTime.Now;
            NumOfTry = 0;
        }

        public OTP(string oTPKey, string oTPValue, DateTime generatedDateTime)
        {
            OTPKey = oTPKey;
            OTPValue = oTPValue;
            GeneratedDateTime = generatedDateTime;
            NumOfTry = 0;
        }
    }
}
