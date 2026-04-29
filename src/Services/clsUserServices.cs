using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{

    public class clsUserServices
    {
        public static clsUser GetUserByUsernameAndPassword(string username, string password)
        {
            return (Exists(username) && clsDataManager.UsersDB[username].Password == password)
                ? clsDataManager.UsersDB[username] : null!;
        }

        public static clsUser AddUser(string username, string password)
        {
            return (!Exists(username))
               ? clsDataManager.UsersDB[username] = new clsUser(++clsDataManager.LastIdInfo.UserID, username, password) : null!;
        }

        public static bool Exists(string username)
        {
            return clsDataManager.UsersDB.ContainsKey(username);
        }

        public static List<string> GetAllUsernames()
        {
            return clsDataManager.UsersDB.Keys.Select(k => k).ToList();
        }
    }
}
