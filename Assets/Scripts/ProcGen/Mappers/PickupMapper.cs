using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class PickupMapper : Mapper
	{
		private Dictionary<NodeRiskState, float> riskToChance = new Dictionary<NodeRiskState, float>();
		private float riskyChance;
		private float dangerousChance;
		private float criticalChance;

		public void SetChances(float riskyChance, float dangerousChance, float criticalChance)
		{
			riskToChance[NodeRiskState.None] = 0;
			riskToChance[NodeRiskState.Risky] = riskyChance;
			riskToChance[NodeRiskState.Dangerous] = dangerousChance;
			riskToChance[NodeRiskState.Critical] = criticalChance;
		}
		public override IList<IList<MapperNode>> GetNodeMap(IList<IList<MapperNode>> map)
		{
			IList<MapperNode> row = null;
			float random = 0;
			for (int i = 0; i < map.Count; i++)
			{
				row = map[i];
				for (int j = 0; j < row.Count; j++)
				{
					random = Random.Range(0f, 1f);

					var riskState = row[j].GetGridNode().riskState;
					if (random < riskToChance[riskState])
					{
						var spawnable = pooler.GetSpawnable(1);
						if (spawnable != null)
						{
							row[j].SetPickup(spawnable);
							
						}
					}

				}
			}
			return map;
		}
	}
}