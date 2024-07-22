using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DrawLine : MonoBehaviour
{
    public GameObject LinePrefab;
    public GameObject Line;

    public LineRenderer LineRenderer;
    public EdgeCollider2D EdgeCollider;
    public List<Vector2> FingerPositionList;

    public List<GameObject> Lines;
    bool CanItBeDrawn;

    private void Start()
    {
        CanItBeDrawn = false;
    }

    void Update()
    {
        if (CanItBeDrawn)
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
    }

    void CreateLine()
    {
        Line = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);
        Lines.Add(Line);
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

    public void Continue()
    {
        foreach (var line in Lines)
        {
            Destroy(line.gameObject);
        }
        Lines.Clear();
    }

    public void StopDrawLine()
    {
        CanItBeDrawn = false;
    }

    public void StartDrawLine()
    {
        CanItBeDrawn = true;
    }
}
