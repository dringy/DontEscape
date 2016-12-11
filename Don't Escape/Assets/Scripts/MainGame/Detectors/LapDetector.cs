using UnityEngine;
using System.Collections;

//Detects if we have done a full lap of the room by visiting each corner
public class LapDetector : MonoBehaviour {

    [SerializeField]
    private float leftEdge;

    [SerializeField]
    private float rightEdge;

    [SerializeField]
    private float topEdge;

    [SerializeField]
    private float bottomEdge;

    [SerializeField]
    private float maxEdgeDelta;

    //Corner flags
    private bool visitedBottomLeftCorner;
    private bool visitedBottomRightCorner;
    private bool visitedTopLeftCorner;
    private bool visitedTopRightCorner;

    private void Start()
    {
        Deactivate();
    }

    private void Reset()
    {
        visitedBottomLeftCorner = false;
        visitedBottomRightCorner = false;
        visitedTopLeftCorner = false;
        visitedTopRightCorner = false;
    }

    //Check for each corner by combing edges
    private void Update()
    {
        Vector3 pos = Player.get.transform.position;

        bool on_left_edge = pos.x <= leftEdge + maxEdgeDelta;
        bool on_right_edge = pos.x >= rightEdge - maxEdgeDelta;
        bool on_bottom_edge = pos.y <= bottomEdge + maxEdgeDelta;
        bool on_top_edge = pos.y >= topEdge - maxEdgeDelta;

        if (on_left_edge)
        {
            if (on_top_edge)
            {
                visitedTopLeftCorner = true;
            }
            else if (on_bottom_edge)
            {
                visitedBottomLeftCorner = true;
            }
        }
        else if (on_right_edge)
        {
            if (on_top_edge)
            {
                visitedTopRightCorner = true;
            }
            else if (on_bottom_edge)
            {
                visitedBottomRightCorner = true;
            }
        }
    }

    public void Activate()
    {
        Reset();
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
        Reset();
    }

    public bool LapDetected()
    {
        return visitedBottomLeftCorner && visitedBottomRightCorner &&
            visitedTopLeftCorner && visitedTopRightCorner;
    }

#if UNITY_EDITOR
    //Visualisation
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((rightEdge + leftEdge) / 2.0f, (topEdge + bottomEdge) / 2.0f),
            new Vector3(Mathf.Abs(rightEdge - leftEdge), Mathf.Abs(topEdge - bottomEdge)));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3((rightEdge + leftEdge) / 2.0f, (topEdge + bottomEdge) / 2.0f),
            new Vector3(Mathf.Abs(rightEdge - leftEdge - (maxEdgeDelta * 2)), 
            Mathf.Abs(topEdge - bottomEdge - (maxEdgeDelta * 2))));
    }
#endif
}
