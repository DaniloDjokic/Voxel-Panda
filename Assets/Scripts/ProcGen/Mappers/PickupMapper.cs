using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class PickupMapper : Mapper
	{
		public override IList<IList<MapperNode>> GetNodeMap(IList<IList<MapperNode>> map)
		{
			IList<MapperNode> row = null;
			for (int i = 0; i < map.Count; i++)
			{
				row = map[i];
				for (int j = 0; j < row.Count; j++)
				{
					var riskState = row[j].GetGridNode().riskState;
					if (riskState > 0)
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