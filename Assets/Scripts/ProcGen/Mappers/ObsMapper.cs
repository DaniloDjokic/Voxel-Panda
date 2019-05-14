using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers 
{
	public class ObsMapper : Mapper
	{
		public override IList<IList<MapperNode>> GetNodeMap(int width, int height)
		{
			var map = base.GetNodeMap(width, height);
			for (int i = 0; i < subMappers.Count; i++)
			{
				map = subMappers[i].GetNodeMap(map);
			}
			return map;
		}
	}
}