using SocialApp;
using SocialApp.Controllers;
using SocialApp.Pages;
using SocialApp.Scripts;
using SocialApp.Services;

var appState = new AppState();
var dataManager = new DataManager();

//Declare Services
var friendServices = new FriendServices(dataManager);
var postServices = new PostServices(dataManager);
var messageServices = new MessageServices(dataManager);
var userServices = new UserServices(dataManager);
var authenticationServices = new AuthenticationServices(appState, userServices, dataManager.LastIdInfo);

//Declare Pages
var authenticationPage = new AuthenticatePage();
var homePage = new HomePage(appState);
var profilePage = new ProfilePage(appState);
var postPage = new PostsPage(appState);
var myPostsPage = new MyPostsPage(appState, postServices);
var newPostsPage = new NewPostsPage(appState, postServices);
var friendPage = new FriendsPage(appState);
var myfriendPage = new MyFriendsPage(appState, friendServices);
var friendRequestPage = new FriendRequestsPage(appState, friendServices);
var sendFriendRequestPage = new SendFriendRequestPage(appState, friendServices);
var chatPage = new ChatPage(appState, friendServices, messageServices);

//Declare Actions
var loginAction = new LoginAction(authenticationServices);
var registerAction = new RegisterAction(authenticationServices);

//Setup Pages
authenticationPage.AddAction(loginAction);
authenticationPage.AddAction(registerAction);
//============================================//
homePage.AddPage(profilePage);
homePage.AddPage(postPage);
homePage.AddPage(friendPage);
homePage.AddPage(chatPage);
//============================================//
postPage.AddPage(myPostsPage);
postPage.AddPage(newPostsPage);
//============================================//
friendPage.AddPage(myfriendPage);
friendPage.AddPage(sendFriendRequestPage);
friendPage.AddPage(friendRequestPage);
//============================================//

var navigationController = new NavigationController(appState);
var inputController = new InputController(navigationController);
var renderController = new RendererController(navigationController);

navigationController.SetDefaultAppPage(homePage);
navigationController.SetDefaultAuthPage(authenticationPage);

var pageController = new PageConttroller(navigationController, renderController, inputController);
pageController.Play();

dataManager.PushData();