using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Mappers 
{
	public class MapperNode
	{
		public static MapperNode Empty = new MapperNode(GridNode.Empty, null);
		private GridNode node;
		private ISpawnable spawnable;
		private ISpawnable pickup;

		public MapperNode(GridNode gridNode, ISpawnable spawnable)
		{
			node = new GridNode(gridNode.occupiedState, gridNode.riskState);
			node.objectRoot = gridNode.objectRoot;
			this.spawnable = spawnable;
			this.pickup = null;
		}

		public void SetPickup(ISpawnable pickup)
		{
			this.pickup = pickup;
		}
		public ISpawnable GetPickup()
		{
			return pickup;
		}
		public bool HasPickup()
		{
			return pickup != null;
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
			if(oldNode.spawnable != null)
			{
				if (newNode.spawnable != null)
				{
					throw new System.Exception("Cannot overwrite spawnable!");
				} else
				{
					newNode.spawnable = oldNode.spawnable;
					if (oldNode.IsObjectRoot())
					{
						newNode.node.objectRoot = oldNode.IsObjectRoot();
					}
				}
			}
			if (oldNode.node.riskState == NodeRiskState.Risky && newNode.node.riskState == NodeRiskState.Risky)
			{
				newNode.node.riskState = NodeRiskState.Dangerous;
			} else if (oldNode.node.riskState == NodeRiskState.Dangerous && newNode.node.riskState == NodeRiskState.Dangerous)
			{
				newNode.node.riskState = NodeRiskState.Critical;
			} else
			{
				newNode.node.riskState = (newNode.node.riskState > oldNode.node.riskState) ? newNode.node.riskState : oldNode.node.riskState;
			}
			return newNode;
		}
	}
}