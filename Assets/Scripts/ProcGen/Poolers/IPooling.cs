using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public interface IPooling
	{
		void SetSubPooling(IPooling pooling);
		void SetSpawnable(ISpawnable spawnable);
		ISpawnable GetSpawnable();
		void CreateSpawnables(int size);
		void ReturnSpawnable(ISpawnable spawnable);
	}
}