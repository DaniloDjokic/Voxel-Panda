using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

[System.Serializable]
public class SpawnData : MonoBehaviour
{
	[SerializeField]
	private List<Pickup> pickups;
	[SerializeField]
	private List<ObsGridData> obstacles;
	[SerializeField]
	private List<Path> paths;
	[SerializeField]
	private List<Backdrop> backdrops;

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