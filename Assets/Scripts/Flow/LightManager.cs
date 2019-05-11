using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	public Light light;
	private const string shadowStateKey = "Shadow_State";

	void Start () {
		light.shadows = (LightShadows)PlayerPrefs.GetInt(shadowStateKey, 0);	
	}

}
