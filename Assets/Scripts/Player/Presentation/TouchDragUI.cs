using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Presentation
{
	public class TouchDragUI : MonoBehaviour, IFlingListener
	{

		public void OnFlingEnded(FlingData flingData)
		{
			throw new System.NotImplementedException();
		}

		public void OnFlingRunning(FlingData flingData)
		{
			throw new System.NotImplementedException();
		}

		public void OnFlingStarted(FlingData flingData)
		{
			throw new System.NotImplementedException();
		}

        public void OnStaminaChanged(FlingData flingData)
        {
            throw new System.NotImplementedException();
        }

        // Use this for initialization
        void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}

