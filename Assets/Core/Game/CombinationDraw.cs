using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombinationDraw : MonoBehaviour, Initable
{
    private LineRenderer lineRenderer;
    public void OnInit()
    {
    }
    public void DeInit()
    {
    }
    public void DrawComBination(List<TileView> tileViews)
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = tileViews.Count;
        for (int i = 0; i < tileViews.Count; i++)
        {
            lineRenderer.SetPosition(i, tileViews[i].transform.position);
        }
        Invoke("Deactivate", 5);
    }

}
