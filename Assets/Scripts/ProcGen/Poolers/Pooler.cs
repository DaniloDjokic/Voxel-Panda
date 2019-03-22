using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoxelPanda.Player.Events;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public class Pooler : IPooling
	{
		public GridData spawnableModel;
		protected List<ISpawnable> spawnables;
		private int spawnablesCount;
		public float DespawnDistanceFromPlayer;

		public void CreateSpawnables(int size)
		{
			spawnablesCount = size;
			spawnables = new List<ISpawnable>();
			for (int i = 0; i < spawnablesCount; i++)
			{
				if (spawnableModel == null)
				{
					throw new System.Exception("Spawnable Model missing!");
				}
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
			ISpawnable spawnable = spawnables.FirstOrDefault(i => IsAvailableToSpawnAndInDimensions(i, maxHeight));
			if (spawnable != null) {
				spawnable.ReserveForSpawning();
			}
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

		public void TryDespawning(Vector3 despawnReferentPosition)
		{
			for (int i = 0; i < spawnables.Count; i++)
			{
				if (!spawnables[i].IsAvailableToSpawn() && ShouldDespawnByDistance(spawnables[i], despawnReferentPosition))
				{
					spawnables[i].Despawn();
				}
			}
		}

		private bool ShouldDespawnByDistance(ISpawnable spawnable, Vector3 despawnReferentPosition)
		{
			return (despawnReferentPosition.z - (spawnable.CurrentPosition().z + spawnable.GetConcreteDimensions().y)) > DespawnDistanceFromPlayer;
		}

		public void DespawnAll()
		{
			for(int i = 0; i < spawnables.Count; i++)
			{
				spawnables[i].Despawn();
			}
		}
	}
}

