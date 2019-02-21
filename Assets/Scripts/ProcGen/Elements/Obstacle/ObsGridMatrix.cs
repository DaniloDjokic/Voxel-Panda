using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoxelPanda.ProcGen.Elements
{
	[Serializable]
	public class ObsGridMatrix
	{
		public int width, height;
		private ObsGridNode[][] obstacleMatrix;
		public ObsGridNode[][] ObstacleMatrix
		{
			get
			{
				if(obstacleMatrix == null)
					CreateMatrix();
				return obstacleMatrix;
			}
		}

		public void CreateMatrix()
		{
			ObsGridNode[][] newObstacleMatrix = new ObsGridNode[this.width][];
			
			for(int i = 0; i < this.width; i++)
			{
				newObstacleMatrix[i] = new ObsGridNode[this.height];
				for(int j = 0; j < this.height; j++)
				{
					/*if(obstacleMatrix != null && i < obstacleMatrix.Length && j < obstacleMatrix[i].Length)
					{
						obstacleMatrix[i][j] = obstacleMatrix[i][j];
					} else
					{*/
						newObstacleMatrix[i][j] = new ObsGridNode();
					//}
				}
			}
			this.obstacleMatrix = newObstacleMatrix;
		}

	}
}
