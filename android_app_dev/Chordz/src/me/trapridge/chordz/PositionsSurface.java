package me.trapridge.chordz;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceView;

public class PositionsSurface extends SurfaceView {

	public final String LOG_TAG = getClass().getSimpleName();
	
	public PositionsSurface(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}
 
	public PositionsSurface(Context C, AttributeSet attribs){
	    super(C, attribs);

	    // Other setup code you want here
	}

	public PositionsSurface(Context C, AttributeSet attribs, int defStyle){
	    super(C, attribs, defStyle);

	    // Other setup code you want here
	}
	
	@Override
	public void draw(Canvas canvas) {
		// TODO Auto-generated method stub
		super.draw(canvas);
		
		Paint textPaint = new Paint();
	    textPaint.setColor(Color.WHITE);

	    canvas.drawLine(0, 0, canvas.getWidth(), canvas.getHeight(), textPaint);
	    canvas.drawCircle(50, 50, 23, textPaint);
	}

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		Log.i(LOG_TAG, "touch");
		// TODO Auto-generated method stub
		return super.onTouchEvent(event);
		
		
	}
	
	
}
