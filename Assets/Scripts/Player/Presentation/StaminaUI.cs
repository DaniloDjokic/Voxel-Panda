using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour, IFlingListener
{
    public Slider slider;

    public void OnStaminaChanged(FlingData flingData)
    {
        slider.value = flingData.CurrentStamina / 100f;
    }

    public void OnFlingEnded(FlingData flingData)
    {
        
    }

    public void OnFlingRunning(FlingData flingData)
    {
        
    }

    public void OnFlingStarted(FlingData flingData)
    {
        
    }
}
