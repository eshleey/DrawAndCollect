using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject LinePrefab;
    public GameObject Line;

    public LineRenderer LineRenderer;
    public EdgeCollider2D EdgeCollider;
    public List<Vector2> FingerPositionList;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 FingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(FingerPosition, FingerPositionList[^1]) > 0.1f)
            {
                UpdateLine(FingerPosition);
            }
        }
    }

    void CreateLine()
    {
        Line = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);
        LineRenderer = Line.GetComponent<LineRenderer>();
        EdgeCollider = Line.GetComponent<EdgeCollider2D>();
        FingerPositionList.Clear();
        FingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        LineRenderer.SetPosition(0, FingerPositionList[0]);
        LineRenderer.SetPosition(1, FingerPositionList[1]);
        EdgeCollider.points = FingerPositionList.ToArray();
    }

    void UpdateLine(Vector2 IncomingFingerPosition)
    {
        FingerPositionList.Add(IncomingFingerPosition);
        LineRenderer.positionCount++;
        LineRenderer.SetPosition(LineRenderer.positionCount - 1, IncomingFingerPosition);
        EdgeCollider.points = FingerPositionList.ToArray();
    }
}
