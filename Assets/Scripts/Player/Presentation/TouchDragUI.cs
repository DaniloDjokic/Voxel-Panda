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
            touchEnd.transform.position = flingData.unmodifiedTouchEndPosition;
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

