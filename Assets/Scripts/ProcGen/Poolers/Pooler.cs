using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public class Pooler : IPooling
	{
		protected List<ISpawnable> spawnableModels = new List<ISpawnable>();
		protected List<ISpawnable> spawnables;
		protected List<IPooling> subPoolers = new List<IPooling>();

		public void CreateSpawnables(int size)
		{
			spawnables = new List<ISpawnable>();
			foreach (var model in spawnableModels)
			{
				for (int i = 0; i < size; i++)
				{
					spawnables.Add(spawnableModels[i]);
				}
			}
		}

		public ISpawnable GetSpawnable()
		{
			throw new System.NotImplementedException();
		}

		public void ReturnSpawnable(ISpawnable spawnable)
		{
			throw new System.NotImplementedException();
		}

		public void SetSpawnable(ISpawnable spawnable)
		{
			spawnableModels.Add(spawnable);
			throw new System.NotImplementedException();
		}

		public void SetSubPooling(IPooling pooling)
		{
			subPoolers.Add(pooling);
		}
	}
}

