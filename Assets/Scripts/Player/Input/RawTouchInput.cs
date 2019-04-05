using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;

public class RawTouchInput : MonoBehaviour
{
    public FlingCalculator flingCalculator;

    private bool detectingInput = true;

    private Vector3 initialTouchPosition;
    private Vector3 touchStartingPosition;
    private Vector3 touchHoldPosition;
    
	void Update ()
    {
        if(detectingInput)
        {
            if (Input.touchCount > 0)
            {
                HandleTouch(Input.touches[0]);
            }
            else
            {
                SimulateTouchWithMouse();
            }
        }    
	}

    public void SetInputDetection(bool active)
    {
        detectingInput = active;
    }

    void HandleTouch(Touch touch)
    {
        if(touch.phase != TouchPhase.Canceled) //this line checks if the mouse simulated touch is valid
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TouchStarted(touch);
                    break;
                case TouchPhase.Moved:
                    TouchHold(touch);
                    break;
                case TouchPhase.Ended:
                    TouchEnded(touch);
                    
                    break;
                default:
                    break;
            }
        }
    }

    void TouchStarted(Touch touch)
    {
    	initialTouchPosition = touch.position;
        //initialTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        flingCalculator.StartFlingCalculation(initialTouchPosition);
    }

    void TouchHold(Touch touch)
    {
    	touchStartingPosition = Camera.main.ScreenToWorldPoint(initialTouchPosition);
        touchHoldPosition = Camera.main.ScreenToWorldPoint(touch.position);
        flingCalculator.UpdateRawVector(ConstructVector(touchStartingPosition, touchHoldPosition), touch.position);
    }

    void TouchEnded(Touch touch)
    {
    	touchStartingPosition = Camera.main.ScreenToWorldPoint(initialTouchPosition);
        touchHoldPosition = Camera.main.ScreenToWorldPoint(touch.position);
        flingCalculator.UpdateRawVector(ConstructVector(touchStartingPosition, touchHoldPosition), touch.position);
        flingCalculator.ApplyFling();

        initialTouchPosition = touchStartingPosition = touchHoldPosition = Vector3.zero;
    }

    void SimulateTouchWithMouse()
    {
        Touch touch = new Touch();
        touch.phase = TouchPhase.Canceled; // this line invalidates the touch if mouse button is not activated

        if (Input.GetMouseButtonDown(0))
            touch.phase = TouchPhase.Began;
        else if (Input.GetMouseButton(0))
            touch.phase = TouchPhase.Moved;
        else if (Input.GetMouseButtonUp(0))
            touch.phase = TouchPhase.Ended;

        touch.position = Input.mousePosition;
        HandleTouch(touch);
    }

    Vector3 ConstructVector(Vector3 touchStart, Vector3 touchEnd)
    {
       Vector3 temp = touchStart - touchEnd;
       temp.y = ConstMoveData.universalY;
       return temp;
    }
}
