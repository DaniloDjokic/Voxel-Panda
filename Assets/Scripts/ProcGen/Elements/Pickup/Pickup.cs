using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Score;

namespace VoxelPanda.ProcGen.Elements
{
	public class Pickup : GridData
	{
        public ScoreCalculator scoreCalculator;

        public void SetScoreCalculator(ScoreCalculator scoreCalculator)
        {
            this.scoreCalculator = scoreCalculator;
        }
	}
}