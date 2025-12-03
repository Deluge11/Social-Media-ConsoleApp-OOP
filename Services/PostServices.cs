using SocialApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Services
{
    public class PostServices
    {
        public Dictionary<string, User> UsersDB { get; }
        public Dictionary<int, Post> PostsDB { get; }
        public LastIdInfo LastIdInfo { get; }

        public PostServices(DataManager dataManager)
        {
            UsersDB = dataManager.UsersDB;
            PostsDB = dataManager.PostsDB;
            LastIdInfo = dataManager.LastIdInfo;
        }
        public int GetMyPostsCount(string username)
        {
            return UsersDB[username].PostsId.Count;
        }

        public List<Post> GetUserPosts(string username)
        {
            List<Post> result = new();

            var user = UsersDB[username];

            foreach (var pId in user.PostsId)
            {
                result.Add(PostsDB[pId]);
            }

            return result;
        }

        public void AddNewPost(string username, string content)
        {
            LastIdInfo.PostID++;
            Post newPost = new Post(LastIdInfo.PostID, content, username, DateTime.Now);
            PostsDB[newPost.Id] = newPost;
            UsersDB[username].AddPost(newPost.Id);
        }

        public void TogglePostLike(string username, int postId)
        {
            PostsDB[postId].Like(username);
        }

        public List<Post> GetNewPosts(string username)
        {
            PriorityQueue<LinkedListNode<int>, int> posts = new();

            var firstPost = UsersDB[username].PostsId.First;

            if (firstPost != null)
            {
                posts.Enqueue(firstPost, -PostsDB[firstPost.Value].Id);
            }

            foreach (var friend in UsersDB[username].Friends)
            {
                firstPost = UsersDB[friend].PostsId.First;

                if (firstPost != null)
                {
                    posts.Enqueue(firstPost, -PostsDB[firstPost.Value].Id);
                }
            }

            List<Post> result = new();

            while (posts.Count > 0 && result.Count < 10)
            {
                var post = posts.Dequeue();

                result.Add(PostsDB[post.Value]);

                post = post.Next;

                if (post != null) posts.Enqueue(post, -PostsDB[post.Value].Id);
            }
            return result;
        }
    }
}
