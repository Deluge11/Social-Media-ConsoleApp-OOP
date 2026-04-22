using SocialApp;
using SocialApp.Data;
using SocialApp.Pages;
using SocialApp.Scripts;
using SocialApp.Actions;
using SocialApp.Controllers;
using SocialApp.Forms;

clsAppState appState = new clsAppState();
clsDataManager dataManager = new clsDataManager();
clsServiceCollection serviceCollection = new clsServiceCollection(dataManager, appState);

clsPageCollector homePage = new clsPageCollector("Home Page");
clsPageCollector postPage = new clsPageCollector("Post Page");
clsPageCollector friendPage = new clsPageCollector("Friends Page");
clsActionCollectorPage authenticationPage = new clsActionCollectorPage("Authentication Page");

clsAboutPage aboutPage = new clsAboutPage();
clsChatListPage chatPage = new clsChatListPage(appState, serviceCollection);
clsMyPostsPage myPostsPage = new clsMyPostsPage(appState, serviceCollection);
clsProfilePage profilePage = new clsProfilePage(appState, serviceCollection);
clsGeneralPostsPage generalPostsPage = new clsGeneralPostsPage(serviceCollection);
clsMyFriendsPage myFriendsPage = new clsMyFriendsPage(appState, serviceCollection);
clsFriendRequestsPage friendRequestPage = new clsFriendRequestsPage(appState, serviceCollection);
clsConnectionPostsPage connectionPostsPage = new clsConnectionPostsPage(appState, serviceCollection);
clsSendFriendRequestPage sendFriendRequestPage = new clsSendFriendRequestPage(appState, serviceCollection);

clsLoginAction loginAction = new clsLoginAction(serviceCollection);
clsRegisterAction registerAction = new clsRegisterAction(serviceCollection);
clsVisitAsGuestAction visitAsGuestAction = new clsVisitAsGuestAction(serviceCollection);

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

clsNavigationController navigationController = new clsNavigationController(appState);
IForm form = new clsMainForm(appState, navigationController);
clsInputController inputController = new clsInputController(appState, navigationController, serviceCollection);
clsPageController pageController = new clsPageController(form, inputController, navigationController);

navigationController.SetMainPage(homePage);
navigationController.SetAuthenticationPage(authenticationPage);

pageController.Start();

dataManager.PushDataToJsonFiles();