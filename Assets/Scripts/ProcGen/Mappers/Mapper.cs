using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class Mapper : IMapping
	{
		protected IPooling pooler;
		protected List<IMapping> subMappers = new List<IMapping>();

		//Random chance
		private float maxChanceToSpawn = 1;
		private float minChanceToSpawn = 0.2f;
		public float chanceToSpawn = 1;
		public float chanceIncrementPerIteration = 0.1f;
		public float chanceDecrementPerSpawn = 0.5f;
		//Map
		public int minMapX = 0, maxMapX = 9;
		public int minMapY = 0, maxMapY = 119;
		//static
		private static IList<IList<MapperNode>> BlankMap;


		public IList<IList<MapperNode>> GetNodeMap(int width, int height)
		{
			maxMapX = width - 1; 
			maxMapY = height - 1;
			IList<IList<MapperNode>> nodeMatrix = GetBlankMap(width, height);
			float randomChance = Random.Range(minChanceToSpawn, maxChanceToSpawn);
			int heightLeft = height;
			for (int i = 0; i < height; i++)
			{
				heightLeft = height - i;
				if (randomChance > chanceToSpawn)
				{
					ISpawnable spawnable = pooler.GetSpawnable(heightLeft);
					if (spawnable != null)
					{
						InsertToMatrix(nodeMatrix, spawnable, width, i);
						DecreaseChance();
						i += (int)spawnable.GetFullDimensions().y - 1;
					}

				} else
				{
					IncreaseChance();
				}
			}
			return nodeMatrix;
		}

		private void InsertToMatrix(IList<IList<MapperNode>> nodeMatrix, ISpawnable spawnable, int matrixWidth, int startZ)
		{
			GridMatrix objectMatrix = spawnable.GetMatrix();
			int endZ = objectMatrix.height + startZ;
			int objectRootX = objectMatrix.objectRootX;
			int objectRootZ = objectMatrix.objectRootZ;

			
			int startX = Random.Range(0, matrixWidth - objectMatrix.concreteObjectWidth) - objectRootX;
			int endX = objectMatrix.width + startX;

			//startZ = startZ - objectRootZ;
			endZ = objectMatrix.height + startZ;
			for (int i = startZ; i < endZ; i++)
			{
				if (i >= minMapY && i <= maxMapY)
				{
					for (int j = startX; j < endX; j++)
					{
						if (j >= minMapX && j <= maxMapX)
						{
							GridNode node = objectMatrix.GetNode(i - startZ, j - startX);
							nodeMatrix[i][j] = MapperNode.OverwriteNode(nodeMatrix[i][j], new MapperNode(node, spawnable));
							//nodeMatrix[i][j] = nodeMatrix[i][j].OverwriteNode(node, spawnable);
						}
					}
				}
			}
		}

		public void SetPooler(IPooling pooler)
		{
			this.pooler = pooler;
		}

		public void SetSubMapper(IMapping mapper)
		{
			subMappers.Add(mapper);
		}

		//Chance
		private void DecreaseChance()
		{
			chanceToSpawn = Mathf.Clamp(chanceToSpawn + chanceIncrementPerIteration, 0, maxChanceToSpawn);
		}
		private void IncreaseChance()
		{
			chanceToSpawn = Mathf.Clamp(chanceToSpawn - chanceDecrementPerSpawn, 0, maxChanceToSpawn);
		}
		private static void FillBlankMap(int width, int height)
		{
			BlankMap = new MapperNode[height][];
			for (int i = 0; i < height; i++)
			{
				BlankMap[i] = new MapperNode[width];
				for (int j = 0; j < width; j++)
				{
					BlankMap[i][j] = MapperNode.Empty;
				}
			}
		}
		private static IList<IList<MapperNode>> GetBlankMap(int width, int height)
		{
			if (BlankMap == null || BlankMap.Count != height || BlankMap[0].Count != width)
			{
				FillBlankMap(width, height);
			}
			return BlankMap;
		}
	}
}