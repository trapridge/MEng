package me.trapridge.chordz;

import com.j256.ormlite.android.apptools.OpenHelperManager;
import com.j256.ormlite.dao.RuntimeExceptionDao;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.NavUtils;
import android.text.Editable;
import android.text.GetChars;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import me.trapridge.chordz.dummy.DummyContent;

/**
 * A fragment representing a single Chord detail screen. This fragment is either
 * contained in a {@link ChordListActivity} in two-pane mode (on tablets) or a
 * {@link ChordDetailActivity} on handsets.
 */
public class ChordDetailFragment extends Fragment {
	/**
	 * The fragment argument representing the item ID that this fragment
	 * represents.
	 */
	public static final String ARG_ITEM_ID = "item_id";

	private final String LOG_TAG = getClass().getSimpleName(); 
	
	private DatabaseHelper databaseHelper = null;
	
	/**
	 * You'll need this in your class to get the helper from the manager once per class.
	 */
	public DatabaseHelper getHelper() {
		if (databaseHelper == null) {
			databaseHelper = OpenHelperManager.getHelper(this.getActivity(), DatabaseHelper.class);
		}
		return databaseHelper;
	}
	
	/**
	 * The dummy content this fragment is presenting.
	 */
	//private DummyContent.DummyItem mItem;
	private ChordData mItem;

	/**
	 * Mandatory empty constructor for the fragment manager to instantiate the
	 * fragment (e.g. upon screen orientation changes).
	 */
	public ChordDetailFragment() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		if (getArguments().containsKey(ARG_ITEM_ID)) {
			// Load the dummy content specified by the fragment
			// arguments. In a real-world scenario, use a Loader
			// to load content from a content provider.
		    RuntimeExceptionDao<ChordData, Integer> simpleDao = getHelper().getSimpleDataDao();
		    //ChordData chord = new ChordData("draft", -1, -1, -1, -1, -1, -1);
		    //simpleDao.create(chord);
		    //showDetail(chord.id);	     
			mItem = simpleDao.queryForId(Integer.parseInt(getArguments().getString(ARG_ITEM_ID)));
			
			//mItem = DummyContent.ITEM_MAP.get(getArguments().getString(ARG_ITEM_ID));
		}
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_chord_detail,
				container, false);

		// Show the dummy content as text in a TextView.
		if (mItem != null) {
			final EditText et = ((EditText) rootView.findViewById(R.id.chord_detail));
			et.setText(mItem.name);
			et.addTextChangedListener(new TextWatcher(){
			    public void afterTextChanged(Editable s) {
			    	Log.i(LOG_TAG, "name changed");
			    	RuntimeExceptionDao<ChordData, Integer> simpleDao = getHelper().getSimpleDataDao();
			    	mItem.name = et.getText().toString(); 
			    	simpleDao.update(mItem);
			    }
			    public void beforeTextChanged(CharSequence s, int start, int count, int after){}
			    public void onTextChanged(CharSequence s, int start, int before, int count){}
			}); 
		}
		
		Button button= (Button) rootView.findViewById(R.id.deleteButton);
		button.setOnClickListener(new View.OnClickListener() {
		    @Override
		    public void onClick(View v) {
		    	RuntimeExceptionDao<ChordData, Integer> simpleDao = getHelper().getSimpleDataDao();
		    	simpleDao.delete(mItem); 
		    	NavUtils.navigateUpTo(getActivity(), new Intent(getActivity(),
						ChordListActivity.class));
		    }
		});
		
		

		return rootView;
	}
	
	
}
