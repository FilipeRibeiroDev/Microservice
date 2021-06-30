using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Account.Domain.Utilities
{
    public static class EmailText
    {
        public static bool IsValid(string emailaddress)
        {
            try
            {
                if (string.IsNullOrEmpty(emailaddress))
                    return false;

                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
