using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwitch : MonoBehaviour
{
    public RawTouchInput rawTouchInput;
    public RawAccInput rawAccInput;
   
    public void SetInputSwitch(bool active)
    {
        rawTouchInput.SetInputDetection(active);
        rawAccInput.SetInputDetection(active);
    }
	
}
