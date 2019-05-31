using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Flow
{
	[System.Serializable]
	public class DifficultyCurve
	{
		public string remoteSettingsKey = "";
		[SerializeField]
		public float[] difficultyValues = new float[5];
		private static readonly int[] zones = new int[5] { 0, 200, 500, 1000, 2000 };
		//Remote settings
		private const char separator = ',';



		public DifficultyCurve()
		{
		}

		public float GetValue(float score)
		{
			for(int i = zones.Length - 1; i >= 0; i--)
			{
				if(score > zones[i])
				{
					return difficultyValues[i];
				}
			}
			return 0;
		}

		public void CheckForRemoteUpdates()
		{
			if (!string.IsNullOrWhiteSpace(remoteSettingsKey))
			{

				string diffValuesCombined = RemoteSettings.GetString(remoteSettingsKey, GetDefaultDifficultyValues());
				string[] diffValuesStringArray = diffValuesCombined.Split(separator);
				for(int i = 0; i < diffValuesStringArray.Length; i++)
				{
					this.difficultyValues[i] = float.Parse(diffValuesStringArray[i]);
				}
			}
		}

		private string GetDefaultDifficultyValues()
		{
			return string.Join(separator.ToString(), Array.ConvertAll(difficultyValues, x => x.ToString()));
		}
	}
}
