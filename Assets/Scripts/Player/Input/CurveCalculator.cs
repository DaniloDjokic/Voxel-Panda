using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Input
{
    public class CurveCalculator : InputCalculator
    {  
        private CurveData curveData = new CurveData();

        private List<ICurveListener> listeners = new List<ICurveListener>();

        public void UpdateRawAccelerationVector(Vector3 newAccelerationVector)
        {
            if (newAccelerationVector.magnitude > constMoveData.accelerationDeadzone)
            {
                curveData.RawAccelerationVector = newAccelerationVector;
                curveData.ModifiedAccelerationVector = ModifyAcceleration(newAccelerationVector);

                physicsController.ApplyCurveForce(curveData.ModifiedAccelerationVector * constMoveData.curveForce);
                CurveRunning(curveData);
            }
        }

        private Vector3 ModifyAcceleration(Vector3 rawAccelerationvector)
        {
            return rawAccelerationvector * constMoveData.accelerationFunctionModifier;
        }

        #region Observers  Logic
        public void Subscribe(ICurveListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        void RemoveListener(ICurveListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

        void CurveRunning(CurveData curveData)
        {
            foreach (ICurveListener listener in listeners)
            {
                listener.OnCurveChanged(curveData);
            }
        }
        #endregion
    }
}