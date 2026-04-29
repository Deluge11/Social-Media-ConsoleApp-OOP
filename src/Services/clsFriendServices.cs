using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsFriendServices
    {
        public static List<string> GetUserFriends(string username)
        {
            List<string> result = new();

            foreach (var friend in clsDataManager.UsersDB[username].Friends)
            {
                result.Add(friend);
            }

            return result;
        }

        public static int GetUserFriendsCount(string username)
        {
            return clsDataManager.UsersDB[username].Friends.Count;
        }

        public static List<string> GetFriendRequestsUsers(string username)
        {
            List<string> result = new();
            foreach (var otherUser in clsDataManager.UsersDB[username].FriendRequests)
            {
                if (clsDataManager.UsersDB[username].FriendRequests.Contains(otherUser))
                {
                    result.Add(otherUser);
                }
            }
            return result;
        }

        public static List<string> GetUsersWhoCanSendFriendRequest(string username)
        {
            List<string> result = new();

            foreach (var otherUser in clsDataManager.UsersDB.Values)
            {
                if (otherUser.Name == username) continue;

                if (CanSendRequestBetweenUsers(username, otherUser.Name))
                {
                    result.Add(otherUser.Name);
                }
            }

            return result;
        }

        public static void ConnectUsers(string user1, string user2)
        {
            clsDataManager.UsersDB[user1].RemoveFriendRequest(user2);
            clsDataManager.UsersDB[user1].AddFriend(user2);
            clsDataManager.UsersDB[user2].AddFriend(user1);

            int chatId = clsDataManager.LastIdInfo.ChatID++;

            clsDataManager.MessagesDB[chatId] = new clsChat(chatId);
            clsDataManager.UsersDB[user1].AddChat(chatId);
            clsDataManager.UsersDB[user2].AddChat(chatId);
        }

        public static bool AddFriendRequest(string username, string otherUsername)
        {
            if (CanSendRequestBetweenUsers(username, otherUsername))
            {
                clsDataManager.UsersDB[otherUsername].AddFriendRequest(username);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CanSendRequestBetweenUsers(string user1, string user2)
        {
            if (clsDataManager.UsersDB[user1].Friends.Contains(user2))
                return false;
            if (clsDataManager.UsersDB[user1].FriendRequests.Contains(user2))
                return false;
            if (clsDataManager.UsersDB[user2].FriendRequests.Contains(user1))
                return false;

            return true;
        }
    }
}
