using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsUserServices
    {
        protected Dictionary<string, clsUser> UsersDB { get; }
        protected clsLastIdInfo LastIdInfo { get; }

        public clsUserServices(clsDataManager dataManager)
        {
            this.UsersDB = dataManager.UsersDB;
            this.LastIdInfo = dataManager.LastIdInfo;
        }


        public clsUser GetUserByUsernameAndPassword(string username, string password)
        {
            return (Exists(username) && UsersDB[username].Password == password)
                ? UsersDB[username] : null!;
        }

        public clsUser AddUser(string username, string password)
        {
            return (!Exists(username))
               ? UsersDB[username] = new clsUser(++LastIdInfo.UserID, username, password) : null!;
        }

        public bool Exists(string username)
        {
            return UsersDB.ContainsKey(username);
        }

        public List<string> GetAllUsernames()
        {
            return UsersDB.Keys.Select(k => k).ToList();
        }
    }
}
