using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Mappers 
{
	public struct MapperNode
	{
		public static MapperNode Empty = new MapperNode(GridNode.Empty, null);
		private GridNode node;
		private ISpawnable spawnable;

		public MapperNode(GridNode gridNode, ISpawnable spawnable)
		{
			node = gridNode;
			this.spawnable = spawnable;
		}
		public GridNode GetGridNode()
		{
			return node;
		}
		public ISpawnable GetSpawnable()
		{
			return spawnable;
		}
	}
}