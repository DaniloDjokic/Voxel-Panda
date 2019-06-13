using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VoxelPanda.Flow;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class Crusher : MonoBehaviour
	{
		//Movement
		public float speed;
        public DifficultyCurve speedController;
		public float catchUpSpeed;
		public float catchUpDistance;

		private Vector3 direction = new Vector3(0, 0, 1);
		public bool shouldMove = false;
		//references
		private string playerTag;
		private const string obstacleTag = "Obstacle";
		private GameManager gameManager;
        private ScoreCalculator scoreCalculator;
		private Transform player;
		public float reviveDistanceOffset;

		public Vector3 startingPosition;

		private void Start()
		{
			speedController.CheckForRemoteUpdates();
		}

		public void Bind(GameManager gameManager, Transform player, ScoreCalculator scoreCalculator)
		{
			this.gameManager = gameManager;
            this.scoreCalculator = scoreCalculator;
			this.player = player;
			playerTag = this.player.tag;
		}

		private void Update()
		{
			if (shouldMove)
			{
				Move();
			}
		}

		#region Movement
		public void ResetPosition()
		{
			this.transform.position = startingPosition;
		}
		public void ResetPositionForRevive()
		{
			this.transform.position = new Vector3(player.position.x, this.transform.position.y, player.position.z - reviveDistanceOffset);
		}
		public void SetShouldMove(bool shouldMove)
		{
			this.shouldMove = shouldMove;
		}
		private void Move()
		{
			if (ShouldCatchUp())
			{
				this.transform.position = this.transform.position + direction * catchUpSpeed * Time.deltaTime;
			} else
			{
				this.transform.position = this.transform.position + direction * GetSpeed() * Time.deltaTime;
			}
			
		}

        private float GetSpeed()
        {
            return speedController.GetValue(scoreCalculator.GetScore());
        }

		private bool ShouldCatchUp()
		{
			return Vector3.Distance(transform.position, player.position) > catchUpDistance;
		}
		#endregion

		#region Collision
		private void PlayerTouched()
		{
			gameManager.EndRun();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(playerTag))
			{
				PlayerTouched();
			}

		}
		#endregion
	}
}
