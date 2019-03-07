<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.ProcGen.Mappers
{
	public interface IMapping 
	{
		IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length);
	}
}

=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Poolers;

namespace VoxelPanda.ProcGen.Mappers
{
	public interface IMapping 
	{
		void SetPooler(IPooling pooler);
		void SetSubMapper(IMapping mapper);
		IEnumerable<IEnumerable<MapperNode>> GetNodeMap(int width, int length);
	}
}

>>>>>>> Stashed changes
