package com.prakass.activity;

import android.app.Activity;
import android.app.Dialog;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.Messenger;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.Toast;

import com.prakass.R;
import com.prakass.service.DownloadService;

public class MainActivity extends Activity {

    private Handler handler = new Handler() {
	public void handleMessage(Message message) {
	    Object path = message.obj;
	    if (message.arg1 == RESULT_OK && path != null) {
		Toast.makeText(MainActivity.this,
			"Downloaded" + path.toString(), Toast.LENGTH_LONG)
			.show();
	    } else {
		Toast.makeText(MainActivity.this, "Download failed.",
			Toast.LENGTH_LONG).show();
	    }
	};
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
	super.onCreate(savedInstanceState);
	setContentView(R.layout.activity_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
	getMenuInflater().inflate(R.menu.main, menu);
	return true;
    }

    public void addContact(View view) {
	Intent intent = new Intent(this, AddContactActivity.class);
	startActivity(intent);
    }

    public void viewContacts(View view) {
	Intent intent = new Intent(this, ViewAllContacts.class);
	startActivity(intent);
    }

    public void activateDownloadService(View view) {
	Intent intent = new Intent(this, DownloadService.class);
	// Create a new Messenger for the communication back
	Messenger messenger = new Messenger(handler);
	intent.putExtra("MESSENGER", messenger);
	intent.setData(Uri
		.parse("http://developer.android.com/develop/index.html"));
	intent.putExtra("urlpath",
		"http://developer.android.com/develop/index.html");
	startService(intent);
    }

    public void displayAboutUs(View view) {
	final Dialog dialog = new Dialog(this);
	dialog.setContentView(R.layout.dialog);
	dialog.setTitle("About This Application");

	Button dialogButton = (Button) dialog.findViewById(R.id.button_ok);
	dialogButton.setOnClickListener(new OnClickListener() {
	    @Override
	    public void onClick(View v) {
		dialog.dismiss();
	    }
	});
	dialog.show();
    }

}
