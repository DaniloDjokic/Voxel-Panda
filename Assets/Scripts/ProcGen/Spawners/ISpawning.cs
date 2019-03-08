using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners 
	{
	public interface ISpawning
	{
		void SetMapper(IMapping mapper);
		void SpawnGrid(int width, int length);
	}
}