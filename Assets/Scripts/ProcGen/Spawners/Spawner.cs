using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners
{
	public class Spawner : ISpawning
	{
		private float gridUnitToMeter = 1;
		private float yHeight = 0f;
		protected IMapping mapper;
		public void SetMapper(IMapping mapper)
		{
			this.mapper = mapper;
		}

		public virtual int SpawnGrid(int startZ, int width, int height)
		{
			IList<IList<MapperNode>> grid = mapper.GetNodeMap(width, height);
			int lastHeightGenerated = 0;
			for(int i = 0; i < height; i++) 
			{
				for(int j = 0; j < width; j++)
				{
					var node = grid[i][j];
					if (node.IsObjectRoot())
					{
						Vector3 dst = new Vector3(j * gridUnitToMeter, yHeight, startZ + i * gridUnitToMeter);
						ISpawnable spawnable = node.GetSpawnable();
						spawnable.Spawn(dst);
						lastHeightGenerated = i + (int)spawnable.GetConcreteDimensions().y;
					}
					if (node.HasPickup())
					{
						Vector3 dst = new Vector3(j * gridUnitToMeter, yHeight, startZ + i * gridUnitToMeter);
						node.GetPickup().Spawn(dst);
					}
				}
			}
			return lastHeightGenerated + startZ;
		}
	}
}