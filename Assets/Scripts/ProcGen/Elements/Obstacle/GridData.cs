using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements
{
	public class GridData : MonoBehaviour, ISpawnable
	{
		public int weight = 1;
		public GridMatrix gridMatrix;
		private bool isAvailableToSpawn = true;
		public bool IsAvailableToSpawn() { return isAvailableToSpawn; }

		public void Despawn()
		{
			isAvailableToSpawn = true;
			gameObject.SetActive(false);
		}

		public Vector2 GetDimensions()
		{
			return new Vector2(gridMatrix.width, gridMatrix.height);
		}

		public GridMatrix GetMatrix()
		{
			return gridMatrix;
		}

		public int GetWeight()
		{
			return weight;
		}

		public void Spawn(Vector3 position)
		{
			isAvailableToSpawn = false;
			gameObject.SetActive(true);
			this.transform.position = position;
		}

	}
}