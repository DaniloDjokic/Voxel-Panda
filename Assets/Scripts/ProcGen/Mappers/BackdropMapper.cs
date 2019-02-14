
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class BackdropMapper : IMapping
	{
		public IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length)
		{
			throw new System.NotImplementedException();
		}

		public void SetPooler(IPooling pooler)
		{
			throw new System.NotImplementedException();
		}

		public void SetSubMapper(IMapping mapper)
		{
			throw new System.NotImplementedException();
		}
	}
}