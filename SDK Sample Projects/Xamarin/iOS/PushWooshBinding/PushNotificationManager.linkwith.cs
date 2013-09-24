using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("PushNotificationManager.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true, Frameworks = "AdSupport CoreGraphics CoreFoundation")]
