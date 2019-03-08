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
		//Movement
		public float speed;
		public float catchUpSpeed;
		public float catchUpDistance;

		private Vector3 direction = new Vector3(0, 0, 1);
		public bool shouldMove = false;
		//references
		private string playerTag;
		private const string obstacleTag = "Obstacle";
		private GameManager gameManager;
		private Transform player;
		private Vector3 startingPosition;

		public void Bind(GameManager gameManager, Transform player)
		{
			this.gameManager = gameManager;
			this.player = player;
			playerTag = this.player.tag;
		}

		private void Awake()
		{
			startingPosition = this.transform.position;
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
		public void SetShouldMove(bool shouldMove)
		{
			this.shouldMove = shouldMove;
		}
		private void Move()
		{
			if (ShouldCatchUp())
			{
				this.transform.position = this.transform.position + direction * catchUpSpeed;
			} else
			{
				this.transform.position = this.transform.position + direction * speed;
			}
			
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
		private void ObstacleTouched(GameObject obstacle)
		{

		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(playerTag))
			{
				PlayerTouched();
			} else if (other.CompareTag(obstacleTag))
			{
				ObstacleTouched(other.gameObject);
			}

		}
		#endregion
	}
}
