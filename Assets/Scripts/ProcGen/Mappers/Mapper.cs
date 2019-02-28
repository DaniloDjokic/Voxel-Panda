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

		public IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int height)
		{
			MapperNode[][] nodeMatrix = new MapperNode[height][];
			//Going through rows
			float randomChance = Random.Range(minChanceToSpawn, maxChanceToSpawn);
			for (var j = 0; j < height; j++)
			{
				nodeMatrix[j] = new MapperNode[width];
				for (int i = 0; i < width; i++)
				{
					if (randomChance > chanceToSpawn) {
						ISpawnable spawnable = pooler.GetSpawnable(width - i, height - j);
						if(spawnable != null) {
							InsertToMatrix(nodeMatrix, spawnable, i, j, width);
							DecreaseChance();
							i = 0;
							j += spawnable.GetMatrix().height - 1;
							break;
						} else
						{
							nodeMatrix[j][i] = MapperNode.Empty;
							IncreaseChance();
						}
					} else
					{
						nodeMatrix[j][i] = MapperNode.Empty;
						IncreaseChance();
					}
				}
			}
			return nodeMatrix;
		}

		private void InsertToMatrix(MapperNode[][] matrix, ISpawnable spawnable, int startX, int startZ, int matrixWidth)
		{
			GridMatrix objectMatrix = spawnable.GetMatrix();
			int width = objectMatrix.width;
			int height = objectMatrix.height;
			for (int j = startZ; j < startZ + height; j++)
			{
				if(matrix[j] == null)
				{
					matrix[j] = new MapperNode[matrixWidth];
				}
				for (int i = 0; i < matrix[j].Length; i++)
				{
					if (i >= startX && i < (startX + width))
					{
						matrix[j][i] = new MapperNode(objectMatrix.GetNode(j - startZ, i - startX), spawnable);
					} else
					{
						matrix[j][i] = MapperNode.Empty;
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
	}
}