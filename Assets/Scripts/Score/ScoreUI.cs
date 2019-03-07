<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Score
{
	public class ScoreUI : MonoBehaviour, IScoreListener
	{
		public void OnScoreChanged(float newScore)
		{
			throw new System.NotImplementedException();
		}
	}

}
>>>>>>> Stashed changes
