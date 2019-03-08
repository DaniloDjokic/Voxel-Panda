using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player;
using VoxelPanda.Player.Events;
using VoxelPanda.Player.Input;

namespace VoxelPanda.Flow
{
	public class InputInjector
	{
		private ConstMoveData constMoveData;
		private DynamicMoveData dynMoveData;
		private PlayerElements playerElements;
		private MoveEvents moveEvents;
		private Crusher crusher;

		public InputInjector(ConstMoveData cMoveData, MoveEvents moveEvents, PlayerElements playerElements, Crusher crusher)
		{
			this.constMoveData = cMoveData;
			this.dynMoveData = new DynamicMoveData();
			this.playerElements = playerElements;
			this.moveEvents = moveEvents;
			this.crusher = crusher;
			BindAll();
		}

		public void BindAll()
		{
			var flingCalculator = new FlingCalculator(playerElements.rawInput, constMoveData, dynMoveData);
			var curveCalculator = new CurveCalculator(playerElements.rawInput, constMoveData, dynMoveData);

			//Subscribe fling listeners
			flingCalculator.Subscribe(playerElements.arrowUI);
			flingCalculator.Subscribe(playerElements.animationManager);
			flingCalculator.Subscribe(playerElements.particles);
			flingCalculator.Subscribe(playerElements.sfx);
			flingCalculator.Subscribe(playerElements.staminaUI);
			flingCalculator.Subscribe(playerElements.touchDragUI);
			flingCalculator.Subscribe(playerElements.physicsApplier);
			//Subscribe curve listeners
			curveCalculator.Subscribe(playerElements.arrowUI);
			curveCalculator.Subscribe(playerElements.animationManager);
			curveCalculator.Subscribe(playerElements.sfx);
			curveCalculator.Subscribe(playerElements.particles);
			curveCalculator.Subscribe(playerElements.physicsApplier);
			//Subscribe move events listeners
			moveEvents.Subscribe(playerElements.animationManager);
			moveEvents.Subscribe(playerElements.particles);
			moveEvents.Subscribe(playerElements.sfx);
			//Bind Dynamic Move Data to physics Applier
			playerElements.physicsApplier.Bind(dynMoveData);

			//Bind specific
			playerElements.camBehaviour.Rebind(playerElements.playerTransform, crusher.transform);
		}
	}
}

