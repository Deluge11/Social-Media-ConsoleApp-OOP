using SocialApp.Pages;
using SocialApp.Scripts;
using SocialApp.Actions;
using SocialApp.Controllers;
using SocialApp.Forms;
using SocialApp.Data;

clsPageCollector homePage = new clsPageCollector("Home Page");
clsPageCollector postPage = new clsPageCollector("Post Page");
clsPageCollector friendPage = new clsPageCollector("Friends Page");
clsActionCollectorPage authenticationPage = new clsActionCollectorPage("Authentication Page");

clsAboutPage aboutPage = new clsAboutPage();
clsChatListPage chatPage = new clsChatListPage();
clsMyPostsPage myPostsPage = new clsMyPostsPage();
clsProfilePage profilePage = new clsProfilePage();
clsMyFriendsPage myFriendsPage = new clsMyFriendsPage();
clsGeneralPostsPage generalPostsPage = new clsGeneralPostsPage();
clsFriendRequestsPage friendRequestPage = new clsFriendRequestsPage();
clsConnectionPostsPage connectionPostsPage = new clsConnectionPostsPage();
clsSendFriendRequestPage sendFriendRequestPage = new clsSendFriendRequestPage();

clsLoginAction loginAction = new clsLoginAction();
clsRegisterAction registerAction = new clsRegisterAction();
clsVisitAsGuestAction visitAsGuestAction = new clsVisitAsGuestAction();

homePage.AddSubPage(new PageWithAccessPermission());
homePage.AddSubPage(new PageWithActionPermission());
homePage.AddSubPage(profilePage);
homePage.AddSubPage(postPage);
homePage.AddSubPage(friendPage);
homePage.AddSubPage(chatPage);
homePage.AddSubPage(aboutPage);

postPage.AddSubPage(generalPostsPage);
postPage.AddSubPage(myPostsPage);
postPage.AddSubPage(connectionPostsPage);

friendPage.AddSubPage(myFriendsPage);
friendPage.AddSubPage(sendFriendRequestPage);
friendPage.AddSubPage(friendRequestPage);

authenticationPage.AddAction(loginAction);
authenticationPage.AddAction(registerAction);
authenticationPage.AddAction(visitAsGuestAction);

clsNavigationController navigationController = new clsNavigationController();
clsInputController inputController = new clsInputController(navigationController);
clsPageController pageController = new clsPageController(new clsMainForm(navigationController), inputController, navigationController);

navigationController.SetMainPage(homePage);
navigationController.SetAuthenticationPage(authenticationPage);

clsDataManager.LoadDataFromJsonFiles();

pageController.Start();