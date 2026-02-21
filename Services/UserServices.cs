using SocialApp.Model;


namespace SocialApp.Services
{
    public class UserServices
    {
        public Dictionary<string, User> UsersDB { get; }

        public UserServices(DataManager dataManager)
        {
            UsersDB = dataManager.UsersDB;
        }


        public User GetUser(string username, string password)
        {
            if (UsersDB.ContainsKey(username) && UsersDB[username].Password == password)
            {
                return UsersDB[username];
            }
            return null;
        }
        public bool Exists(string username)
        {
            return UsersDB.ContainsKey(username);
        }
        public void AddUser(User user)
        {
            if (UsersDB.ContainsKey(user.Name))
            {
                return;
            }
            UsersDB[user.Name] = user;
        }

    }
}
