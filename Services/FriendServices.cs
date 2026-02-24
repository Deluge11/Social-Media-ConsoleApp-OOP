using SocialApp.Model;


namespace SocialApp.Services
{
   public class FriendServices
    {
        private Dictionary<string, User> UsersDB { get; }
        private Dictionary<int, Chat> MessagesDB { get; }
        private LastIdInfo LastIdInfo { get; }

        public FriendServices(DataManager dataManager)
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
        public List<string> GetUnfriendsUsers(string username)
        {
            List<string> result = new();

            foreach (var otherUser in UsersDB.Values)
            {
                if (otherUser.Name == username) continue;

                if (CanSendRequestToThisUser(username, otherUser.Name))
                {
                    result.Add(otherUser.Name);
                }
            }

            return result;
        }

        public bool CanSendRequestToThisUser(string user1, string user2)
        {
            if (UsersDB[user1].Friends.Contains(user2))
                return false;
            if (UsersDB[user1].FriendRequests.Contains(user2))
                return false;
            if (UsersDB[user2].FriendRequests.Contains(user1))
                return false;

            return true;
        }

        public void ConnectUsers(string user1, string user2)
        {
            UsersDB[user1].RemoveFriendRequest(user2);
            UsersDB[user1].AddFriend(user2);
            UsersDB[user2].AddFriend(user1);

            int chatId = LastIdInfo.ChatID++;

            MessagesDB[chatId] = new Chat(chatId);
            UsersDB[user1].AddChat(chatId);
            UsersDB[user2].AddChat(chatId);
        }

        public void AddFreindRequest(string username, string otherUsername)
        {
            UsersDB[otherUsername].AddFriendRequest(username);
        }
    }
}
