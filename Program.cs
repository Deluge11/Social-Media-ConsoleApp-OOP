using SocialApp;
using SocialApp.Pages;
using SocialApp.Scripts;
using SocialApp.Services;
using SocialApp.Controllers;


AppState appState = new AppState();

DataManager dataManager = new DataManager();

PostServices postServices = new PostServices(dataManager);
UserServices userServices = new UserServices(dataManager);
FriendServices friendServices = new FriendServices(dataManager);
MessageServices messageServices = new MessageServices(dataManager);
AuthenticationServices authenticationServices = new AuthenticationServices(appState, userServices);

About aboutPage = new About();
HomePage homePage = new HomePage();
PostsPage postPage = new PostsPage();
FriendsPage friendPage = new FriendsPage();
AuthenticatePage authenticationPage = new AuthenticatePage();
MyPostsPage myPostsPage = new MyPostsPage(appState, postServices);
NewPostsPage newPostsPage = new NewPostsPage(appState, postServices);
MyFriendsPage myFriendsPage = new MyFriendsPage(appState, friendServices);
ChatPage chatPage = new ChatPage(appState, friendServices, messageServices);
ProfilePage profilePage = new ProfilePage(appState, friendServices, postServices);
FriendRequestsPage friendRequestPage = new FriendRequestsPage(appState, friendServices);
SendFriendRequestPage sendFriendRequestPage = new SendFriendRequestPage(appState, friendServices);

LoginAction loginAction = new LoginAction(authenticationServices);
RegisterAction registerAction = new RegisterAction(authenticationServices);

homePage.AddPage(profilePage);
homePage.AddPage(postPage);
homePage.AddPage(friendPage);
homePage.AddPage(chatPage);
homePage.AddPage(aboutPage);

postPage.AddPage(myPostsPage);
postPage.AddPage(newPostsPage);

friendPage.AddPage(myFriendsPage);
friendPage.AddPage(sendFriendRequestPage);
friendPage.AddPage(friendRequestPage);

authenticationPage.AddAction(loginAction);
authenticationPage.AddAction(registerAction);

NavigationController navigationController = new NavigationController(appState);
RendererController renderController = new RendererController(appState, navigationController);
InputController inputController = new InputController(appState, navigationController,authenticationServices);
PageController pageController = new PageController(navigationController, renderController, inputController);

navigationController.SetDefaultAppPage(homePage);
navigationController.SetDefaultAuthPage(authenticationPage);

pageController.Play();

dataManager.PushData();