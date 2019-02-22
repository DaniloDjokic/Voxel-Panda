using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public class Pooler : IPooling
	{
		public GridData spawnableModel;
		protected List<ISpawnable> spawnables;
		private int spawnablesCount;

		public void CreateSpawnables(int size)
		{
			spawnablesCount = size;
			spawnables = new List<ISpawnable>();
			for (int i = 0; i < size; i++)
			{
				spawnables.Add(spawnableModel);
			}
		}

		public int GetAvailableWeightSum()
		{
			return CurrentlyAvailable() * spawnableModel.GetWeight();
		}
		public int CurrentlyAvailable()
		{
			return spawnables.Count(i => i.IsAvailableToSpawn());
		}

		public ISpawnable GetSpawnable()
		{
			return spawnables.First(i => i.IsAvailableToSpawn());
		}

		public void SetSpawnable(ISpawnable spawnable)
		{
			spawnableModel = (GridData) spawnable;
		}
	}
}

