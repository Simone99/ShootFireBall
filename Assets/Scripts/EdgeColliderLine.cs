using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderLine : MonoBehaviour
{
    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public EdgeCollider2D edges;
    public int lengthOfLineRenderer;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        List<Vector2> listPoints = new List<Vector2>();
        lengthOfLineRenderer = edges.GetPoints(listPoints);
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.01f;
        lineRenderer.positionCount = lengthOfLineRenderer;
        Vector3[] tmp = new Vector3[lengthOfLineRenderer];
        for(int i = 0; i < lengthOfLineRenderer; i++){
            tmp[i] = (Vector3)listPoints[i];
        }
        lineRenderer.SetPositions(tmp);
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        lineRenderer.sortingOrder = 4;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }
}
