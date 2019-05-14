using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners 
	{
	public interface ISpawning
	{
		void SetMapper(IMapping mapper);
		int SpawnGrid(int startZ, int width, int length);
	}
}