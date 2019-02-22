using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Mappers 
{
	public struct MapperNode
	{
		private GridNode node;

		public MapperNode(GridNode node)
		{
			this.node = node;
		}
	}
}