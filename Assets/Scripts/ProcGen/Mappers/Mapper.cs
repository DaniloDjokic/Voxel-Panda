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

		public IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length)
		{
			MapperNode[][] nodeMatrix = new MapperNode[length][];
			//Going through rows
			float randomChance = Random.Range(0, maxChanceToSpawn);
			for (var j = 0; j < length; j++)
			{
				nodeMatrix[j] = new MapperNode[width];
				for (int i = 0; i < width; i++)
				{
					if (randomChance > chanceToSpawn) {
						ISpawnable spawnable = pooler.GetSpawnable();
						InsertToMatrix(nodeMatrix, spawnable, i, j);
						DecrementChance();
						i = 0;
						j += spawnable.GetMatrix().height;
						break;
					} else
					{
						nodeMatrix[i][j] = new MapperNode();
						IncrementChance();
					}
				}
			}
			return nodeMatrix;
		}

		private void InsertToMatrix(MapperNode[][] matrix, ISpawnable spawnable, int startX, int startZ)
		{
			GridMatrix objectMatrix = spawnable.GetMatrix();
			int width = objectMatrix.width;
			int height = objectMatrix.height;
			for (int j = 0; j < height; j++)
			{
				for(int i = 0; i < width; i++)
				{
					if(i >= startX)
					{
						matrix[startX + i][startZ + j] = new MapperNode(objectMatrix.GetNode(i, j));
					} else
					{
						matrix[startX + i][startZ + j] = new MapperNode();
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
		private void IncrementChance()
		{
			chanceToSpawn = Mathf.Clamp(chanceToSpawn + chanceIncrementPerIteration, minChanceToSpawn, maxChanceToSpawn);
		}
		private void DecrementChance()
		{
			chanceToSpawn = Mathf.Clamp(chanceToSpawn - chanceDecrementPerSpawn, minChanceToSpawn, maxChanceToSpawn);
		}
	}
}