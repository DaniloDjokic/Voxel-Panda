using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

public class ArrowUI : MonoBehaviour, IFlingListener, ICurveListener
{
    public LineRenderer lineRenderer;
    public int positionCount;

    public float visualModLimit;
    public float visualModStep;

    Vector3[] positions;

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

        Vector3 endPoint = flingData.TransposedVectorEndPosition;
        Vector3 offset = (endPoint - flingData.PlayerPosition) * visualModStep;

        lineRenderer.SetPosition(1, flingData.PlayerPosition + Vector3.ClampMagnitude(offset, flingData.MaxCurrentFlingVector.magnitude * visualModLimit));
    }

    public void OnFlingStarted(FlingData flingData)
    {
        lineRenderer.enabled = true;
    }

    public void OnStaminaChanged(FlingData flingData)
    { }

    public void OnCurveChanged(CurveData curveData)
    {
        throw new System.NotImplementedException();
    }
}

