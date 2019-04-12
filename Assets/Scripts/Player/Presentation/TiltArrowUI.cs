using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelPanda.Player.Events;

public class TiltArrowUI : MonoBehaviour, ICurveListener
{
    public Image leftArrow;
    public Image rightArrow;

    private void Start()
    {
        SetInvisible();
    }

    public void SetInvisible()
    {
        var color = leftArrow.material.color;
        leftArrow.color = new Color(color.r, color.g, color.b, 0f);
        rightArrow.color = new Color(color.r, color.g, color.b, 0f);
    }

    private void ChangeColor(Image arrow, float curve)
    {
        var color = arrow.color;
        color.a = curve;
        arrow.color = color;
    }

    public void OnCurveChanged(CurveData curveData)
    {
        float curve = curveData.ModifiedAccelerationVector.x;

        if(curve < 0.25f && curve > -0.25f)
        {
            Debug.Log("KDSA");
            SetInvisible();
            return;
        }
        if (curve > 0f)
        {
            ChangeColor(rightArrow, Mathf.Abs(curve));
        }
        else if (curve < 0f)
        {
            ChangeColor(leftArrow, Mathf.Abs(curve));
        }
    }
}
