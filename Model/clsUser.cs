

namespace SocialApp.Model
{
    public class clsUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public LinkedList<int> PostsId { get; set; }
        public HashSet<string> Friends { get; set; }
        public HashSet<string> FriendRequests { get; set; }
        public HashSet<int> ChatID { get; set; }
        public int Permissions { get; set; }

        public clsUser(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
            PostsId = new();
            Friends = new();
            FriendRequests = new();
            ChatID = new();
        }

        public void AddPost(int postId)
        {
            PostsId.AddFirst(postId);
        }
        public void AddFriendRequest(string username)
        {
            FriendRequests.Add(username);
        }
        public void RemoveFriendRequest(string username)
        {
            FriendRequests.Remove(username);
        }
        public void AddFriend(string username)
        {
            Friends.Add(username);
        }
        public void AddChat(int chatID)
        {
            ChatID.Add(chatID);
        }

    }
}
