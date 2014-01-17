package com.prakass.activity;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.prakass.R;
import com.prakass.dao.ContactDAO;
import com.prakass.domain.Contact;

public class AddContactActivity extends Activity {
    EditText eText;
    ContactDAO contactDAO;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
	super.onCreate(savedInstanceState);
	setContentView(R.layout.activity_add_contact);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
	getMenuInflater().inflate(R.menu.add_contact, menu);
	return true;
    }
    
    public void saveContact(View view){
	Contact c = new Contact();
	
	 eText = (EditText)findViewById(R.id.editText1);
	 c.setFirstName(eText.getText().toString());
	 
	 eText = (EditText)findViewById(R.id.editText2);
	 c.setLastName(eText.getText().toString());
	 
	 eText = (EditText)findViewById(R.id.editText3);
	 c.setPhoneNumber(eText.getText().toString());
	 
	 eText = (EditText)findViewById(R.id.editText4);
	 c.setEmail(eText.getText().toString());
	 
	 contactDAO = new ContactDAO(this);
	 contactDAO.open();
	 
	 String message = c.validate();
	 
	 if(message.equalsIgnoreCase("OK")){
	     Contact c1 = contactDAO.createContact(c);
	     message = "Contact of "+c.getFirstName()+ " " +c.getLastName()+" Created.";
	 }
	 Toast.makeText(this, message, Toast.LENGTH_SHORT).show();
    }

}
