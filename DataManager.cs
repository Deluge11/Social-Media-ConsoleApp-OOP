using SocialApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp
{
    public class DataManager
    {
        private string MassageFile = "massage.json";
        private string UsersFile = "Users.json";
        private string IdsFile = "LastId.json";
        private string PostsFile = "Posts.json";

        public Dictionary<string, User> UsersDB { get; private set; } = new();
        public Dictionary<int, Post> PostsDB { get; private set; } = new();
        public Dictionary<int, Messages> MessagesDB { get; private set; } = new();
        public LastIdInfo LastIdInfo { get; private set; } = new();


        public DataManager()
        {
            PullData();
        }


        public void PushData()
        {
            string IdsJsonString = JsonConvert.SerializeObject(LastIdInfo, Formatting.Indented);
            File.WriteAllText(IdsFile, IdsJsonString);

            string UsersJsonString = JsonConvert.SerializeObject(UsersDB, Formatting.Indented);
            File.WriteAllText(UsersFile, UsersJsonString);

            string MassagesJsonString = JsonConvert.SerializeObject(MessagesDB, Formatting.Indented);
            File.WriteAllText(MassageFile, MassagesJsonString);

            string PostsJsonString = JsonConvert.SerializeObject(PostsDB, Formatting.Indented);
            File.WriteAllText(PostsFile, PostsJsonString);
        }
        private void PullData()
        {
            if (File.Exists(UsersFile))
            {
                string json = File.ReadAllText(UsersFile);
                UsersDB = JsonConvert.DeserializeObject<Dictionary<string, User>>(json) ?? new();
            }

            if (File.Exists(IdsFile))
            {
                string json = File.ReadAllText(IdsFile);
                LastIdInfo = JsonConvert.DeserializeObject<LastIdInfo>(json) ?? new();
            }
            if (File.Exists(MassageFile))
            {
                string json = File.ReadAllText(MassageFile);
                MessagesDB = JsonConvert.DeserializeObject<Dictionary<int, Messages>>(json) ?? new();
            }
            if (File.Exists(PostsFile))
            {
                string json = File.ReadAllText(PostsFile);
                PostsDB = JsonConvert.DeserializeObject<Dictionary<int, Post>>(json) ?? new();
            }
        }





    }
}
