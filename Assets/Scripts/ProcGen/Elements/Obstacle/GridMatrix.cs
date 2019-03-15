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
		[SerializeField]
		private GridMatrix flippedMatrix;
		public GridMatrix FlippedMatrix
		{
			get {
				if (isFlippableHorizontally)
				{
					if (flippedMatrix == null)
					{
						GenerateFlippedVersion();
					}
					return flippedMatrix;
				}
				return null;
			}
			private set { flippedMatrix = value; }
		}
[SerializeField]
		public int concreteObjectWidth = 0, concreteObjectHeight = 0;
		[SerializeField]
		public GridNode objectRoot { get; private set; }
		[SerializeField]
		private int objectRootX;
		public int ObjectRootX
		{
			get {
				return objectRootX;
			}
			private set { this.objectRootX = value; }
		}
		[SerializeField]
		public int ObjectRootZ { get; private set; }
		[SerializeField]
		public bool isFlippableHorizontally;

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
					newObstacleMatrix[i].Add(new GridNode());
				}
			}
			this.obstacleMatrix = newObstacleMatrix;
		}

		public void GenerateFlippedVersion()
		{
			flippedMatrix = Instantiate(this);
			flippedMatrix.objectRootX = width - concreteObjectWidth - this.objectRootX;
			flippedMatrix.ObjectRootZ = this.ObjectRootZ;
			flippedMatrix.flippedMatrix = null;
			var flippedObstacleMatrix = new List<NodeList>();
			for(int i = 0; i < this.height; i++)
			{
				flippedObstacleMatrix.Add(new NodeList());

				for(int j = this.width - 1; j >= 0; j--)
				{
					var modelNode = obstacleMatrix[i].nodes[j];
					var modifiedNode = new GridNode(modelNode.occupiedState, modelNode.riskState);
					if(flippedMatrix.objectRootX == this.width - 1 - j && flippedMatrix.ObjectRootZ == i)
					{
						modifiedNode.objectRoot = true;
						flippedMatrix.objectRoot = modifiedNode;
						flippedObstacleMatrix[i].Add(modifiedNode);
					} else
					{
						flippedObstacleMatrix[i].Add(modifiedNode);
					}
				}
			}
			flippedMatrix.obstacleMatrix = flippedObstacleMatrix;
			//Calculate obstacle root
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
					} else
					{
						this.ObjectRootX = j;
						this.ObjectRootZ = i;
					}
				}
			}
			this.objectRoot = node;
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
