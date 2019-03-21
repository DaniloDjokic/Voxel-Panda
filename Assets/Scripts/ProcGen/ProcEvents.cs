using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;
using VoxelPanda.ProcGen.Spawners;

namespace VoxelPanda.ProcGen
{
	public class SpawnerData
	{
		public ISpawning spawner;
		public int lastZGenerated;
		public int generationBuffer;
		public int generationOffset;
		public int batchHeight;
		public static int gridWidth = 10;

		public SpawnerData(ISpawning spawner, int generationOffset = 0, int generationBuffer = 50, int batchHeight = 40, int lastZ = 0)
		{
			this.spawner = spawner;
			this.lastZGenerated = lastZ;
			this.generationBuffer = generationBuffer;
			this.generationOffset = generationOffset;
			this.batchHeight = batchHeight;
		}

		public void TryToSpawn(int ZPosition)
		{
			if(this.lastZGenerated < ZPosition + generationBuffer)
			{
				this.lastZGenerated = spawner.SpawnGrid(this.lastZGenerated + generationOffset, 10, batchHeight);
			}
		}
	}

	public class ProcEvents : IMoveListener
	{
		public List<SpawnerData> spawners = new List<SpawnerData>();

		public ProcEvents(MoveEvents moveEvents)
		{
			moveEvents.Subscribe(this);
		}

		public void AddSpawningListener(SpawnerData spawnerData)
		{
			spawners.Add(spawnerData);
		}

		public void OnPositionChanged(Vector3 position)
		{
			int ZPosition = (int)position.z;
			for (int i = 0; i < spawners.Count; i++)
			{
				spawners[i].TryToSpawn(ZPosition);
			}
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
		}
	}
}