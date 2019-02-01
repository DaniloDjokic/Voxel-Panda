using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public interface IPooling
	{
		ISpawnable GetPoolable();
		void CreatePoolables();
		void ReturnPoolable(ISpawnable poolable);
	}
}