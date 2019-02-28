using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

[CustomEditor(typeof(GridMatrix))]
public class ObsGridDataEditor : Editor {
	private int curWidth, curHeight;
	private GridMatrix obsGridMatrix;

	private static Color passableOccState = new Color(0.61f, 0.84f, 0.91f);
	private static Color noneOccState = new Color(1f, 1f, 1f);
	private static Color blockedOccState = new Color(0f, 0.75f, 0.95f);
	private static Color dangerousRiskState = new Color(0.68f, 0f , 0f );
	private static Color riskyRiskState = new Color(1f, 0.68f, 0f);
	private static Color noneRiskState = new Color(1f, 1f, 1f);
	private static Color rootObjBorderColor = new Color(0.98f, 0.4f, 0.79f);
	private static string buttonContent;
	private Dictionary<NodeOccupiedState, Dictionary<NodeRiskState, GUIStyle>> stylesDict = new Dictionary<NodeOccupiedState, Dictionary<NodeRiskState, GUIStyle>>();

	private void OnEnable()
	{
		char tmpContent = (char)0x25A0;
		buttonContent = tmpContent.ToString();
	}

	public override void OnInspectorGUI()
	{
		obsGridMatrix = (GridMatrix)target;		
		serializedObject.Update();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical();
		curWidth = EditorGUILayout.IntField(obsGridMatrix.width);
		EditorGUILayout.EndVertical();
		EditorGUILayout.BeginVertical();
		curHeight = EditorGUILayout.IntField(obsGridMatrix.height);
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		RenderMatrix();
		EditorGUILayout.EndVertical();
		CheckForWidthHeightChanges();
		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty(serializedObject.targetObject);
	}

	private void CheckForWidthHeightChanges()
	{
		bool isDirty = false;
		if(curWidth != obsGridMatrix.width)
		{
			obsGridMatrix.width = curWidth;
			isDirty = true;
		}
		if(curHeight != obsGridMatrix.height)
		{
			obsGridMatrix.height = curHeight;
			isDirty = true;
		}
		if (isDirty)
		{
			obsGridMatrix.CreateMatrix();
		}

	}

	public void RenderMatrix()
	{
		if (obsGridMatrix.width != 0 && obsGridMatrix.height != 0) {
			for (int i = obsGridMatrix.ObstacleMatrix.Count - 1; i >= 0 ; i--)
			{

				EditorGUILayout.BeginHorizontal();
				RenderRowOfBoxes(obsGridMatrix.ObstacleMatrix[i]);
				EditorGUILayout.EndHorizontal();
			}
		}
	}
	public void RenderRowOfBoxes(NodeList nodeList)
	{
		List<GridNode> nodes = nodeList.nodes;
		for(int i = 0; i < nodes.Count; i++)
		{
			EditorGUILayout.BeginVertical();
			var guiStyle = GetGUIStyle(nodes[i].occupiedState, nodes[i].riskState, nodes[i].objectRoot);
			if (GUILayout.Button(buttonContent, guiStyle))
			{
				var e = Event.current;
				if  (e.button == 0)
				{
					ChangeOccupiedState(nodes[i]);
				} else if(e.button == 1)
				{
					ChangeRiskState(nodes[i]);
				} else if (e.button == 2)
				{
					obsGridMatrix.SetObjectRoot(nodes[i]);
				}

			}

			EditorGUILayout.EndVertical();
			//GUILayout.FlexibleSpace();
		}
	}

	public void ChangeOccupiedState(GridNode node)
	{
		node.occupiedState++;
		if (node.occupiedState > NodeOccupiedState.Blocked)
		{
			node.occupiedState = NodeOccupiedState.None;
		}	
	}
	public void ChangeRiskState(GridNode node)
	{
		node.riskState++;
		if (node.riskState >= NodeRiskState.Critical)
		{
			node.riskState = NodeRiskState.None;
		}
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
	private GUIStyle GetGUIStyle(NodeOccupiedState occState, NodeRiskState riskState, bool isObjectRoot)
	{
		if (!stylesDict.ContainsKey(occState))
		{
			stylesDict.Add(occState, new Dictionary<NodeRiskState, GUIStyle>());
		}
		if (!stylesDict[occState].ContainsKey(riskState))
		{
			GUIStyle style = new GUIStyle(GUI.skin.button);
			style.normal.background = MakeTex(2, 2, GetColorByOccupiedState(occState));
			style.normal.textColor = GetColorByRiskState(riskState);
			style.fontSize = 36;
			style.padding = new RectOffset(4,2,0,4);
			stylesDict[occState].Add(riskState, style);
		}
		if (isObjectRoot)
		{
			GUIStyle borderedStyle = new GUIStyle(stylesDict[occState][riskState]);
			borderedStyle.normal.background = MakeBorderedTex(7, 7, GetColorByOccupiedState(occState), rootObjBorderColor, 3);
			borderedStyle.border = new RectOffset(3, 3, 3, 3);
			return borderedStyle;
		} else
		{
			return stylesDict[occState][riskState];
		}

	}

	private void RecreateMatrix(ref GridData obsGridData)
	{
		
	}

	private Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width * height];
		for(int i = 0; i < pix.Length; ++i)
		{
			pix[i] = col;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}

	private Texture2D MakeBorderedTex(int width, int height, Color inner, Color outer, int borderWidth)
	{
		Color[] pix = new Color[width * height];
		for(int i = 0; i < pix.Length; ++i)
		{
			if (i < width * borderWidth || i % height < borderWidth || i % height > (height - borderWidth) || i >= (width * (height - borderWidth)))
			{
				pix[i] = outer;
			} else
			{
				pix[i] = inner;
			}
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}
}