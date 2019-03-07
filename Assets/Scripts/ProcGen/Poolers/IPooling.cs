<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public interface IPooling
	{
		ISpawnable GetPoolable();
		void CreatePoolables();
		void ReturnPoolable(ISpawnable poolable);
	}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

namespace VoxelPanda.ProcGen.Poolers
{
	public interface IPooling
	{
		void SetSubPooling(IPooling pooling);
		void SetPoolable(ISpawnable poolable);
		ISpawnable GetPoolable();
		void CreatePoolables();
		void ReturnPoolable(ISpawnable poolable);
	}
>>>>>>> Stashed changes
}