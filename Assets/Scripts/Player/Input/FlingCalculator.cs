using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Input
{

	public class FlingCalculator : InputCalculator
	{
        private List<IFlingListener> listeners = new List<IFlingListener>();

        private FlingData flingData;
        private Vector3 rawVector;

        #region UnityFunctions
        private void Start()
        {
            Init();
            flingData = new FlingData();
            
        }

        private void Update()
        {
            RefillStamina();
        }
        #endregion

        public void StartFlingCalculation(Vector3 initialTouchPosition)
        {
            //TODO: Sync with options setting vector stamina cost
            flingData.MaxFlingVector = Vector3.forward * (GetUsableMaxStamina() / constMoveData.vectorStaminaCost);

            flingData.unmodifiedTouchStartingPosition = initialTouchPosition;
            flingData.PlayerPosition = dynamicMoveData.currentPosition;
            FlingStarted(flingData);
        }

        public void UpdateRawVector(Vector3 rawVector, Vector3 touchHoldPos)
        {
            this.rawVector = rawVector;
            flingData.unmodifiedTouchEndPosition = touchHoldPos;
            flingData.RawFlingVector = rawVector;
            flingData.PlayerPosition = dynamicMoveData.currentPosition;
            flingData.TransposedVectorEndPosition = dynamicMoveData.currentPosition + rawVector;

            flingData.ModifiedFlingVector = CompressVector(rawVector);
            flingData.MaxCurrentFlingVector = Vector3.forward * (GetUsableStamina() / constMoveData.vectorStaminaCost);

            FlingRunning(flingData);
        }

        public void ApplyFling()
        {
            if(dynamicMoveData.currentStamina > constMoveData.staminaPerFling)
            {
                SpendStamina(flingData.ModifiedFlingVector);
                physicsController.ApplyFlingForce(flingData.ModifiedFlingVector * constMoveData.flingForce);
                FlingEnded(flingData);
            }         
        }

        private void Init()
        {
            dynamicMoveData.currentStamina = constMoveData.maxStamina;
        }

        private Vector3 CompressVector(Vector3 vector)
        {
                Vector3 temp;
                temp = Vector3.ClampMagnitude(vector, GetUsableStamina() / constMoveData.vectorStaminaCost);
                return temp;
            
        }

        private void SpendStamina(Vector3 flingVector)
        {
            dynamicMoveData.currentStamina -= constMoveData.staminaPerFling;
            dynamicMoveData.currentStamina -= flingVector.magnitude * constMoveData.vectorStaminaCost;
            dynamicMoveData.currentStamina = Mathf.Clamp(dynamicMoveData.currentStamina, 0f, constMoveData.maxStamina);

            StaminaChanged();
        }

        private float GetUsableStamina()
        {
            return dynamicMoveData.currentStamina - constMoveData.staminaPerFling;
        }

        private float GetUsableMaxStamina()
        {
            return constMoveData.maxStamina - constMoveData.staminaPerFling;
        }

        private void RefillStamina()
        {
            if (dynamicMoveData.currentStamina < constMoveData.maxStamina)
            {
                dynamicMoveData.currentStamina += constMoveData.staminaRegenPerSecond * Time.deltaTime;
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

