using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;

namespace VoxelPanda.Flow
{
	public class GameManager
	{
		public GameState gameState = GameState.Running;

		private PhysicsApplier player;
		private RawInput rawInput;
		private DeathController deathController;
		private Crusher crusher;

		public GameManager(PhysicsApplier player, RawInput rawInput, DeathController deathController, Crusher crusher)
		{
			this.player = player;
			this.rawInput = rawInput;
			this.deathController = deathController;
			this.crusher = crusher;
			deathController.gameManager = this;
		}

		public void SetRandomSeed(string seed)
		{
			Random.InitState(seed.GetHashCode());
		}

		public void StartLevel()
		{
			crusher.ResetPosition();
			player.ResetPosition();
			rawInput.SetDetectingInput(true);
			ChangeState(GameState.Start);
		}

		public void StartRunning()
		{
			crusher.SetShouldMove(true);
			ChangeState(GameState.Running);
		}

		public void PauseLevel()
		{
			rawInput.SetDetectingInput(false);
			crusher.SetShouldMove(false);
			ChangeState(GameState.Paused);
		}

		public void EndRun()
		{
			deathController.RaiseScreen();
			rawInput.SetDetectingInput(false);
			crusher.SetShouldMove(false);
			ChangeState(GameState.Stopped);
		}

		private void ChangeState(GameState newState)
		{
			gameState = newState;
		}
	}
	public enum GameState { Start, Running, Paused, Stopped }
}

