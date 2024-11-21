using OTPService;

namespace OTPTestProject
{
    public class OTPServiceTest
    {
        private readonly IOTPData _otpData;
        private readonly OTPService.OTPService _otpService;

        public OTPServiceTest()
        {
            _otpData = new OTPData();
            _otpService = new OTPService.OTPService(_otpData);
        }

        [Theory]
        [InlineData("someone@dept.dso.org.sg")]
        public void OnlyDSODomainEmailAllowed(string email)
        {
            var generatedOTP = _otpService.GenerateOTP(email);

            Assert.NotNull(generatedOTP);
        }

        [Theory]
        [InlineData("dept.dso.org.sg.")]
        [InlineData("someone@dept.dso.org.sg.")]
        [InlineData("someone@gmail.com")]
        public void BadEmailNotAllowed(string email)
        {
            var generatedOTP = _otpService.GenerateOTP(email);

            Assert.Null(generatedOTP);
        }
    }
}