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

		public InputInjector(ConstMoveData cMoveData, DynamicMoveData dMoveData, MoveEvents moveEvents, PlayerElements playerElements, Crusher crusher)
		{
			this.constMoveData = cMoveData;
            this.dynMoveData = dMoveData;
			this.playerElements = playerElements;
			this.moveEvents = moveEvents;
			this.crusher = crusher;
			BindAll();
		}

		public void BindAll()
		{
            var flingCalculator = playerElements.flingCalculator;
            var curveCalculator = playerElements.curveCalculator;
            flingCalculator.Bind(constMoveData, dynMoveData, playerElements.physicsController);
            curveCalculator.Bind(constMoveData, dynMoveData, playerElements.physicsController);

            //Subscribe fling listeners
            flingCalculator.Subscribe(playerElements.arrowUI);
			//flingCalculator.Subscribe(playerElements.animationManager);
			//flingCalculator.Subscribe(playerElements.particles);
			//flingCalculator.Subscribe(playerElements.sfx);
			flingCalculator.Subscribe(playerElements.staminaUI);
			//flingCalculator.Subscribe(playerElements.touchDragUI);
			//Subscribe curve listeners
			//curveCalculator.Subscribe(playerElements.arrowUI);
			//curveCalculator.Subscribe(playerElements.animationManager);
			//curveCalculator.Subscribe(playerElements.sfx);
			//curveCalculator.Subscribe(playerElements.particles);
			//Subscribe move events listeners
			//moveEvents.Subscribe(playerElements.animationManager);
			//moveEvents.Subscribe(playerElements.particles);
			//moveEvents.Subscribe(playerElements.sfx);
			//Bind Dynamic Move Data to physics Applier
			playerElements.physicsController.Bind(dynMoveData);

			//Bind specific
			playerElements.camBehaviour.Rebind(playerElements.playerTransform, crusher.transform);
		}
	}
}

