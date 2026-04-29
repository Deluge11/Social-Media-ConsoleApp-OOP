using SocialApp.Data;
using SocialApp.Model;


namespace SocialApp.Services
{
    public class clsPostServices
    {
        public static int GetMyPostsCount(string username)
        {
            return clsDataManager.UsersDB.ContainsKey(username) ? clsDataManager.UsersDB[username].PostsId.Count : 0;
        }

        public static List<clsPost> GetAllPosts()
        {
            return clsDataManager.PostsDB.Values.Reverse().ToList();
        }

        public static List<clsPost> GetUserPosts(string username)
        {
            List<clsPost> result = new();

            return clsDataManager.UsersDB.ContainsKey(username)
                ?
                clsDataManager.PostsDB.Values.Where(p => clsDataManager.UsersDB[username].PostsId.Contains(p.Id)).Reverse().ToList()
                :
                new List<clsPost>();
        }

        public static void AddNewPost(string username, string post)
        {
            clsPost newPost = new clsPost(++clsDataManager.LastIdInfo.PostID, post, username, DateTime.Now);
            clsDataManager.PostsDB[newPost.Id] = newPost;
            clsDataManager.UsersDB[username].AddPost(newPost.Id);
        }

        public static void TogglePostLike(string username, int postId)
        {
            if (clsDataManager.PostsDB.ContainsKey(postId))
                clsDataManager.PostsDB[postId].Like(username);
        }

        public static int GetPostsTotalLikes(string username)
        {
            List<clsPost> userPosts = GetUserPosts(username);
            return userPosts.Sum(p => p.Likes.Count);
        }

        public static List<clsPost> GetNewPosts(string username)
        {
            if (!clsDataManager.UsersDB.ContainsKey(username))
                return [];

            PriorityQueue<LinkedListNode<int>, int> posts = new();

            LinkedListNode<int> firstPost = clsDataManager.UsersDB[username].PostsId.First;

            if (firstPost != null)
            {
                posts.Enqueue(firstPost, -clsDataManager.PostsDB[firstPost.Value].Id);
            }

            foreach (string friend in clsDataManager.UsersDB[username].Friends)
            {
                firstPost = clsDataManager.UsersDB[friend].PostsId.First;

                if (firstPost != null)
                {
                    posts.Enqueue(firstPost, -clsDataManager.PostsDB[firstPost.Value].Id);
                }
            }

            List<clsPost> result = new();

            while (posts.Count > 0 && result.Count < 10)
            {
                LinkedListNode<int> post = posts.Dequeue();

                result.Add(clsDataManager.PostsDB[post.Value]);

                post = post.Next;

                if (post != null) posts.Enqueue(post, -clsDataManager.PostsDB[post.Value].Id);
            }
            return result;
        }
    }
}
