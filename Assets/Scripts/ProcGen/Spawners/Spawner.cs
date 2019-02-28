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

		public void SpawnGrid(int width, int length)
		{
			IEnumerable<IEnumerable<MapperNode>> grid = mapper.GetNodeMap(width, length);
			int j = 0;
			foreach(var col in grid)
			{
				int i = 0;
				foreach(MapperNode node in col)
				{
					if (node.GetGridNode().objectRoot)
					{
						Vector3 dst = new Vector3(i * gridUnitToMeter, yHeight, j * gridUnitToMeter);
						node.GetSpawnable().Spawn(dst);
					}
					i++;
				}
				j++;
			}
		}
	}
}