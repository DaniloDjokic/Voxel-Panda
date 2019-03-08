using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Input
{

	public class FlingCalculator : InputCalculator
	{
        private List<IFlingListener> listeners = new List<IFlingListener>();
        
        private FlingData flingData = new FlingData();
        private Vector3 rawVector;

        #region UnityFunctions
        private void Start()
        {
            Init();
        }

        private void Update()
        {
            RefillStamina();
        }
        #endregion

        public void StartFlingCalculation()
        {
            flingData.PlayerPosition = dynamicMoveData.currentPosition;
            FlingStarted(flingData);
        }

        public void UpdateRawVector(Vector3 rawVector)
        {
            this.rawVector = rawVector;
            flingData.RawFlingVector = rawVector;
            flingData.PlayerPosition = dynamicMoveData.currentPosition;
            flingData.TransposedVectorEndPosition = dynamicMoveData.currentPosition + rawVector;
            FlingRunning(flingData);
        }

        public void ApplyFling()
        {
            if(dynamicMoveData.currentStamina > constMoveData.staminaPerFling)
            {
                if (dynamicMoveData.currentStamina > (rawVector.magnitude * constMoveData.vectorStaminaCost))
                {
                    SpendStamina(rawVector);
                    physicsController.ApplyFlingForce(rawVector * constMoveData.flingForce);
                }
                else
                {
                    SpendStamina(CompressVector(rawVector));
                    physicsController.ApplyCurveForce(CompressVector(rawVector * constMoveData.flingForce));
                }
                FlingEnded(flingData);
            }         
        }

        private void Init()
        {
            dynamicMoveData.currentStamina = constMoveData.maxStamina;

            Debug.Log(listeners);
        }

        private Vector3 CompressVector(Vector3 vector)
        {
            return Vector3.ClampMagnitude(vector, dynamicMoveData.currentStamina * constMoveData.vectorStaminaCost);
        }

        private void SpendStamina(Vector3 flingVector)
        {
            dynamicMoveData.currentStamina -= constMoveData.staminaPerFling;
            dynamicMoveData.currentStamina -= flingVector.magnitude * constMoveData.vectorStaminaCost;
            dynamicMoveData.currentStamina = Mathf.Clamp(dynamicMoveData.currentStamina, 0f, constMoveData.maxStamina);

            StaminaChanged();
        }

        private void RefillStamina()
        {
            if (dynamicMoveData.currentStamina < constMoveData.maxStamina)
            {
                dynamicMoveData.currentStamina += (constMoveData.staminaRegenPerSecond) * Time.deltaTime;
                StaminaChanged();
            }              
        }


        #region Obvservers Logic
        public void AddListener(IFlingListener listener)
		{
		}

		public void Subscribe(IFlingListener listener)
		{
			listeners.Add(listener);
		}

		public void RemoveListener(IFlingListener listener)
		{
			if(listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}


        private void StaminaChanged()
        {
            flingData.CurrentStamina = dynamicMoveData.currentStamina;
            for(int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnStaminaChanged(flingData);
            }
        }

        private void FlingRunning(FlingData flingData)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnFlingRunning(flingData);
            }
        }

        private void FlingStarted(FlingData flingData)
		{
			for(int i = 0; i < listeners.Count; i++)
			{
				listeners[i].OnFlingStarted(flingData);
			}
		}

        private void FlingEnded(FlingData flingData)
        {
            for(int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnFlingEnded(flingData);
            }
        }
        #endregion
    }
}

