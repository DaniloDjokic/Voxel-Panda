using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

public class ArrowUI : MonoBehaviour, IFlingListener, ICurveListener
{
    public LineRenderer lineRenderer;
    public int lineSmoothnes;

    private Vector3[] positions;

    private void Start()
    {
        lineRenderer.enabled = false;

        positions = new Vector3[lineSmoothnes];
        lineRenderer.positionCount = lineSmoothnes;
    }

    public void OnFlingEnded(FlingData flingData)
    {
        lineRenderer.enabled = false;
        ResetPositions();
    }

    public void OnFlingRunning(FlingData flingData)
    {
        SetStraightPositions(flingData);
    }

    public void OnFlingStarted(FlingData flingData)
    {
        lineRenderer.enabled = true;
    }

    public void OnStaminaChanged(FlingData flingData)
    { }

    public void OnCurveChanged(CurveData curveData)
    {
        SetCurvedPositions(curveData);
    }

    void ResetPositions()
    {
        for (int i = 0; i < lineSmoothnes; i++)
            lineRenderer.SetPosition(i, Vector3.zero);
    }

    void SetStraightPositions(FlingData flingData)
    {
        float lineLength = Vector3.Distance(flingData.TransposedVectorEndPosition, flingData.PlayerPosition);
        float posDistance = lineLength / lineSmoothnes;

        Vector3 offset = Vector3.zero;
        Vector3 dir = (flingData.TransposedVectorEndPosition - flingData.PlayerPosition).normalized;

        for(int i = 0; i < lineSmoothnes; i++)
        {
            lineRenderer.SetPosition(i, transform.position + offset);
            offset += dir * posDistance;
        }
    }

    void SetCurvedPositions(CurveData curveData)
    {
        lineRenderer.SetPosition(lineSmoothnes - 1, lineRenderer.GetPosition(lineSmoothnes - 1) + curveData.ModifiedAccelerationVector);
    }
}

