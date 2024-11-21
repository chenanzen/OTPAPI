// See https://aka.ms/new-console-template for more information
using OTPConsole;
using OTPService;
using System.Xml.Linq;


var otpmodule = new Email_OTP_Module();

// get user's email address
Console.WriteLine("Please enter your email: ");
var email = Console.ReadLine() ?? string.Empty;

// generate OTP
var emailStat = otpmodule.generate_OTP_email(email);

if (emailStat == STATUS_EMAIL.STATUS_EMAIL_INVALID)
{
    Console.WriteLine("Invalid email address. Please enter valid email DSO email address.");
}
else if (emailStat == STATUS_EMAIL.STATUS_EMAIL_FAIL)
{
    Console.WriteLine("OTP System is not able to send OTP to your email at the moment. " +
        "We are sorry for any inconvenience. Please contact our support at " +
        "Support@support.dso.org.sg for any further assistance.");
}
else
{
    // emailStat is OK
    var keepChecking = true;
    while (keepChecking)
    {
        var result = otpmodule.check_OTP();
        switch (result)
        {
            case STATUS_OTP.STATUS_OTP_OK:
                Console.Write("Correct OTP received.");
                keepChecking = false;
                break;

            case STATUS_OTP.STATUS_OTP_NOTOK:
                Console.Write("Incorrect OTP received. Please try again.");
                break;

            case STATUS_OTP.STATUS_OTP_FAIL:
                // fail more than 10x. stop process
                Console.Write("Number of incorrect tries have been exceeded. System will now terminate. It has been a pleasure serving you.");
                keepChecking = false;
                break;

            case STATUS_OTP.STATUS_OTP_TIMEOUT:
                // timeout, user is not responsive. stop process.
                Console.Write("Generated OTP has expired. System will now terminate. It has been a pleasure serving you.");
                keepChecking = false;
                break;
        }
    }

    Console.ReadLine();
}



