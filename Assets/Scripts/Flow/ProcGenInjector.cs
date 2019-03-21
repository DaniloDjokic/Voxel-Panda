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
			var obstacleRandomizer = new PoolRandomizer();
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
			var pathRandomizer = new PoolRandomizer();
			foreach (var gridData in spawnData.Paths)
			{
				var pathPooler = new PathPooler();
				pathPooler.SetSpawnable(gridData);
				pathPooler.CreateSpawnables(spawnData.pathPoolSize);
				pathRandomizer.SetSubPooling(pathPooler);
			}
			var pathMapper = new PathMapper();
			var pathSpawner = new PathSpawner();

			//Backdrop
			var backdropRandomizer = new PoolRandomizer();
			foreach(var gridData in spawnData.Backdrops)
			{
				var backdropPooler = new BackdropPooler();
				backdropPooler.SetSpawnable(gridData);
				backdropPooler.CreateSpawnables(spawnData.backdropPoolSize);
				backdropRandomizer.SetSubPooling(backdropPooler);
			}
			var backdropMapper = new BackdropMapper();
			var backdropSpawner = new BackdropSpawner();

			//Binding Subs
			obstacleMapper.SetSubMapper(pickupMapper);
			
			this.Bind(spawnData.Pickups, pickupPooler, pickupMapper);
			pickupPooler.CreateSpawnables(spawnData.pickupPoolSize);
			this.Bind(obstacleRandomizer, obstacleMapper);
			obstacleSpawner.SetMapper(obstacleMapper);
			procEvents.AddSpawningListener(new SpawnerData(obstacleSpawner, spawnData.obstaclesGenerationOffset, spawnData.obstaclesGenerationBuffer));

			this.Bind(pathRandomizer, pathMapper);
			pathSpawner.SetMapper(pathMapper);

			procEvents.AddSpawningListener(new SpawnerData(pathSpawner));

			this.Bind(backdropRandomizer, backdropMapper);
			backdropSpawner.SetMapper(backdropMapper);
			procEvents.AddSpawningListener(new SpawnerData(backdropSpawner));
			//this.Bind(spawnData.Backdrops, backdropPooler, backdropMapper, backdropSpawner);
		}

		private void Bind(IList<ISpawnable> spawnables, IPooling pooler, IMapping mapper)
		{
			Bind(spawnables, pooler);
			Bind(pooler, mapper);
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

	}
}