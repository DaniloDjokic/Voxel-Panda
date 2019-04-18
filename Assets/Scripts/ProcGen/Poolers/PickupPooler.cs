using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.Score;

namespace VoxelPanda.ProcGen.Poolers
{
	public class PickupPooler : Pooler
	{
        public void SetScoreCalculator(ScoreCalculator scoreCalculator)
        {
            for (int i = 0; i < spawnables.Count; i++)
            {
                ((Pickup)spawnables[i]).SetScoreCalculator(scoreCalculator);
            }
        }
	}
}

