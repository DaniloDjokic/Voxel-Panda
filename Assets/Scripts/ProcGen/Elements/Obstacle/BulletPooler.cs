using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements
{
	public class BulletPooler
	{
		BulletBehaviour bulletModel;
		private List<BulletBehaviour> bullets;
		private int poolSize;

		public BulletPooler(BulletBehaviour bulletModel, int poolSize)
		{
			this.bulletModel = bulletModel;
			this.poolSize = poolSize;
			CreateSpawnables();
		}

		private void CreateSpawnables()
		{
			bullets = new List<BulletBehaviour>();
			for(int i = 0; i < poolSize; i++)
			{
				if(bulletModel == null)
				{
					throw new System.Exception("Bullet Model missing!");
				}
				var bullet = GameObject.Instantiate(bulletModel);
				bullet.Despawn();
				bullets.Add(bullet);
			}
		}

		public int CurrentlyAvailable()
		{
			return bullets.Count(bullet => bullet.AvailableToSpawn());
		}

		public void DespawnAll()
		{
			for (var i = 0; i < bullets.Count; i++)
			{
				bullets[i].Despawn();
			}
		}

		public BulletBehaviour GetBullet()
		{
			for (var i = 0; i < bullets.Count; i++)
			{
				if (bullets[i].AvailableToSpawn())
				{
					return bullets[i];
				}
			}
			return null;
		}
	}
}

