


namespace SocialApp
{
    public static class clsInputValidation
    {
        public static bool IsValidPost(string post)
        {
            return post != null && post.Length >= 5;
        }

        public static string GetUsernameAndPasswordValidateErrorMessage(string username, string password)
        {
            if (username.Length > 10)
            {
                return "Username must be equal or less than 10 letters";
            }
            if (username.Length < 3)
            {
                return "Username must be 3 letters atleast";
            }
            if (password.Length < 5)
            {
                return "Password must be 5 letters or more";
            }
            return string.Empty;
        }

    }
}
