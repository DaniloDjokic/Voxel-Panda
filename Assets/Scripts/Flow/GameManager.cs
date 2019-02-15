using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;

namespace VoxelPanda.Flow
{
	public class GameManager
	{
		public GameState gameState = GameState.Running;

		private RawInput rawInput;
		private DeathController deathController;

		public GameManager(RawInput rawInput, DeathController deathController)
		{
			this.rawInput = rawInput;
			this.deathController = deathController;
		}

		public void StartLevel()
		{
			rawInput.SetDetectingInput(true);
			ChangeState(GameState.Start);
		}

		public void StartRunning()
		{
			ChangeState(GameState.Running);
		}

		public void PauseLevel()
		{
			ChangeState(GameState.Paused);
		}

		public void EndRun()
		{
			deathController.RaiseScreen();
			rawInput.SetDetectingInput(false);
			ChangeState(GameState.Stopped);
		}

		private void ChangeState(GameState newState)
		{
			gameState = newState;
		}
	}
	public enum GameState { Start, Running, Paused, Stopped }
}

