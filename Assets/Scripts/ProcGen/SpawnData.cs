using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen;
using VoxelPanda.ProcGen.Elements;

[System.Serializable]
public class SpawnData : MonoBehaviour
{
	[SerializeField]
	private List<Pickup> pickups;
	[SerializeField]
	private List<GridData> obstacles;
	[SerializeField]
	private List<Path> paths;
	[SerializeField]
	private List<Backdrop> backdrops;
	[SerializeField]
	public int obstaclePoolSize = 10;
	[SerializeField]
	public int pickupPoolSize = 10;
	[SerializeField]
	public int pathPoolSize = 10;
	[SerializeField]
	public int backdropPoolSize = 10;
	[SerializeField]
	public int obstaclesGenerationOffset = 5;
	[SerializeField]
	public int obstaclesGenerationBuffer = 40;
	[SerializeField]
	public float coinSpawnCriticalChance = 0.2f;
	[SerializeField]
	public float coinSpawnDangerousChance = 0.1f;
	[SerializeField]
	public float coinSpawnRiskyChance = 0.005f;

	public IList<ISpawnable> Pickups
	{
		get
		{
			return pickups.ConvertAll(i => i as ISpawnable);
		}
	}
	public IList<ISpawnable> Obstacles
	{
		get
		{
			return obstacles.ConvertAll(i => i as ISpawnable);
		}
	}
	public IList<ISpawnable> Paths
	{
		get
		{
			return paths.ConvertAll(i => i as ISpawnable);
		}
	}
	public IList<ISpawnable> Backdrops
	{
		get
		{
			return backdrops.ConvertAll(i => i as ISpawnable);
		}
	}
}