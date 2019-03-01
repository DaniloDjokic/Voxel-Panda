using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public class ObsRandomizer : IPooling
	{
		private List<Pooler> subPoolers;

		public ObsRandomizer()
		{
			subPoolers = new List<Pooler>();
		}

		public void CreateSpawnables(int size)
		{
		}

		public ISpawnable GetSpawnable(int maxHeight)
		{
			return CalculateRandomSpawnable(maxHeight);
		}
		private ISpawnable CalculateRandomSpawnable(int maxHeight)
		{
			int totalWeight = GetAvailableWeightSum(maxHeight);
			if (totalWeight > 0) {
				int random = Random.Range(0, totalWeight);
				int randomIndex = -1;
				for (int i = 0; i < subPoolers.Count; i++)
				{
					random -= subPoolers[i].GetAvailableWeightSum(maxHeight);
					if (random < 0)
					{
						randomIndex = i;
						break;
					}
				}
				return subPoolers[randomIndex].GetSpawnable(maxHeight);
			} else
			{
				return null;
			}

		}

		public void SetSpawnable(ISpawnable spawnable)
		{
		}

		public void SetSubPooling(Pooler pooler)
		{
			subPoolers.Add(pooler);
		}

		public int GetAvailableWeightSum(int maxHeight)
		{
			int totalWeight = 0;
			for(int i = 0; i < subPoolers.Count; i++)
			{
				totalWeight += subPoolers[i].GetAvailableWeightSum(maxHeight);
			}
			return totalWeight;
		}

		public int CurrentlyAvailable(int maxHeight)
		{
			int sum = 0;
			for (int i = 0; i < subPoolers.Count; i++)
			{
				sum += subPoolers[i].CurrentlyAvailable(maxHeight);
			}
			return sum;
		}
	}
}

