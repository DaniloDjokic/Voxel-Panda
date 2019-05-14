
using UnityEngine;

namespace VoxelPanda.Player.Input
{
	public class InputCalculator : MonoBehaviour, IInputListener
    {
		public ConstMoveData constMoveData;
        public DynamicMoveData dynamicMoveData;
        public PhysicsController physicsController;

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

        public void Bind(ConstMoveData cMoveData, DynamicMoveData dMoveData, PhysicsController physicsController)
        {
            this.constMoveData = cMoveData;
            this.dynamicMoveData = dMoveData;
            this.physicsController = physicsController;
        }
	}
}
