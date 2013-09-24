using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;

namespace PushWooshBinding
{

	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     PointF Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_libraries
	//

	public delegate void LocationHandler (CLLocation location);
	public delegate void PushwooshGetTagsHandler (NSDictionary tags);
	public delegate void PushwooshErrorHandler (NSError error);

	[Model, BaseType (typeof (NSObject))]
	public partial interface HtmlWebViewControllerDelegate {

		[Export ("htmlWebViewControllerDidClose:")]
		void HtmlWebViewControllerDidClose (HtmlWebViewController viewController);
	}

	[BaseType (typeof (UIViewController))]
	public partial interface HtmlWebViewController  {

		[Export ("initWithURLString:")]
		IntPtr Constructor (string url);

		[Export ("delegate", ArgumentSemantic.Assign)]
		HtmlWebViewControllerDelegate Delegate { get; set; }

		[Export ("webview", ArgumentSemantic.Retain)]
		UIWebView Webview { get; set; }

		[Export ("activityIndicator", ArgumentSemantic.Retain)]
		UIActivityIndicatorView ActivityIndicator { get; set; }

		[Export ("supportedOrientations")]
		PWSupportedOrientations SupportedOrientations { get; set; }
	}
	
	[Model]
	public partial interface PushNotificationDelegate {

		[Export ("onDidRegisterForRemoteNotificationsWithDeviceToken:")]
		void  DidRegisterForRemoteNotificationsWithDeviceToken (string token);

		[Export ("onDidFailToRegisterForRemoteNotificationsWithError:")]
		void  DidFailToRegisterForRemoteNotificationsWithError (NSError error);

		[Export ("onPushReceived:withNotification:onStart:")]
		void PushReceivedWithNotificationOnStart (PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart);

		[Export ("onPushAccepted:withNotification:")]
		void PushAcceptedWithNotification (PushNotificationManager pushManager, NSDictionary pushNotification);

		[Export ("onPushAccepted:withNotification:onStart:")]
		void PushAcceptedWithNotificationOnStart (PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart);

		[Export ("onTagsReceived:")]
		void  TagsReceived (NSDictionary tags);

		[Export ("onTagsFailedToReceive:")]
		void  TagsFailedToReceive (NSError error);
	}


	[BaseType (typeof (NSObject))]
	public partial interface PWLocationTracker {

		[Export ("locationManager", ArgumentSemantic.Retain)]
		CLLocationManager LocationManager { get; set; }

		[Export ("enabled")]
		bool Enabled { get; set; }

		[Export ("backgroundMode", ArgumentSemantic.Copy)]
		string BackgroundMode { get; set; }

		[Export ("locationUpdatedInForeground", ArgumentSemantic.Copy)]
		LocationHandler LocationUpdatedInForeground { get; set; }

		[Export ("locationUpdatedInBackground", ArgumentSemantic.Copy)]
		LocationHandler LocationUpdatedInBackground { get; set; }
	}


	[BaseType (typeof (NSObject))]
	public partial interface PWTags {

		[Static, Export ("incrementalTagWithInteger:")]
		NSDictionary IncrementalTagWithInteger (int delta);
	}

	[BaseType (typeof (NSObject))]
	public partial interface PushNotificationManager {

		[Export ("appCode", ArgumentSemantic.Copy)]
		string AppCode { get; set; }

		[Export ("appName", ArgumentSemantic.Copy)]
		string AppName { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)]
		NSObject Delegate { get; set; }

		[Export ("richPushWindow", ArgumentSemantic.Retain)]
		UIWindow RichPushWindow { get; set; }

		[Export ("pushNotifications", ArgumentSemantic.Retain)]
		NSDictionary PushNotifications { get; set; }

		[Export ("supportedOrientations")]
		PWSupportedOrientations SupportedOrientations { get; set; }

		[Export ("locationTracker", ArgumentSemantic.Retain)]
		PWLocationTracker LocationTracker { get; set; }

		[Export ("showPushnotificationAlert")]
		bool ShowPushnotificationAlert { get; set; }

		[Static, Export ("initializeWithAppCode:appName:")]
		void InitializeWithAppCodeAndName (string appCode, string appName);

		[Static, Export ("pushManager")]
		PushNotificationManager PushManager { get; }

		[Static, Export ("getAPSProductionStatus")]
		bool GetAPSProductionStatus { get; }

		[Export ("initWithApplicationCode:appName:")]
		IntPtr Constructor (string appCode, string appName);

		[Export ("initWithApplicationCode:navController:appName:")]
		IntPtr Constructor (string appCode, UIViewController navController, string appName);

		[Export ("showWebView")]
		void ShowWebView ();

		[Export ("startLocationTracking")]
		void StartLocationTracking ();

		[Export ("startLocationTracking:")]
		void StartLocationTracking (string mode);

		[Export ("stopLocationTracking")]
		void StopLocationTracking ();

		[Export ("tags")]
		NSDictionary Tags { set; }

		[Export ("loadTags")]
		void LoadTags ();

		[Export ("loadTags:error:")]
		void LoadTags (PushwooshGetTagsHandler successHandler, PushwooshErrorHandler errorHandler);

		[Export ("sendAppOpen")]
		void SendAppOpen ();

		[Export ("sendBadges:")]
		void SendBadges (int badge);

		[Export ("sendLocation:")]
		void SendLocation (CLLocation location);

		[Export ("recordGoal:")]
		void RecordGoal (string goal);

		[Export ("recordGoal:withCount:")]
		void RecordGoal (string goal, NSNumber count);

		[Export ("getPushToken")]
		string GetPushToken { get; }

		[Export ("handlePushRegistration:")]
		void HandlePushRegistration (NSData devToken);

		[Export ("handlePushRegistrationString:")]
		void HandlePushRegistrationString (string deviceID);

		[Export ("handlePushRegistrationFailure:")]
		void HandlePushRegistrationFailure (NSError error);

		[Export ("handlePushReceived:")]
		bool HandlePushReceived (NSDictionary userInfo);

		[Export ("getApnPayload:")]
		NSDictionary GetApnPayload (NSDictionary pushNotification);

		[Export ("getCustomPushData:")]
		string GetCustomPushData (NSDictionary pushNotification);

		[Static, Export ("clearNotificationCenter")]
		void ClearNotificationCenter ();
	}
	
}

