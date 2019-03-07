using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

public class ArrowUI : MonoBehaviour, IFlingListener
{
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    public void OnFlingEnded(FlingData flingData)
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void OnFlingRunning(FlingData flingData)
    {
        lineRenderer.SetPosition(0, flingData.PlayerPosition);
        lineRenderer.SetPosition(1, flingData.TransposedVectorEndPosition);
    }

    public void OnFlingStarted(FlingData flingData)
    {
        lineRenderer.enabled = true;
    }

    public void OnStaminaChanged(FlingData flingData)
    { }
}
