using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Score;

namespace VoxelPanda.ProcGen.Elements
{
	public class Coin : Pickup
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                scoreCalculator.PickupCoin();
                this.Despawn();
            } 
        }
    }
}