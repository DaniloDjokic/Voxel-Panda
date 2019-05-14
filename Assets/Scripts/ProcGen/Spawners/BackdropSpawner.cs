using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners
{
	public class BackdropSpawner : Spawner
	{
		public override int SpawnGrid(int startZ, int width, int height)
		{
			return base.SpawnGrid(startZ, width, height) - 1;
		}
	}
}