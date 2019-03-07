<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsGridData : MonoBehaviour {

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

namespace VoxelPanda.ProcGen.Elements
{
	public class ObsGridData : MonoBehaviour, ISpawnable
	{
		public void Despawn()
		{
			throw new System.NotImplementedException();
		}

		public Vector2 GetDimensions()
		{
			throw new System.NotImplementedException();
		}

		public void Spawn(Transform transform)
		{
			throw new System.NotImplementedException();
		}
	}
}
>>>>>>> Stashed changes
