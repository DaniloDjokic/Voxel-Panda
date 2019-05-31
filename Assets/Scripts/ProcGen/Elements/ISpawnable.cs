using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Score;

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

		Vector3 CurrentPosition();
		bool IsAvailableToSpawn();
		void ReserveForSpawning();
		void SetOrientation(Orientation orientation);
		void RandomizeOrientation();

        void Bind(ScoreCalculator scoreCalculator);
	}
	public enum Orientation {LEFT, RIGHT}
}
