using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class DeathController
	{
		private ScoreCalculator scoreCalculator;
		private DeathUI deathUI;

		public DeathController(ScoreCalculator scoreCalculator, DeathUI deathUI)
		{
			this.scoreCalculator = scoreCalculator;
			this.deathUI = deathUI;
		}

		public void RaiseScreen()
		{

		}
	}

}
