using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;
using VoxelPanda.Player.Input;
using VoxelPanda.ProcGen;
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class GameManager : IMoveListener
	{
		public GameState gameState = GameState.Stopped;

        private ScoreCalculator scoreCalculator;
		private PhysicsController player;
		private RawAccInput accInput;
        private RawTouchInput touchInput;
		private DeathController deathController;
		private Crusher crusher;
        private ProcGenInjector pgInjector;
		public ProcEvents procEvents;
        private SpawnData spawnData;

		public GameManager(PlayerElements playerElements, DeathController deathController, Crusher crusher, ProcEvents procEvents, ScoreCalculator scoreCalculator, ProcGenInjector procGenInjector)
		{
            this.scoreCalculator = scoreCalculator;
			this.player = playerElements.physicsController;
			this.accInput = playerElements.accInput;
            this.touchInput = playerElements.touchInput;
			this.deathController = deathController;
			this.crusher = crusher;
			this.procEvents = procEvents;
            this.pgInjector = procGenInjector;
			deathController.gameManager = this;
		}

		public void SetRandomSeed(string seed)
		{
			Random.InitState(seed.GetHashCode());
		}

		public void StartLevel()
		{
            scoreCalculator.Reset();
			crusher.ResetPosition();
			player.ResetPlayer();
			procEvents.OnPositionChanged(player.transform.position);
			accInput.SetInputDetection(true);
            touchInput.SetInputDetection(true);

			ChangeState(GameState.Start);
		}

		public void RestartLevel()
		{
			crusher.ResetPositionForRevive();
			player.RevivePlayer();
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

        public void OptionsReset()
        {
            pgInjector.ResetObstacleBinds();
            procEvents.ResetAll();
            StartLevel();
        }

		public void EndRun()
		{
			procEvents.ResetAll();
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

		public void OnPositionChanged(Vector3 position)
		{
			if (gameState == GameState.Start)
			{
				StartRunning();
			}
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
	public enum GameState { Start, Running, Paused, Stopped }
}

