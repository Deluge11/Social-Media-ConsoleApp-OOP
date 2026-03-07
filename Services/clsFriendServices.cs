using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsFriendServices
    {
        protected Dictionary<string, clsUser> UsersDB { get; }
        protected Dictionary<int, clsChat> MessagesDB { get; }
        protected clsLastIdInfo LastIdInfo { get; }

        public clsFriendServices(clsDataManager dataManager)
        {
            UsersDB = dataManager.UsersDB;
            MessagesDB = dataManager.MessagesDB;
            LastIdInfo = dataManager.LastIdInfo;
        }


        public List<string> GetUserFriends(string username)
        {
            List<string> result = new();

            foreach (var friend in UsersDB[username].Friends)
            {
                result.Add(friend);
            }

            return result;
        }

        public int GetUserFriendsCount(string username)
        {
            return UsersDB[username].Friends.Count;
        }

        public List<string> GetFriendRequestsUsers(string username)
        {
            List<string> result = new();
            foreach (var otherUser in UsersDB[username].FriendRequests)
            {
                if (UsersDB[username].FriendRequests.Contains(otherUser))
                {
                    result.Add(otherUser);
                }
            }
            return result;
        }

        public List<string> GetUserWhoCanSendFriendRequest(string username)
        {
            List<string> result = new();

            foreach (var otherUser in UsersDB.Values)
            {
                if (otherUser.Name == username) continue;

                if (CanSendRequestBetweenUsers(username, otherUser.Name))
                {
                    result.Add(otherUser.Name);
                }
            }

            return result;
        }

        public void ConnectUsers(string user1, string user2)
        {
            UsersDB[user1].RemoveFriendRequest(user2);
            UsersDB[user1].AddFriend(user2);
            UsersDB[user2].AddFriend(user1);

            int chatId = LastIdInfo.ChatID++;

            MessagesDB[chatId] = new clsChat(chatId);
            UsersDB[user1].AddChat(chatId);
            UsersDB[user2].AddChat(chatId);
        }

        public void AddFriendRequest(string username, string otherUsername)
        {
            if (CanSendRequestBetweenUsers(username, otherUsername))
                UsersDB[otherUsername].AddFriendRequest(username);
        }

        private bool CanSendRequestBetweenUsers(string user1, string user2)
        {
            if (UsersDB[user1].Friends.Contains(user2))
                return false;
            if (UsersDB[user1].FriendRequests.Contains(user2))
                return false;
            if (UsersDB[user2].FriendRequests.Contains(user1))
                return false;

            return true;
        }
    }
}
