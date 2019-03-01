using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.ProcGen.Mappers;

namespace VoxelPanda.ProcGen.Spawners
{
	public class TestSpawnPrinter : ISpawning
	{
		private IMapping mapper;
		public string path = "Assets/Resources/SpawnMatrix.csv";
		public void SetMapper(IMapping mapper)
		{
			this.mapper = mapper;
		}

		public void SpawnGrid(int width, int length)
		{

			StreamWriter writer = new StreamWriter(path, true);
			IList<IList<MapperNode>> grid = mapper.GetNodeMap(width, length);

			foreach (var col in grid)
			{
				string row = "";
				foreach (MapperNode node in col)
				{
					GridNode gridNode = node.GetGridNode();
					row += string.Format("{0}||{1}||{2},", gridNode.occupiedState.ToString(), gridNode.riskState.ToString(), gridNode.objectRoot.ToString());
				}
				writer.WriteLine(row);
			}
			writer.Close();
		}
	}
}

