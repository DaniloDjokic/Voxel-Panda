using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class DeathController
	{
		private ScoreCalculator scoreCalculator;

		public DeathController(ScoreCalculator scoreCalculator)
		{
			this.scoreCalculator = scoreCalculator;
		}
	}

}
