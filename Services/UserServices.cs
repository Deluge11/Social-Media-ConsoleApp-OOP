using SocialApp.Model;


namespace SocialApp.Services
{
    public class UserServices
    {
        public Dictionary<string, User> UsersDB { get; }
        public LastIdInfo LastIdInfo { get; }

        public UserServices(DataManager dataManager)
        {
            this.UsersDB = dataManager.UsersDB;
            this.LastIdInfo = dataManager.LastIdInfo;
        }


        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return (Exists(username) && UsersDB[username].Password == password)
                ? UsersDB[username] : null!;
        }

        public User AddUser(string username, string password)
        {
            return (!Exists(username))
               ? UsersDB[username] = new User(++LastIdInfo.UserID, username, password) : null!;
        }

        public bool Exists(string username)
        {
            return UsersDB.ContainsKey(username);
        }
    }
}
