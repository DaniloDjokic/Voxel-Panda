using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Score;

namespace VoxelPanda.ProcGen.Elements
{
	public class Coin : Pickup
    {
        public string coinTriggerEvent = "Play_Coin";

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                scoreCalculator.PickupCoin();
                AkSoundEngine.PostEvent(coinTriggerEvent, gameObject);
                this.Despawn();
            } 
        }
    }
}