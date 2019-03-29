using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class BackdropMapper : Mapper
	{
		public override IList<IList<MapperNode>> GetNodeMap(int width, int height)
		{
			IList<IList<MapperNode>> nodeMatrix = GetBlankMap(width, height);
			int heightLeft = 0;
			for(int i = 0; i < height;)
			{
				heightLeft = height - i;
				ISpawnable spawnable = pooler.GetSpawnable(heightLeft);
				if(spawnable == null)
				{
					return nodeMatrix;
				}
				spawnable.SetOrientation(Orientation.RIGHT);
				GridMatrix spawnableMatrix = spawnable.GetMatrix();
				nodeMatrix[i + spawnableMatrix.ObjectRootZ][spawnableMatrix.ObjectRootX] = new MapperNode(spawnableMatrix.GetNode(spawnableMatrix.ObjectRootZ, spawnableMatrix.ObjectRootX), spawnable);

				ISpawnable leftSpawnable = pooler.GetSpawnable(heightLeft);
				if(leftSpawnable == null)
				{
					return nodeMatrix;
				}
				leftSpawnable.SetOrientation(Orientation.LEFT);
				GridMatrix leftSpawnableMatrix = leftSpawnable.GetMatrix();
				nodeMatrix[i + leftSpawnableMatrix.ObjectRootZ + 1][leftSpawnableMatrix.ObjectRootX] = new MapperNode(leftSpawnableMatrix.GetNode(leftSpawnableMatrix.ObjectRootZ, leftSpawnableMatrix.ObjectRootX), leftSpawnable);
				i += spawnableMatrix.height;
			}
			return nodeMatrix;
		}
	}
}