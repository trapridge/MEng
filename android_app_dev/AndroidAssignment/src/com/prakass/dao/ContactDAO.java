package com.prakass.dao;

import java.util.ArrayList;
import java.util.List;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.prakass.domain.Contact;
import com.prakass.helper.Common;
import com.prakass.helper.MySQLiteHelper;

public class ContactDAO {
    // Database fields
    private SQLiteDatabase database;
    private MySQLiteHelper dbHelper;
    private String[] allColumns = { MySQLiteHelper.COLUMN_ID,
	    MySQLiteHelper.COLUMN_FIRST_NAME, MySQLiteHelper.COLUMN_LAST_NAME,
	    MySQLiteHelper.COLUMN_PHONE_NUMBER, MySQLiteHelper.COLUMN_EMAIL };

    public ContactDAO(Context context) {
	dbHelper = new MySQLiteHelper(context);
    }

    public void open() {
	database = dbHelper.getWritableDatabase();
    }

    public void close() {
	dbHelper.close();
    }

    public Contact createContact(Contact contact) {
	ContentValues values = new ContentValues();
	values.put(MySQLiteHelper.COLUMN_FIRST_NAME, contact.getFirstName());
	values.put(MySQLiteHelper.COLUMN_LAST_NAME, contact.getLastName());
	values.put(MySQLiteHelper.COLUMN_PHONE_NUMBER, contact.getPhoneNumber());
	values.put(MySQLiteHelper.COLUMN_EMAIL, contact.getEmail());

	long insertId = database.insert(MySQLiteHelper.TABLE_CONTACTS, null,
		values);

	Cursor cursor = database.query(MySQLiteHelper.TABLE_CONTACTS,
		allColumns, MySQLiteHelper.COLUMN_ID + " = " + insertId, null,
		null, null, null);
	cursor.moveToFirst();
	Contact newContact = cursorToContact(cursor);
	cursor.close();
	Common.log("new contact"+newContact.toString()+ "Created.");
	return newContact;
    }

    public List<Contact> getAllContacts() {
	List<Contact> contacts = new ArrayList<Contact>();
	Cursor cursor = database.query(MySQLiteHelper.TABLE_CONTACTS,
		allColumns, null, null, null, null, null);

	cursor.moveToFirst();
	while (!cursor.isAfterLast()) {
	    Contact contact = cursorToContact(cursor);
	    contacts.add(contact);
	    cursor.moveToNext();
	}
	cursor.close();
	return contacts;
    }

    public void deleteContact(Contact c) {
	long id = c.getId();
	database.delete(MySQLiteHelper.TABLE_CONTACTS, MySQLiteHelper.COLUMN_ID
		+ " = " + id, null);
    }

    private Contact cursorToContact(Cursor cursor) {
	Contact c = new Contact();
	c.setId(cursor.getLong(0));
	c.setFirstName(cursor.getString(1));
	c.setLastName(cursor.getString(2));
	c.setPhoneNumber(cursor.getString(3));
	c.setEmail(cursor.getString(4));
	return c;
    }
}
