<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour {

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
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class DeathController
	{
		private ScoreCalculator scoreCalculator;

		public DeathController(ScoreCalculator scoreCalculator)
		{
			this.scoreCalculator = scoreCalculator;
		}
	}

}
>>>>>>> Stashed changes
