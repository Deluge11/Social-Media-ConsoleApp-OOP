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
        public User AddUser(string username, string password)
        {
            if (!UsersDB.ContainsKey(username))
            {
                UsersDB[username] = new User(++LastIdInfo.UserID,username,password);
                return UsersDB[username];
            }
            return null;
        }

    }
}
