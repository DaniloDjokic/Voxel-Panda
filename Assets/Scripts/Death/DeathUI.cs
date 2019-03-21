﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelPanda.Flow;

namespace VoxelPanda.Flow
{
	public class DeathUI : MonoBehaviour
	{
		public GameObject deathScreen;
		private DeathController deathController;
		public Button restartButton;
		public Text countdownTimer;
		public string countdownPreText = "Starting in:\n";
		private string countdownText = "";
		public float countdownTime = 3f;
		private float currentTime = 0f;
		private bool isCountingDown = false;

		public void Bind(DeathController deathController)
		{
			this.deathController = deathController;
		}

		private void Update()
		{
			UpdateCountDown();
		}
		private void UpdateCountDown()
		{
			if(isCountingDown)
			{
				countdownText = countdownPreText + currentTime.ToString("n2");
				countdownTimer.text = countdownText;
				currentTime -= Time.deltaTime;
				if(currentTime < 0)
				{
					TimerCountOut();
				}
			}
		}
		public void RaiseScreen()
		{
			restartButton.gameObject.SetActive(true);
			countdownTimer.gameObject.SetActive(false);
			deathScreen.SetActive(true);
		}
		public void LowerScreen()
		{
			deathScreen.SetActive(false);
		}

		public void StartAgain()
		{
			restartButton.gameObject.SetActive(false);
			countdownTimer.gameObject.SetActive(true);
			StartCountDown();
		}

		private void StartCountDown()
		{
			isCountingDown = true;
			currentTime = countdownTime;
		}

		private void TimerCountOut()
		{
			isCountingDown = false;
			deathController.StartAgain();
		}
	}
}

