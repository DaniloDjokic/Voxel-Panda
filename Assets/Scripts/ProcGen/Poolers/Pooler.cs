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
				ISpawnable spawnable = GameObject.Instantiate(spawnableModel);
				spawnable.Despawn();
				spawnables.Add(spawnable);
			}
		}

		public int GetAvailableWeightSum(int maxHeight)
		{
			return CurrentlyAvailable(maxHeight) * spawnableModel.GetWeight();
		}
		public int CurrentlyAvailable(int maxHeight)
		{
			return spawnables.Count(i => IsAvailableToSpawnAndInDimensions(i, maxHeight));
		}

		public ISpawnable GetSpawnable(int maxHeight)
		{
			ISpawnable spawnable = spawnables.First(i => IsAvailableToSpawnAndInDimensions(i, maxHeight));
			spawnable.ReserveForSpawning();
			return spawnable;
		}

		public void SetSpawnable(ISpawnable spawnable)
		{
			spawnableModel = (GridData) spawnable;
		}
		private bool IsAvailableToSpawnAndInDimensions(ISpawnable i, int maxHeight)
		{
			return i.IsAvailableToSpawn() && i.GetConcreteDimensions().y <= maxHeight;
		}
	}
}

