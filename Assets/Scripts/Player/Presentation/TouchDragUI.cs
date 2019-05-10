using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;
using UnityEngine.UI;

namespace VoxelPanda.Player.Presentation
{
	public class TouchDragUI : MonoBehaviour, IFlingListener
	{
        public Image touchStart;
        public Image touchEnd;

        public float visualModifier;

        void Start()
        {
            ToggleImages(false);
        }

        public void ToggleImages(bool showing)
        { 
            touchStart.enabled = showing;
            touchEnd.enabled = showing;
        }

		public void OnFlingEnded(FlingData flingData)
		{
            ToggleImages(false);
		}

		public void OnFlingRunning(FlingData flingData)
		{
            Vector3 newPos = flingData.unmodifiedTouchEndPosition;

            Vector3 offset = (newPos - touchStart.transform.position);
            touchEnd.transform.position = touchStart.transform.position + Vector3.ClampMagnitude(offset, flingData.MaxFlingVector.magnitude * visualModifier);
        }
        
		public void OnFlingStarted(FlingData flingData)
		{
            ToggleImages(true);
            touchStart.transform.position = flingData.unmodifiedTouchStartingPosition;
		}

        public void OnStaminaChanged(FlingData flingData)
        {
            
        }
	}
}

