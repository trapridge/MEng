package me.trapridge.chordz;

import java.util.List;
import java.util.Random;

import me.trapridge.chordz.dummy.DummyContent;

import com.j256.ormlite.android.apptools.OpenHelperManager;
import com.j256.ormlite.dao.RuntimeExceptionDao;

import android.app.ListFragment;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.Toast;

/**
 * An activity representing a list of Chords. This activity has different
 * presentations for handset and tablet-size devices. On handsets, the activity
 * presents a list of items, which when touched, lead to a
 * {@link ChordDetailActivity} representing item details. On tablets, the
 * activity presents the list of items and item details side-by-side using two
 * vertical panes.
 * <p>
 * The activity makes heavy use of fragments. The list of items is a
 * {@link ChordListFragment} and the item details (if present) is a
 * {@link ChordDetailFragment}.
 * <p>
 * This activity also implements the required
 * {@link ChordListFragment.Callbacks} interface to listen for item selections.
 */
public class ChordListActivity extends FragmentActivity implements
		ChordListFragment.Callbacks {
	
	private final String LOG_TAG = getClass().getSimpleName();
	private DatabaseHelper databaseHelper = null;
	
	/**
	 * You'll need this in your class to get the helper from the manager once per class.
	 */
	public DatabaseHelper getHelper() {
		if (databaseHelper == null) {
			databaseHelper = OpenHelperManager.getHelper(this, DatabaseHelper.class);
		}
		return databaseHelper;
	}
	
	/**
	 * Whether or not the activity is in two-pane mode, i.e. running on a tablet
	 * device.
	 */
	private boolean mTwoPane;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		doSampleDatabaseStuff("onCreate");
		
		setContentView(R.layout.activity_chord_list);

		if (findViewById(R.id.chord_detail_container) != null) {
			// The detail container view will be present only in the
			// large-screen layouts (res/values-large and
			// res/values-sw600dp). If this view is present, then the
			// activity should be in two-pane mode.
			mTwoPane = true;

			// In two-pane mode, list items should be given the
			// 'activated' state when touched.
			((ChordListFragment) getSupportFragmentManager().findFragmentById(
					R.id.chord_list)).setActivateOnItemClick(true);
		}

		// TODO: If exposing deep links into your app, handle intents here.
	}

	/**
	 * Callback method from {@link ChordListFragment.Callbacks} indicating that
	 * the item with the given ID was selected.
	 */
	@Override
	public void onItemSelected(String id) {
		showDetail(id);
	}
	
	private void showDetail(String id) {
		if (mTwoPane) {
			// In two-pane mode, show the detail view in this activity by
			// adding or replacing the detail fragment using a
			// fragment transaction.
			Bundle arguments = new Bundle();
			arguments.putString(ChordDetailFragment.ARG_ITEM_ID, id);
			ChordDetailFragment fragment = new ChordDetailFragment();
			fragment.setArguments(arguments);
			getSupportFragmentManager().beginTransaction()
					.replace(R.id.chord_detail_container, fragment).commit();
		}
		else {
			// In single-pane mode, simply start the detail activity
			// for the selected item ID.
			Intent detailIntent = new Intent(this, ChordDetailActivity.class);
			detailIntent.putExtra(ChordDetailFragment.ARG_ITEM_ID, id);
			startActivity(detailIntent);
		}
	}
	
	@Override
	  public boolean onCreateOptionsMenu(Menu menu) {
	    MenuInflater inflater = getMenuInflater();
	    inflater.inflate(R.menu.chord_list_menu, menu);
	    return true;
	  } 
	
	/**
	 * Do our sample database stuff.
	 */
	private void doSampleDatabaseStuff(String action) {
		/*
		// get our dao
		RuntimeExceptionDao<ChordData, Integer> simpleDao = getHelper().getSimpleDataDao();
		// query for all of the data objects in the database
		List<ChordData> list = simpleDao.queryForAll();
		// our string builder for building the content-view
		StringBuilder sb = new StringBuilder();
		sb.append("got ").append(list.size()).append(" entries in ").append(action).append("\n");

		// if we already have items in the database
		int simpleC = 0;
		for (ChordData simple : list) {
			sb.append("------------------------------------------\n");
			sb.append("[").append(simpleC).append("] = ").append(simple).append("\n");
			simpleC++;
		}
		sb.append("------------------------------------------\n");
		for (ChordData simple : list) {
			simpleDao.delete(simple);
			sb.append("deleted id ").append(simple.id).append("\n");
			Log.i(LOG_TAG, "deleting simple(" + simple.id + ")");
			simpleC++;
		}

		int createNum;
		do {
			createNum = new Random().nextInt(3) + 1;
		} while (createNum == list.size());
		for (int i = 0; i < createNum; i++) {
			// create a new simple object
			//long millis = System.currentTimeMillis();
			ChordData chord = new ChordData("draft", -1, -1, -1, -1, -1, -1);
			// store it in the database
			simpleDao.create(chord);
			Log.i(LOG_TAG, "created simple(draft, -1, -1, -1, -1, -1, -1");
			// output it
			sb.append("------------------------------------------\n");
			sb.append("created new entry #").append(i + 1).append(":\n");
			sb.append(chord).append("\n");
			try {
				Thread.sleep(5);
			} catch (InterruptedException e) {
				// ignore
			}
		}
				
		Log.i(LOG_TAG, sb.toString());
		Log.i(LOG_TAG, "Done with page at " + System.currentTimeMillis());
		*/
	}

	@Override
	protected void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();
		
		/*
		 * You'll need this in your class to release the helper when done.
		 */
		if (databaseHelper != null) {
			OpenHelperManager.releaseHelper();
			databaseHelper = null;
		}
	}
	
	@Override
	  public boolean onOptionsItemSelected(MenuItem item) {
	    switch (item.getItemId()) {
	    // action with ID action_refresh was selected
	    case R.id.add:
	      Toast.makeText(this, "Add selected", Toast.LENGTH_SHORT).show();
	      RuntimeExceptionDao<ChordData, Integer> simpleDao = getHelper().getSimpleDataDao();
	      ChordData chord = new ChordData("draft (added)", -1, -1, -1, -1, -1, -1);
	      simpleDao.create(chord);
	      showDetail(Integer.toString(chord.id));
	      break;
	    // action with ID action_settings was selected
	      /*
	    case R.id.action_settings:
	      Toast.makeText(this, "Settings selected", Toast.LENGTH_SHORT)
	          .show();
	      break;
	      */
	    default:
	      break;
	    }

	    return true;
	  } 
	
}
