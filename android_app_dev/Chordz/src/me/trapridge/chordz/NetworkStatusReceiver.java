package me.trapridge.chordz;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;
import android.widget.Toast;

public class NetworkStatusReceiver extends BroadcastReceiver {

	private final String LOG_TAG = getClass().getSimpleName();
	
	@Override
	public void onReceive(Context context, Intent intent) {
		Log.i(LOG_TAG, "Received network status broadcast");
		Toast changed = Toast.makeText(context, "Network connectivity changed", Toast.LENGTH_SHORT);
		changed.show();
	}

}
