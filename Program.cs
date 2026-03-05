using SocialApp;
using SocialApp.Pages;
using SocialApp.Scripts;
using SocialApp.Controllers;
using SocialApp.Actions;

clsAppState appState = new clsAppState();
clsDataManager dataManager = new clsDataManager();
clsServiceCollection serviceCollection = new clsServiceCollection(dataManager, appState);

clsHomePage homePage = new clsHomePage();
clsPostsPage postPage = new clsPostsPage();
clsAboutPage aboutPage = new clsAboutPage();
clsFriendsPage friendPage = new clsFriendsPage();
clsAuthenticatePage authenticationPage = new clsAuthenticatePage();
clsChatListPage chatPage = new clsChatListPage(appState, serviceCollection);
clsMyPostsPage myPostsPage = new clsMyPostsPage(appState, serviceCollection);
clsProfilePage profilePage = new clsProfilePage(appState, serviceCollection);
clsNewPostsPage newPostsPage = new clsNewPostsPage(appState, serviceCollection);
clsMyFriendsPage myFriendsPage = new clsMyFriendsPage(appState, serviceCollection);
clsFriendRequestsPage friendRequestPage = new clsFriendRequestsPage(appState, serviceCollection);
clsSendFriendRequestPage sendFriendRequestPage = new clsSendFriendRequestPage(appState, serviceCollection);

clsLoginAction loginAction = new clsLoginAction(serviceCollection);
clsRegisterAction registerAction = new clsRegisterAction(serviceCollection);
clsVisitAsGuestAction visitAsGuestAction = new clsVisitAsGuestAction(serviceCollection);

homePage.AddSubPage(profilePage);
homePage.AddSubPage(postPage);
homePage.AddSubPage(friendPage);
homePage.AddSubPage(chatPage);
homePage.AddSubPage(aboutPage);

postPage.AddSubPage(myPostsPage);
postPage.AddSubPage(newPostsPage);

friendPage.AddSubPage(myFriendsPage);
friendPage.AddSubPage(sendFriendRequestPage);
friendPage.AddSubPage(friendRequestPage);

authenticationPage.AddAction(loginAction);
authenticationPage.AddAction(registerAction);
authenticationPage.AddAction(visitAsGuestAction);

clsNavigationController navigationController = new clsNavigationController(appState);
clsRendererController renderController = new clsRendererController(appState, navigationController);
clsInputController inputController = new clsInputController(appState, navigationController, serviceCollection);
clsPageController pageController = new clsPageController(navigationController, renderController, inputController);

navigationController.SetMainPage(homePage);
navigationController.SetAuthenticationPage(authenticationPage);

pageController.Start();

dataManager.PushDataToJsonFiles();