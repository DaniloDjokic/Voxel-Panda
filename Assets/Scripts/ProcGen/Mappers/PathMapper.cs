using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class PathMapper : Mapper
	{
		public override IList<IList<MapperNode>> GetNodeMap(int width, int height)
		{
			IList<IList<MapperNode>> nodeMatrix = GetBlankMap(width, height);
			int heightLeft = 0;
			for(int i = 0; i < height;)
			{
				heightLeft = height - i;
				ISpawnable spawnable = pooler.GetSpawnable(heightLeft);
				if (spawnable == null) {
					return nodeMatrix;
				}
				GridMatrix spawnableMatrix = spawnable.GetMatrix();
				nodeMatrix[i + spawnableMatrix.ObjectRootZ][spawnableMatrix.ObjectRootX] = new MapperNode(spawnableMatrix.GetNode(spawnableMatrix.ObjectRootZ, spawnableMatrix.ObjectRootX), spawnable);
				i += spawnableMatrix.height;
			}
			return nodeMatrix;
		}
	}
}