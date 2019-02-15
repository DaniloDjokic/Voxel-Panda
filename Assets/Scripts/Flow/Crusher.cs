using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VoxelPanda.Flow;

namespace VoxelPanda.Flow
{
	public class Crusher : MonoBehaviour
	{
		private GameManager gameManager;

		public void Bind(GameManager gameManager)
		{
			this.gameManager = gameManager;
		}

		private void PlayerTouched()
		{
			gameManager.EndRun();
		}
	}
}
