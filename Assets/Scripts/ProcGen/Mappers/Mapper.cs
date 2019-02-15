using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public class Mapper : IMapping
	{
		protected IPooling pooler;
		protected List<IMapping> subMappers = new List<IMapping>();

		public IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length)
		{
			throw new System.NotImplementedException();
		}

		public void SetPooler(IPooling pooler)
		{
			this.pooler = pooler;
		}

		public void SetSubMapper(IMapping mapper)
		{
			subMappers.Add(mapper);
		}
	}
}