using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Account.Domain.Utilities
{
    public static class PasswordPolicy
    {
        private static readonly int Minimum_Length = 7;
        private static readonly int Upper_Case_length = 1;
        private static readonly int Lower_Case_length = 1;
        private static readonly int NonAlpha_length = 1;
       
        public static bool IsValid(string Password)
        {
            if (string.IsNullOrEmpty(Password))
                return false;
            if (Password.Length < Minimum_Length)
                return false;
            if (UpperCaseCount(Password) < Upper_Case_length)
                return false;
            if (LowerCaseCount(Password) < Lower_Case_length)
                return false;
            if (NumericCount(Password) < 1)
                return false;
            if (NonAlphaCount(Password) < NonAlpha_length)
                return false;
            return true;
        }

        private static int UpperCaseCount(string Password)
        {
            return Regex.Matches(Password, "[A-Z]").Count;
        }

        private static int LowerCaseCount(string Password)
        {
            return Regex.Matches(Password, "[a-z]").Count;
        }
        private static int NumericCount(string Password)
        {
            return Regex.Matches(Password, "[0-9]").Count;
        }
        private static int NonAlphaCount(string Password)
        {
            return Regex.Matches(Password, @"[^0-9a-zA-Z\._]").Count;
        }
    }
}
