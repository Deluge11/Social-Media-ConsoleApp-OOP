using SocialApp.Model;
using Newtonsoft.Json;


namespace SocialApp
{
    public class clsDataManager
    {
        private const string MessageFile = "Message.json";
        private const string UsersFile = "Users.json";
        private const string IdsFile = "LastId.json";
        private const string PostsFile = "Posts.json";

        public Dictionary<string, clsUser> UsersDB { get; private set; } = new();
        public Dictionary<int, clsPost> PostsDB { get; private set; } = new();
        public Dictionary<int, clsChat> MessagesDB { get; private set; } = new();
        public clsLastIdInfo LastIdInfo { get; private set; } = new();


        public clsDataManager()
        {
            LoadDataFromJsonFiles();
        }


        public void PushDataToJsonFiles()
        {
            string IdsJsonString = JsonConvert.SerializeObject(LastIdInfo, Formatting.Indented);
            File.WriteAllText(IdsFile, IdsJsonString);

            string UsersJsonString = JsonConvert.SerializeObject(UsersDB, Formatting.Indented);
            File.WriteAllText(UsersFile, UsersJsonString);

            string MessagesJsonString = JsonConvert.SerializeObject(MessagesDB, Formatting.Indented);
            File.WriteAllText(MessageFile, MessagesJsonString);

            string PostsJsonString = JsonConvert.SerializeObject(PostsDB, Formatting.Indented);
            File.WriteAllText(PostsFile, PostsJsonString);
        }
        private void LoadDataFromJsonFiles()
        {
            if (File.Exists(UsersFile))
            {
                string json = File.ReadAllText(UsersFile);
                UsersDB = JsonConvert.DeserializeObject<Dictionary<string, clsUser>>(json) ?? new();
            }
            if (File.Exists(IdsFile))
            {
                string json = File.ReadAllText(IdsFile);
                LastIdInfo = JsonConvert.DeserializeObject<clsLastIdInfo>(json) ?? new();
            }
            if (File.Exists(MessageFile))
            {
                string json = File.ReadAllText(MessageFile);
                MessagesDB = JsonConvert.DeserializeObject<Dictionary<int, clsChat>>(json) ?? new();
            }
            if (File.Exists(PostsFile))
            {
                string json = File.ReadAllText(PostsFile);
                PostsDB = JsonConvert.DeserializeObject<Dictionary<int, clsPost>>(json) ?? new();
            }
        }





    }
}
