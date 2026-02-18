using SocialApp;
using SocialApp.Controllers;
using SocialApp.Interfaces;
using SocialApp.Pages;
using SocialApp.Scripts;
using SocialApp.Services;

AppState appState = new AppState();

DataManager dataManager = new DataManager();

FriendServices friendServices = new FriendServices(dataManager);
PostServices postServices = new PostServices(dataManager);
MessageServices messageServices = new MessageServices(dataManager);
UserServices userServices = new UserServices(dataManager);
AuthenticationServices authenticationServices = new AuthenticationServices(appState, userServices, dataManager.LastIdInfo);

AuthenticatePage authenticationPage = new AuthenticatePage();
HomePage homePage = new HomePage(appState);
ProfilePage profilePage = new ProfilePage(appState);
PostsPage postPage = new PostsPage(appState);
MyPostsPage myPostsPage = new MyPostsPage(appState, postServices);
NewPostsPage newPostsPage = new NewPostsPage(appState, postServices);
FriendsPage friendPage = new FriendsPage(appState);
MyFriendsPage myFriendsPage = new MyFriendsPage(appState, friendServices);
FriendRequestsPage friendRequestPage = new FriendRequestsPage(appState, friendServices);
SendFriendRequestPage sendFriendRequestPage = new SendFriendRequestPage(appState, friendServices);
ChatPage chatPage = new ChatPage(appState, friendServices, messageServices);

LoginAction loginAction = new LoginAction(authenticationServices);
RegisterAction registerAction = new RegisterAction(authenticationServices);

About aboutPage = new About();

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

INavigationController navigationController = new NavigationController(appState);
IInputController inputController = new InputController(navigationController);
IRendererController renderController = new RendererController(navigationController);

navigationController.SetDefaultAppPage(homePage);
navigationController.SetDefaultAuthPage(authenticationPage);

PageController pageController = new PageController(navigationController, renderController, inputController);

pageController.Play();

dataManager.PushData();