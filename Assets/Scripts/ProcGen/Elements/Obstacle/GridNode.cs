using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements
{
	public enum NodeOccupiedState { None, Passable, Blocked };
	public enum NodeRiskState { None, Risky, Dangerous, Critical  }
	[System.Serializable]
	public class GridNode
	{
		public static GridNode Empty = new GridNode(NodeOccupiedState.None, NodeRiskState.None);

		[SerializeField]
		public NodeOccupiedState occupiedState = NodeOccupiedState.None;
		[SerializeField]
		public NodeRiskState riskState = NodeRiskState.None;
		[SerializeField]
		public bool objectRoot = false;

		public GridNode()
		{

		}
		public GridNode(NodeOccupiedState occupiedState, NodeRiskState riskState)
		{
			this.occupiedState = occupiedState;
			this.riskState = riskState;
		}
	}
}
