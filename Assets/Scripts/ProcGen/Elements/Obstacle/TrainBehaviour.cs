using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements.Obstacle
{
	public class TrainBehaviour : ObsBehaviour
	{
		public float trainSpeed;
		public float trainStoppageTime;

		private Vector3 direction;
		private float leftBoundary, rightBoundary;

		private void Update()
		{
			
		}
	}
}
