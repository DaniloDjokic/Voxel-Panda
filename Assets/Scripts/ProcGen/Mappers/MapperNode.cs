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
			node = new GridNode(gridNode.occupiedState, gridNode.riskState);
			node.objectRoot = gridNode.objectRoot;
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
		public bool IsObjectRoot()
		{
			return node.objectRoot;
		}
		public static MapperNode OverwriteNode(MapperNode oldNode, MapperNode newNode)
		{
			if (oldNode.node.riskState == NodeRiskState.Risky && newNode.node.riskState == NodeRiskState.Risky)
			{
				newNode.node.riskState = NodeRiskState.Dangerous;
			} else if (oldNode.node.riskState == NodeRiskState.Dangerous && newNode.node.riskState == NodeRiskState.Dangerous)
			{
				newNode.node.riskState = NodeRiskState.Critical;
			}
			return newNode;
		}
	}
}