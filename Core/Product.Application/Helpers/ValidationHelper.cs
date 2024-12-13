using System.Text.RegularExpressions;

namespace Product.Application.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidPassword(string password)
        {
            var passwordPattern = @"^(?=.[A-Z])(?=.[0-9])(?=.{7,})";
            if (Regex.IsMatch(password, passwordPattern))
            {
                return true;
            }
            return false;
        }
    }
}
