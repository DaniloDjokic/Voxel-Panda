using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoxelPanda.ProcGen.Elements
{
	public enum NodeOccupiedState { None, Passable, Blocked };
	public enum NodeRiskState { None, Risky, Dangerous, Critical  }
	public class ObsGridNode
	{
		public NodeOccupiedState occupiedState = NodeOccupiedState.None;
		public NodeRiskState riskState = NodeRiskState.None;

		public ObsGridNode()
		{

		}
		public ObsGridNode(NodeOccupiedState occupiedState, NodeRiskState riskState)
		{
			this.occupiedState = occupiedState;
			this.riskState = riskState;
		}
	}
}
