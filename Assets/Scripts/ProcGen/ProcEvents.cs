<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcEvents : MonoBehaviour {

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
using VoxelPanda.Player.Events;
using VoxelPanda.ProcGen.Spawners;

namespace VoxelPanda.ProcGen
{
	public class ProcEvents : IMoveListener
	{

		public void AddSpawningListener(ISpawning spawning)
		{

		}

		public void OnPositionChanged(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
			throw new System.NotImplementedException();
		}
	}
}
>>>>>>> Stashed changes
