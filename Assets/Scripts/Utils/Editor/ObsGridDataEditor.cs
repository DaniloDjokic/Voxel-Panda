using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

[CustomEditor(typeof(ObsGridData))]
public class ObsGridDataEditor : Editor {
	private int curWidth, curHeight;
	private ObsGridData obsGridData;

	private static Color passableOccState = new Color(0.61f, 0.84f, 0.91f);
	private static Color noneOccState = new Color(1f, 1f, 1f);
	private static Color blockedOccState = new Color(0f, 0.75f, 0.95f);
	private static Color dangerousRiskState = new Color(1f, 0.68f, 0f);
	private static Color riskyRiskState = new Color(1f, 0.68f, 0f);
	private static Color noneRiskState = new Color(1f, 1f, 1f);
	private static string buttonContent;

	private void OnEnable()
	{
		char tmpContent = (char)0x25A0;
		buttonContent = tmpContent.ToString();
	}

	public override void OnInspectorGUI()
	{
		obsGridData = (ObsGridData)target;
		
		serializedObject.Update();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical();
		curWidth = EditorGUILayout.IntField(obsGridData.obsGridMatrix.width);
		EditorGUILayout.EndVertical();
		EditorGUILayout.BeginVertical();
		curHeight = EditorGUILayout.IntField(obsGridData.obsGridMatrix.height);
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		RenderMatrix();
		EditorGUILayout.EndVertical();
		CheckForWidthHeightChanges();
		serializedObject.ApplyModifiedProperties();
	}

	private void CheckForWidthHeightChanges()
	{
		bool isDirty = false;
		if(curWidth != obsGridData.obsGridMatrix.width)
		{
			obsGridData.obsGridMatrix.width = curWidth;
			isDirty = true;
		}
		if(curHeight != obsGridData.obsGridMatrix.height)
		{
			obsGridData.obsGridMatrix.height = curHeight;
			isDirty = true;
		}
		if (isDirty)
		{
			obsGridData.obsGridMatrix.CreateMatrix();
		}

	}

	public void RenderMatrix()
	{
		for (int i = 0; i < obsGridData.obsGridMatrix.ObstacleMatrix.Length; i++)
		{

			EditorGUILayout.BeginHorizontal();
			RenderRowOfBoxes(ref obsGridData.obsGridMatrix.ObstacleMatrix[i]);
			EditorGUILayout.EndHorizontal();
		}
	}
	public void RenderRowOfBoxes(ref ObsGridNode[] nodeArray)
	{
		var standardBkgClr = GUI.backgroundColor;
		for(int i = 0; i < nodeArray.Length; i++)
		{
			EditorGUILayout.BeginVertical();
			GUI.backgroundColor = GetColorByOccupiedState(nodeArray[i].occupiedState);
			GUI.color = GetColorByRiskState(nodeArray[i].riskState);
			if (GUILayout.Button(buttonContent))
			{
				var e = Event.current;
				if  (e.button == 0)
				{
					ChangeOccupiedState(ref nodeArray[i]);
				} else if(e.button == 1)
				{
					ChangeRiskState(ref nodeArray[i]);
				}

			}
			GUI.backgroundColor = standardBkgClr;
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			/*EditorGUILayout.BeginVertical();
			if(GUILayout.Button(nodeArray[i].riskState.ToString()))
			{
				ChangeRiskState(ref nodeArray[i]);
				Debug.Log(nodeArray[i].occupiedState);
			}
			EditorGUILayout.EndVertical();*/
		}
	}

	public void ChangeOccupiedState(ref ObsGridNode node)
	{
		node.occupiedState = (node.occupiedState >= NodeOccupiedState.Blocked) ? NodeOccupiedState.None : node.occupiedState + 1;		
	}
	public void ChangeRiskState(ref ObsGridNode node)
	{
		node.riskState = (node.riskState >= NodeRiskState.Dangerous) ? NodeRiskState.None : node.riskState + 1;
	}

	public Color GetColorByOccupiedState(NodeOccupiedState state)
	{
		if (state == NodeOccupiedState.None)
		{
			return noneOccState;
		} else if (state == NodeOccupiedState.Passable)
		{
			return passableOccState;
		} else if (state == NodeOccupiedState.Blocked)
		{
			return blockedOccState;
		}
		else return Color.black;
	}
	public Color GetColorByRiskState(NodeRiskState state)
	{
		if (state == NodeRiskState.None)
		{
			return noneRiskState;
		} else if (state == NodeRiskState.Risky)
		{
			return riskyRiskState;
		} else if (state == NodeRiskState.Dangerous)
		{
			return dangerousRiskState;
		} else
		{
			return Color.black;
		}
	}

	private void RecreateMatrix(ref ObsGridData obsGridData)
	{
		
	}
}