using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsPostServices
    {
        protected Dictionary<string, clsUser> UsersDB { get; }
        protected Dictionary<int, clsPost> PostsDB { get; }
        protected clsLastIdInfo LastIdInfo { get; }

        public clsPostServices(clsDataManager dataManager)
        {
            UsersDB = dataManager.UsersDB;
            PostsDB = dataManager.PostsDB;
            LastIdInfo = dataManager.LastIdInfo;
        }


        public int GetMyPostsCount(string username)
        {
            return UsersDB.ContainsKey(username) ? UsersDB[username].PostsId.Count : 0;
        }

        public List<clsPost> GetAllPosts()
        {
            return PostsDB.Values.ToList();
        }

        public List<clsPost> GetUserPosts(string username)
        {
            List<clsPost> result = new();

            if (!UsersDB.ContainsKey(username))
            {
                return result;
            }

            clsUser user = UsersDB[username];

            foreach (int pId in user.PostsId)
            {
                result.Add(PostsDB[pId]);
            }

            return result;
        }

        public void AddNewPost(string username, string post)
        {
            clsPost newPost = new clsPost(++LastIdInfo.PostID, post, username, DateTime.Now);
            PostsDB[newPost.Id] = newPost;
            UsersDB[username].AddPost(newPost.Id);
        }

        public void TogglePostLike(string username, int postId)
        {
            if (PostsDB.ContainsKey(postId))
                PostsDB[postId].Like(username);
        }

        public int GetPostsTotalLikes(string username)
        {
            List<clsPost> userPosts = GetUserPosts(username);
            return userPosts.Sum(p => p.Likes.Count);
        }

        public List<clsPost> GetNewPosts(string username)
        {
            if (!UsersDB.ContainsKey(username))
                return [];

            PriorityQueue<LinkedListNode<int>, int> posts = new();

            LinkedListNode<int> firstPost = UsersDB[username].PostsId.First;

            if (firstPost != null)
            {
                posts.Enqueue(firstPost, -PostsDB[firstPost.Value].Id);
            }

            foreach (string friend in UsersDB[username].Friends)
            {
                firstPost = UsersDB[friend].PostsId.First;

                if (firstPost != null)
                {
                    posts.Enqueue(firstPost, -PostsDB[firstPost.Value].Id);
                }
            }

            List<clsPost> result = new();

            while (posts.Count > 0 && result.Count < 10)
            {
                LinkedListNode<int> post = posts.Dequeue();

                result.Add(PostsDB[post.Value]);

                post = post.Next;

                if (post != null) posts.Enqueue(post, -PostsDB[post.Value].Id);
            }
            return result;
        }
    }
}
