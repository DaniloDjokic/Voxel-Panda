using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Mappers;
using VoxelPanda.ProcGen.Poolers;
using VoxelPanda.ProcGen.Spawners;

namespace VoxelPanda.Flow
{
	public class ProcGenInjector
	{
		private ProcEvents procEvents;
		private SpawnData spawnData;

		public ProcGenInjector(SpawnData spawnData, ProcEvents procEvents)
		{
			this.spawnData = spawnData;
			this.procEvents = procEvents;
		}


		public void BindAll()
		{
			//Pickups
			var pickupPooler = new CoinPooler();
			var pickupMapper = new CoinMapper();

			//Obstacles
			var obstacleRandomizer = new ObsRandomizer();
			foreach (var gridData in spawnData.Obstacles)
			{
				var obstaclePooler = new ObsPooler();
				obstaclePooler.SetSpawnable(gridData);
				obstaclePooler.CreateSpawnables(spawnData.obstaclePoolSize);
				obstacleRandomizer.SetSubPooling(obstaclePooler);
			}

			var obstacleMapper = new ObsMapper();
			//var obstacleSpawner = new TestSpawnPrinter();
			var obstacleSpawner = new ObsSpawner();

			//Path
			var pathPooler = new PathPooler();
			var pathMapper = new PathMapper();
			var pathSpawner = new PathSpawner();

			//Backdrop
			var backdropPooler = new BackdropPooler();
			var backdropMapper = new BackdropMapper();
			var backdropSpawner = new BackdropSpawner();

			//Binding Subs
			obstacleMapper.SetSubMapper(pickupMapper);
			
			this.Bind(spawnData.Pickups, pickupPooler, pickupMapper);
			this.Bind(obstacleRandomizer, obstacleMapper);
			this.Bind(obstacleMapper, obstacleSpawner);
			this.Bind(spawnData.Paths, pathPooler, pathMapper, pathSpawner);
			this.Bind(spawnData.Backdrops, backdropPooler, backdropMapper, backdropSpawner);
			obstacleSpawner.SpawnGrid(10, 120);
		}

		private void Bind(IList<ISpawnable> spawnables, IPooling pooler, IMapping mapper)
		{
			Bind(spawnables, pooler);
			Bind(pooler, mapper);
		}
		private void Bind(IList<ISpawnable> spawnables, IPooling pooler, IMapping mapper, ISpawning spawning)
		{
			Bind(spawnables, pooler, mapper);
			Bind(mapper, spawning);
		}
		private void Bind(IList<ISpawnable> spawnables, IPooling pooler)
		{
			for(var i = 0; i < spawnables.Count; i++)
			{
				pooler.SetSpawnable(spawnables[i]);
			}

		}
		private void Bind(IPooling pooler, IMapping mapper)
		{
			mapper.SetPooler(pooler);

		}
		private void Bind(IMapping mapper, ISpawning spawning)
		{
			spawning.SetMapper(mapper);
			this.procEvents.AddSpawningListener(spawning);
		}
	}
}