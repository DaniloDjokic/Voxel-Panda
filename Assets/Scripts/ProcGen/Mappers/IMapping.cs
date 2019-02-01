using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.ProcGen.Mappers
{
	public interface IMapping 
	{
		IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length);
	}
}

