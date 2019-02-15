using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners
{
	public class Spawner : ISpawning
	{
		protected IMapping mapper;
		public void SetMapper(IMapping mapper)
		{
			this.mapper = mapper;
		}

		public void SpawnGrid(int width, int length)
		{
			throw new System.NotImplementedException();
		}
	}
}