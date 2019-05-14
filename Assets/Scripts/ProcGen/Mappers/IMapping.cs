using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public interface IMapping 
	{
		void SetPooler(IPooling pooler);
		void SetSubMapper(IMapping mapper);
		IList<IList<MapperNode>> GetNodeMap(int width, int length);
		IList<IList<MapperNode>> GetNodeMap(IList<IList<MapperNode>> map);
	}
}
