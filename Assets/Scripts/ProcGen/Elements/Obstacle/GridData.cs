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
		public Orientation orientation = Orientation.RIGHT;
		public Transform rotationPivot;

		public void Despawn()
		{
			isAvailableToSpawn = true;
			gameObject.SetActive(false);
		}

		public Vector2 GetFullDimensions()
		{
			return new Vector2(gridMatrix.width, gridMatrix.height);
		}

		public Vector2 GetConcreteDimensions()
		{
			return new Vector2(gridMatrix.concreteObjectWidth, gridMatrix.concreteObjectHeight);
		}

		public GridMatrix GetMatrix()
		{
			return (orientation == Orientation.RIGHT) ? gridMatrix : gridMatrix.FlippedMatrix;
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

		public void ReserveForSpawning()
		{
			isAvailableToSpawn = false;
		}

		public void SetOrientation(Orientation orientation)
		{
			if (gridMatrix.isFlippableHorizontally)
			{
				this.orientation = orientation;
				if (this.orientation == Orientation.LEFT)
				{
					rotationPivot.rotation = Quaternion.Euler(0, 180, 0);
				} else
				{
					rotationPivot.rotation = Quaternion.Euler(0, 0, 0);
				}
			}
		}

		public void RandomizeOrientation()
		{
			this.SetOrientation((Random.Range(0, 2) == 0) ? Orientation.LEFT : Orientation.RIGHT);  
		}
	}
}