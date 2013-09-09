var mode = Ti.App.Properties.getString('bg-location-mode', 'PWTrackingDisabled');

Ti.include('pushwoosh.js');

if (mode === 'PWTrackingDisabled') {
	Ti.App.currentService.stop();
} else {
	if(mode === 'PWTrackAccurateLocationChanges') {
		Ti.Geolocation.accuracy = Ti.Geolocation.ACCURACY_BEST;
		Ti.Geolocation.preferredProvider = Ti.Geolocation.PROVIDER_GPS;
	} else {
		Ti.Geolocation.accuracy = Ti.Geolocation.ACCURACY_THREE_KILOMETERS;
		Ti.Geolocation.preferredProvider = Ti.Geolocation.PROVIDER_NETWORK;
	}
	
	Ti.Geolocation.removeEventListener('location', PushWoosh.handleLocation);
	Ti.Geolocation.addEventListener('location', PushWoosh.handleLocation);
}
