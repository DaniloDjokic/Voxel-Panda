using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelPanda.ProcGen.Elements
{
	[CreateAssetMenu(fileName = "ObsGridMatrix", menuName = "LoadData/GridMatrix", order = 1)]
	[System.Serializable]
	public class GridMatrix : ScriptableObject
	{
		[SerializeField]
		public int width, height;
		[SerializeField]
		private List<NodeList> obstacleMatrix;
		public List<NodeList> ObstacleMatrix
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
			List<NodeList> newObstacleMatrix = new List<NodeList>();

			for(int i = 0; i < this.height; i++)
			{
				newObstacleMatrix.Add(new NodeList());

				for(int j = 0; j < this.width; j++)
				{
					/*if(obstacleMatrix != null && i < obstacleMatrix.Length && j < obstacleMatrix[i].Length)
					{
						obstacleMatrix[i][j] = obstacleMatrix[i][j];
					} else
					{*/
						newObstacleMatrix[i].Add(new GridNode());
					//}
				}
			}
			this.obstacleMatrix = newObstacleMatrix;
		}

		public GridNode GetNode(int i, int j)
		{
			return obstacleMatrix[i].nodes[j];
		}

		public void SetObjectRoot(GridNode node)
		{
			for (int i = 0; i < obstacleMatrix.Count; i++)
			{
				for (int j = 0; j < obstacleMatrix[i].nodes.Count; j++)
				{
					if (obstacleMatrix[i].nodes[j] != node)
					{
						GetNode(i, j).objectRoot = false;
					}
				}
			}
			node.objectRoot = true;
		}
	}
	[System.Serializable]
	public class NodeList
	{
		[SerializeField]
		public List<GridNode> nodes;

		public NodeList()
		{
			nodes = new List<GridNode>();
		}

		public void Add(GridNode node)
		{
			nodes.Add(node);
		}

	}
}
