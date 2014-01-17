package com.prakass.activity;

import java.util.List;

import android.app.ListActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Toast;

import com.prakass.R;
import com.prakass.dao.ContactDAO;
import com.prakass.domain.Contact;

public class ViewAllContacts extends ListActivity {
    ContactDAO contactDAO;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
	super.onCreate(savedInstanceState);
	setContentView(R.layout.activity_view_all_contacts);

	contactDAO = new ContactDAO(this);
	contactDAO.open();

	List<Contact> list = contactDAO.getAllContacts();

	// Use the SimpleCursorAdapter to show the
	// elements in a ListView
	ArrayAdapter<Contact> adapter = new ArrayAdapter<Contact>(this,
		android.R.layout.simple_list_item_1, list);
	setListAdapter(adapter);
    }

    public void deleteFirst(View view) {
	@SuppressWarnings("unchecked")
	ArrayAdapter<Contact> adapter = (ArrayAdapter<Contact>) getListAdapter();
	Contact contact = null;
	if (getListAdapter().getCount() > 0) {
	    contact = (Contact) getListAdapter().getItem(0);
	    contactDAO.deleteContact(contact);
	    adapter.remove(contact);
	    Toast.makeText(this,"Removed contact of "+contact.getFirstName() +" " + contact.getLastName(), Toast.LENGTH_SHORT).show();
	}
	adapter.notifyDataSetChanged();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
	// Inflate the menu; this adds items to the action bar if it is present.
	getMenuInflater().inflate(R.menu.view_all_contacts, menu);
	return true;
    }

    @Override
    protected void onResume() {
	contactDAO.open();
	super.onResume();
    }

    @Override
    protected void onPause() {
	contactDAO.close();
	super.onPause();
    }

}
