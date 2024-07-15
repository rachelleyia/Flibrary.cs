using System.Collections.Generic;

namespace FilmLibrary
{
    internal static class Account
    {
        private static readonly List<string> Users = new List<string>();
        private static readonly Dictionary<string, int> LoginAttempts = new Dictionary<string, int>();

        static Account()
        {
            CreateDummyUsers();
        }

        private static void CreateDummyUsers()
        {
            Users.Add("F-library-0001");
            Users.Add("F-library-0002");
            Users.Add("F-library-0003");
            Users.Add("F-library-0004");
            Users.Add("F-library-0005");
        }

        public static string Login(string usernumber)
        {
            if (!LoginAttempts.ContainsKey(usernumber))
            {
                LoginAttempts[usernumber] = 0;
            }

            if (LoginAttempts[usernumber] < 3)
            {
                if (Users.Contains(usernumber))
                {
                    LoginAttempts[usernumber] = 0;
                    return "Login successful";
                }
                else
                {
                    LoginAttempts[usernumber]++;
                    int attemptsLeft = 3 - LoginAttempts[usernumber];
                    return $"Incorrect credentials (attempts left: {attemptsLeft})";
                }
            }
            else
            {
                return "Maximum login attempts reached. Please try again.";
            }
        }
    }
}