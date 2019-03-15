using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;

namespace VoxelPanda.Flow
{
	public class GameManager
	{
		public GameState gameState = GameState.Running;

		private PhysicsController player;
		private RawAccInput accInput;
        private RawTouchInput touchInput;
		private DeathController deathController;
		private Crusher crusher;

		public GameManager(PlayerElements playerElements, DeathController deathController, Crusher crusher)
		{
			this.player = playerElements.physicsController;
			this.accInput = playerElements.accInput;
            this.touchInput = playerElements.touchInput;
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
			accInput.SetInputDetection(true);
            touchInput.SetInputDetection(true);
			ChangeState(GameState.Start);
		}

		public void StartRunning()
		{
			crusher.SetShouldMove(true);
			ChangeState(GameState.Running);
		}

		public void PauseLevel()
		{
            accInput.SetInputDetection(false);
            touchInput.SetInputDetection(false);
            crusher.SetShouldMove(false);
			ChangeState(GameState.Paused);
		}

		public void EndRun()
		{
			deathController.RaiseScreen();
            accInput.SetInputDetection(false);
            touchInput.SetInputDetection(false);
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

