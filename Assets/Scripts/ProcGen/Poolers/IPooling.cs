using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public interface IPooling
	{
		void SetSpawnable(ISpawnable spawnable);
		ISpawnable GetSpawnable(int maxHeight);
		void CreateSpawnables(int size);
		int GetAvailableWeightSum(int maxHeight);
		int CurrentlyAvailable(int maxHeight);
	}
}