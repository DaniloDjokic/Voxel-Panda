using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements
{ 
	public interface ISpawnable
	{
		void Spawn(Transform transform);
		void Despawn();
		Vector2 GetDimensions();
		int GetWeight();
		GridMatrix GetMatrix();

		bool IsAvailableToSpawn();
	}
}
