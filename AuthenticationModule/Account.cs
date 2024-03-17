using System.Runtime.InteropServices;

namespace SchoolTestsApp.AuthenticationModule
{
    public static class Account
    {
        protected class User
        {
            public int id { get; set; }
            public string password { get; set; }

            public string login { get; set; }

            public bool isStudent { get; set; }

        }

        private static User? currentUser { get; set; } = null;

        public static bool isAuthenticated()
        {
            if (currentUser != null)
            {
                return true;
            }
            else
            {
                 return false;
            }
        }

        public static bool isStudent()
        {
            if (currentUser != null)
            {
                return currentUser.isStudent;
            }
            else
            {
                return false;
            }
        }

        public static string? GetPassword()
        {
            if (currentUser != null)
            {
                return currentUser.password;
            }
            else
            {
                return null;
            }
        }

        public static string? GetLogin()
        {
            if (currentUser != null)
            {
                return currentUser.login;
            }
            else
            {
                return null;
            }
        }

        public static int? GetID()
        {
            if (currentUser != null)
            {
                return currentUser.id;
            }
            else
            {
                return null;
            }
        }

        public static void init(int id, string login, string password, bool isStuddent)
        {
            currentUser = new User()
            {
                id=id,
                login = login,
                password = password,
                isStudent = isStuddent
            };
        }
    }


}
