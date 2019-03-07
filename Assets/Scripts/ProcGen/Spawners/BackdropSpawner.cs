<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropSpawner : MonoBehaviour {

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
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners
{
	public class BackdropSpawner : ISpawning
	{
		public void SetMapper(IMapping mapper)
		{
			throw new System.NotImplementedException();
		}

		public void SpawnGrid(int width, int length)
		{
			throw new System.NotImplementedException();
		}
	}
}
>>>>>>> Stashed changes
