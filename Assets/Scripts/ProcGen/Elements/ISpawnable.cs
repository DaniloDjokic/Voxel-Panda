using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements
{ 
	public interface ISpawnable
	{
		void Spawn(Vector3 position);
		void Despawn();
		Vector2 GetFullDimensions();
		Vector2 GetConcreteDimensions();
		int GetWeight();
		GridMatrix GetMatrix();

		bool IsAvailableToSpawn();
		void ReserveForSpawning();
		void SetOrientation(Orientation orientation);
		void RandomizeOrientation();
	}
	public enum Orientation {UP, DOWN, LEFT, RIGHT}
}
