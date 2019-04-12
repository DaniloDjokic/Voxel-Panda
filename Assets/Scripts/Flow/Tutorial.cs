using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Flow
{
	public enum TutorialState { Movement, Curve, Finished }
	public class Tutorial : MonoBehaviour, IMoveListener, ICurveListener
	{
		public GameObject MovementTutorialAnimation;
		public GameObject CurveTutorialAnimation;
		public float minTimeToShowAnimation;
		private float timer = 0f;
		private bool currentTutorialFinished = true;

		private const string TutorialStateKey = "Tutorial_State";

		private TutorialState tutorialState;

		private void Start()
		{
			tutorialState = GetState();
			ShowTutorial();
		}

		private void Update()
		{
			UpdateTimer();
			ShowTutorial();
		}

		private TutorialState GetState()
		{
			if(!PlayerPrefs.HasKey(TutorialStateKey))
			{
				PlayerPrefs.SetInt(TutorialStateKey, (int)TutorialState.Movement);
				return TutorialState.Movement;
			} else
			{
				return (TutorialState)PlayerPrefs.GetInt(TutorialStateKey);
			}
		}
		private void SetState(TutorialState state)
		{
			tutorialState = state;
			PlayerPrefs.SetInt(TutorialStateKey, (int)tutorialState);
		}
		public static void ResetTutorial()
		{
			PlayerPrefs.SetInt(TutorialStateKey, (int)TutorialState.Movement);
		}

		private void UpdateTimer()
		{
			if (timer > 0f)
			{
				timer -= Time.deltaTime;
			}
		}

		public void ShowTutorial()
		{
			if (timer <= 0f && currentTutorialFinished)
			{
				currentTutorialFinished = false;
				MovementTutorialAnimation.SetActive(false);
				CurveTutorialAnimation.SetActive(false);
				if(tutorialState == TutorialState.Movement)
				{
					MovementTutorialAnimation.SetActive(true);
					timer = minTimeToShowAnimation;
				} else if (tutorialState == TutorialState.Curve)
				{
					CurveTutorialAnimation.SetActive(true);
					timer = minTimeToShowAnimation;
				}
			}
		}

		public void OnCurveChanged(CurveData curveData)
		{
			if(tutorialState == TutorialState.Curve)
			{
				currentTutorialFinished = true;
				SetState(TutorialState.Finished);
			}
		}

		public void OnPositionChanged(Vector3 position)
		{

			if (tutorialState == TutorialState.Movement)
			{
				currentTutorialFinished = true;
				SetState(TutorialState.Curve);
			}
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
		}

		public bool IsTutorialFinished()
		{
			return tutorialState == TutorialState.Finished;
		}
	}
}
