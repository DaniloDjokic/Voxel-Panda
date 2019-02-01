using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.ProcGen.Spawners 
	{
	public interface ISpawning
	{
		void SpawnGrid(int width, int length);
	}
}
