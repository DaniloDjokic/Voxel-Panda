using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Flow;

namespace VoxelPanda.Flow
{
	public class DeathUI : MonoBehaviour
	{
		public GameObject deathScreen;
		private DeathController deathController;

		public void Bind(DeathController deathController)
		{
			this.deathController = deathController;
		}

		public void RaiseScreen()
		{
			deathScreen.SetActive(true);
		}
		public void LowerScreen()
		{
			deathScreen.SetActive(false);
		}

		public void StartAgain()
		{
			deathController.StartAgain();
		}
	}
}

