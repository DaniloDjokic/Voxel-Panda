
using UnityEngine;

namespace VoxelPanda.Player.Input
{
	public class InputCalculator : IInputListener
	{
		protected ConstMoveData constMoveData;
		protected DynamicMoveData dynamicMoveData;

		public InputCalculator(RawInput rawInput, ConstMoveData constMoveData, DynamicMoveData dynMoveData)
		{
			rawInput.Subscribe(this);
			this.constMoveData = constMoveData;
			this.dynamicMoveData = dynMoveData;
		}

		public void OnAccelerometerChanged(Vector3 accelerometer)
		{
			throw new System.NotImplementedException();
		}

		public void OnTouchDragged(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public void OnTouchEnded(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public void OnTouchStarted(Vector3 position)
		{
			throw new System.NotImplementedException();
		}
	}
}
