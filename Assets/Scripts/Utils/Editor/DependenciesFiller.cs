using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoxelPanda.Flow;
using VoxelPanda.Player;
using VoxelPanda.Player.Input;
using VoxelPanda.Player.Presentation;
using VoxelPanda.Score;

namespace VoxelPanda.Editor
{
	
	public class DependenciesFiller : EditorWindow
	{
		private Vector2 scrollPos;
		private bool fillInjector = true;
		private bool fillPlayerElements = true;

		[MenuItem("Window/Fill Dependencies")]
		static void Init()
		{
			DependenciesFiller dFiller = GetWindow<DependenciesFiller>();
			dFiller.Show();
		}

		private void OnGUI()
		{
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.BeginVertical();
			fillInjector = EditorGUILayout.Toggle(fillInjector);
			EditorGUILayout.EndVertical();
			EditorGUILayout.BeginVertical();
			fillPlayerElements = EditorGUILayout.Toggle(fillPlayerElements);
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Fill dependencies"))
			{
				FillDependencies();
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndScrollView();
		}

		private void FillDependencies()
		{
			if (fillInjector) { FillInjector(); }
			if (fillPlayerElements) { FillPlayerElements(); }
		}
		private void FillInjector()
		{
			Injector injector = GameObject.FindObjectOfType<Injector>();
			injector.constMoveData = GameObject.FindObjectOfType<ConstMoveData>();
			injector.spawnData = FindObjectOfType<SpawnData>();
			injector.scoreUI = FindObjectOfType<ScoreUI>();
			injector.deathUI = FindObjectOfType<DeathUI>();
			injector.playerElements = FindObjectOfType<PlayerElements>();
			injector.crusher = FindObjectOfType<Crusher>();
            injector.dMoveData = GameObject.FindObjectOfType<DynamicMoveData>();
		}
		private void FillPlayerElements()
		{
			PlayerElements playerElements = GameObject.FindObjectOfType<PlayerElements>();
			playerElements.physicsController = GameObject.FindObjectOfType<PhysicsController>();
			playerElements.playerTransform = playerElements.physicsController.transform;
			playerElements.accInput = GameObject.FindObjectOfType<RawAccInput>();
            playerElements.touchInput = GameObject.FindObjectOfType<RawTouchInput>();

            playerElements.animationManager = GameObject.FindObjectOfType<AnimationManager>();
			playerElements.arrowUI = GameObject.FindObjectOfType<ArrowUI>();
			playerElements.camBehaviour = GameObject.FindObjectOfType<CamBehaviour>();
			playerElements.particles = GameObject.FindObjectOfType<Particles>();
			playerElements.sfx = GameObject.FindObjectOfType<SFX>();
			playerElements.staminaUI = GameObject.FindObjectOfType<StaminaUI>();
			playerElements.touchDragUI = GameObject.FindObjectOfType<TouchDragUI>();
            playerElements.flingCalculator = GameObject.FindObjectOfType<FlingCalculator>();
            playerElements.curveCalculator = GameObject.FindObjectOfType<CurveCalculator>();
        }
	}
}

