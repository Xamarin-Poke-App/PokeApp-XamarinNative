using System;
using Foundation;
using PokeAppiOS;
using UIKit;

namespace PokeAppiOS {
	[Register ("SceneDelegate")]
	public class SceneDelegate : UIResponder, IUIWindowSceneDelegate {

		[Export ("window")]
		public UIWindow Window { get; set; }

        public static SceneDelegate Current { get; private set; }


        [Export ("scene:willConnectToSession:options:")]
		public void WillConnect (UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
		{
			Current = this;

            UIWindowScene myScene = scene as UIWindowScene;
            Window = new UIWindow(myScene);
            LoadInitialViewController();
		}

		[Export ("sceneDidDisconnect:")]
		public void DidDisconnect (UIScene scene)
		{
			// Called as the scene is being released by the system.
			// This occurs shortly after the scene enters the background, or when its session is discarded.
			// Release any resources associated with this scene that can be re-created the next time the scene connects.
			// The scene may re-connect later, as its session was not neccessarily discarded (see UIApplicationDelegate `DidDiscardSceneSessions` instead).
		}

		[Export ("sceneDidBecomeActive:")]
		public void DidBecomeActive (UIScene scene)
		{
			// Called when the scene has moved from an inactive state to an active state.
			// Use this method to restart any tasks that were paused (or not yet started) when the scene was inactive.
		}

		[Export ("sceneWillResignActive:")]
		public void WillResignActive (UIScene scene)
		{
			// Called when the scene will move from an active state to an inactive state.
			// This may occur due to temporary interruptions (ex. an incoming phone call).
		}

		[Export ("sceneWillEnterForeground:")]
		public void WillEnterForeground (UIScene scene)
		{
			// Called as the scene transitions from the background to the foreground.
			// Use this method to undo the changes made on entering the background.
		}

		[Export ("sceneDidEnterBackground:")]
		public void DidEnterBackground (UIScene scene)
		{
			// Called as the scene transitions from the foreground to the background.
			// Use this method to save data, release shared resources, and store enough scene-specific state information
			// to restore the scene back to its current state.
		}

		private void LoadInitialViewController()
		{
            bool isLoggedIn = false;
            if (isLoggedIn)
            {
				SegueToHome();   
            }
			else
			{
				SegueToLogin();
			}
        }

		public void SegueToHome()
		{
			// For demo purposes initializes 'SecondViewController' as UIViewControler
			// but could be replaced by storyboard
            UIViewController viewController = new SecondViewController();
            UINavigationController navigationMain = new UINavigationController(viewController);
            Window.RootViewController = navigationMain;
            Window.MakeKeyAndVisible();
        }

		private void SegueToLogin()
		{
			var mainStoryboard = UIStoryboard.FromName("Main", null).InstantiateInitialViewController() as UIViewController;
            Window.RootViewController = mainStoryboard;
            Window.MakeKeyAndVisible();
        }

		public void Logout()
		{
			// clean storage
			SegueToLogin();
		}
	}
}

