package com.prakass.helper;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class MySQLiteHelper extends SQLiteOpenHelper {
    private static final String DATABASE_NAME = "db.contacts";

    public static final String TABLE_CONTACTS = "CONTACTS";

    public static final String COLUMN_ID = "_id";
    public static final String COLUMN_FIRST_NAME = "first_name";
    public static final String COLUMN_LAST_NAME = "last_name";
    public static final String COLUMN_PHONE_NUMBER = "phone_number";
    public static final String COLUMN_EMAIL = "email";

    private static final int DATABASE_VERSION = 1;

    public static final String CREATE_DATABASE = "create table "
	    + TABLE_CONTACTS + "(" + COLUMN_ID
	    + " integer primary key autoincrement, " + COLUMN_FIRST_NAME
	    + " text not null," + COLUMN_LAST_NAME + " text not null,"
	    + COLUMN_PHONE_NUMBER + " text not null," + COLUMN_EMAIL
	    + " text);";

    public MySQLiteHelper(Context context) {
	super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
	db.execSQL(CREATE_DATABASE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
	Log.w(MySQLiteHelper.class.getName(),
		"Upgrading database from version " + oldVersion + " to "
			+ newVersion + ", which will destroy all old data");
	db.execSQL("DROP TABLE IF EXISTS " + TABLE_CONTACTS);
	onCreate(db);

    }

}
