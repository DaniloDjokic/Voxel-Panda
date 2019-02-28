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

		public int GetAvailableWeightSum(int maxWidth, int maxHeight)
		{
			return CurrentlyAvailable(maxWidth, maxHeight) * spawnableModel.GetWeight();
		}
		public int CurrentlyAvailable(int maxWidth, int maxHeight)
		{
			return spawnables.Count(i => IsAvailableToSpawnAndInDimensions(i, maxWidth, maxHeight));
		}

		public ISpawnable GetSpawnable(int maxWidth, int maxHeight)
		{
			return spawnables.First(i => IsAvailableToSpawnAndInDimensions(i, maxWidth, maxHeight));
		}

		public void SetSpawnable(ISpawnable spawnable)
		{
			spawnableModel = (GridData) spawnable;
		}
		private bool IsAvailableToSpawnAndInDimensions(ISpawnable i, int maxWidth, int maxHeight)
		{
			return i.IsAvailableToSpawn() && i.GetDimensions().x <= maxWidth && i.GetDimensions().y <= maxHeight;
		}
	}
}

