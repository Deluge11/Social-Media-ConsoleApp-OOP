//using SocialApp;
//using SocialApp.Data;
//using SocialApp.Pages;
//using SocialApp.Scripts;
//using SocialApp.Actions;
//using SocialApp.Controllers;

//clsAppState appState = new clsAppState();
//clsDataManager dataManager = new clsDataManager();
//clsServiceCollection serviceCollection = new clsServiceCollection(dataManager, appState);

//clsHomePage homePage = new clsHomePage();
//clsPostsPage postPage = new clsPostsPage();
//clsAboutPage aboutPage = new clsAboutPage();
//clsFriendsPage friendPage = new clsFriendsPage();
//clsAuthenticatePage authenticationPage = new clsAuthenticatePage();
//clsChatListPage chatPage = new clsChatListPage(appState, serviceCollection);
//clsMyPostsPage myPostsPage = new clsMyPostsPage(appState, serviceCollection);
//clsProfilePage profilePage = new clsProfilePage(appState, serviceCollection);
//clsGeneralPostsPage generalPostsPage = new clsGeneralPostsPage(serviceCollection);
//clsMyFriendsPage myFriendsPage = new clsMyFriendsPage(appState, serviceCollection);
//clsFriendRequestsPage friendRequestPage = new clsFriendRequestsPage(appState, serviceCollection);
//clsConnectionPostsPage connectionPostsPage = new clsConnectionPostsPage(appState, serviceCollection);
//clsSendFriendRequestPage sendFriendRequestPage = new clsSendFriendRequestPage(appState, serviceCollection);

//clsLoginAction loginAction = new clsLoginAction(serviceCollection);
//clsRegisterAction registerAction = new clsRegisterAction(serviceCollection);
//clsVisitAsGuestAction visitAsGuestAction = new clsVisitAsGuestAction(serviceCollection);

//TestPage page = new TestPage();
//homePage.AddSubPage(page);

//homePage.AddSubPage(profilePage);
//homePage.AddSubPage(postPage);
//homePage.AddSubPage(friendPage);
//homePage.AddSubPage(chatPage);
//homePage.AddSubPage(aboutPage);

//postPage.AddSubPage(generalPostsPage);
//postPage.AddSubPage(myPostsPage);
//postPage.AddSubPage(connectionPostsPage);

//friendPage.AddSubPage(myFriendsPage);
//friendPage.AddSubPage(sendFriendRequestPage);
//friendPage.AddSubPage(friendRequestPage);

//authenticationPage.AddAction(loginAction);
//authenticationPage.AddAction(registerAction);
//authenticationPage.AddAction(visitAsGuestAction);


//clsNavigationController navigationController = new clsNavigationController(appState);
//clsRendererController renderController = new clsRendererController(appState, navigationController);
//clsInputController inputController = new clsInputController(appState, navigationController, serviceCollection);
//clsPageController pageController = new clsPageController(navigationController, renderController, inputController);

//navigationController.SetMainPage(homePage);
//navigationController.SetAuthenticationPage(authenticationPage);

//pageController.Start();

//dataManager.PushDataToJsonFiles();


using SocialApp;
using SocialApp.Grids;

clsTestGrid grid = new clsTestGrid();

grid.Content = $"S C R O L L {clsCustomTags.InvisibleChar} B A R";


grid.ResetContent();

grid.Print();

clsScrollBar scrollBard = new clsScrollBar();


scrollBard.ResetContent();


scrollBard.Print();