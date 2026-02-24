using SocialApp.Model;


namespace SocialApp.Services
{
    public class AuthenticationServices
    {
        private AppState AppState { get; }
        private UserServices UserServices { get; }

        public AuthenticationServices(AppState appState, UserServices userServices)
        {
            AppState = appState;
            UserServices = userServices;
        }

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"\tLogin Screen");
            Console.WriteLine("-----------------------------");

            Console.Write("| Enter the Username: ");
            string username = Console.ReadLine()!.Trim();

            Console.Write("| Enter the Password: ");
            string password = Console.ReadLine()!.Trim();

            AppState.User = UserServices.GetUser(username, password);

            if (AppState.User != null)
            {
                AppState.IsAuthenticated = true;
                Console.Clear();
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"| Welcome {username} ");
                Console.WriteLine("| Press any key to continue");
                Console.WriteLine("-----------------------------");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("| Username or Password is invalid");
            Console.WriteLine("| Press any key to continue");
            Console.ReadKey();

        }

        public void Logout()
        {
            AppState.IsAuthenticated = false;
        }

        public void Register()
        {
            Console.Clear();

            Console.WriteLine("-----------------------------");
            Console.WriteLine($"\tRegister Screen");
            Console.WriteLine("-----------------------------");


            Console.Write("| Enter the Username: ");
            string username = Console.ReadLine()!.Trim();

            Console.Write("| Enter the Password: ");
            string password = Console.ReadLine()!.Trim();

            string validateAuthInfoResult = ValidateAuthInfo(username, password);

            if (!string.IsNullOrEmpty(validateAuthInfoResult))
            {
                Console.Clear();
                Console.WriteLine(validateAuthInfoResult);
                Console.ReadKey();
                return;
            }

            if (UserServices.Exists(username))
            {
                Console.Clear();
                Console.WriteLine("| This user exists already");
                Console.ReadKey();
                return;
            }

            AppState.IsAuthenticated = true;
            AppState.User = UserServices.AddUser(username, password);
        }

        private string ValidateAuthInfo(string username, string password)
        {
            if (username.Length > 10)
            {
                return "| Username must be equal or less than 10 letters";
            }
            if (username.Length < 3)
            {
                return "| Username must be 3 letters atleast";
            }
            if (password.Length < 5)
            {
                return "| Password must be 5 letters or more";
            }
            return string.Empty;
        }
    }
}
