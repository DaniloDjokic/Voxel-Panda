<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropMapper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
=======
﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class BackdropMapper : IMapping
	{
		public IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length)
		{
			throw new System.NotImplementedException();
		}

		public void SetPooler(IPooling pooler)
		{
			throw new System.NotImplementedException();
		}

		public void SetSubMapper(IMapping mapper)
		{
			throw new System.NotImplementedException();
		}
	}
}
>>>>>>> Stashed changes
