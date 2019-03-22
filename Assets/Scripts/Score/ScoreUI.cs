using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VoxelPanda.Score
{
	public class ScoreUI : MonoBehaviour, IScoreListener
	{
        public Text scoreText;

        public ScoreUI(ScoreCalculator scoreCalculator)
        {
            scoreCalculator.Subscribe(this);
        }

		public void OnScoreChanged(float newScore)
		{
            scoreText.text = newScore.ToString();
		}
	}

}