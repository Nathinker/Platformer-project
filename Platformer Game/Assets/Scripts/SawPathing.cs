using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawPathing : MonoBehaviour
{
    #region Fields
    public List<Transform> pathPositions = new();
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    private PathDirection pathDirection = PathDirection.Forward;
    private int pathProg = 0;
    #endregion

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(pathPositions[0].position, pathPositions[1].position);
        transform.position = pathPositions[0].position;
    }
    #endregion

    #region Update
    void Update()
    {
        // Draw Red Debug Lines between the Path Points, to show the path
        int lastPositionIndex = pathPositions.Count - 1;
        for (int i = 0; i < lastPositionIndex; i++)
        {
            Debug.DrawLine(pathPositions[i].position, pathPositions[i + 1].position, Color.red);
        }

        // Distance Covered from current point to next point on the path
        float distCovered = (Time.time - startTime) * speed;

        // How far along the path the saw has travelled
        float fractionOfJourney = distCovered / journeyLength;

        Vector3 currentPosition = transform.position;
        if (pathDirection == PathDirection.Forward && pathProg != lastPositionIndex)
        {
            Debug.Log($"Path Progress Forwards: {pathProg}");
            transform.position = Vector3.Lerp(pathPositions[pathProg].position, pathPositions[pathProg + 1].position, fractionOfJourney);
        }

        if (pathDirection == PathDirection.Backward && pathProg != 0)
        {
            Debug.Log($"Path Progress Backwards: {pathProg}");
            transform.position = Vector3.Lerp(pathPositions[pathProg].position, pathPositions[pathProg - 1].position, fractionOfJourney);
        }

        if (currentPosition != pathPositions[lastPositionIndex].position)
        {
            if (fractionOfJourney >= 1 && PathDirection.Forward == pathDirection && currentPosition == pathPositions[pathProg + 1].position)
            {
                pathProg += 1;
                startTime = Time.time;
                journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg + 1].position);
            }
        }

        if (currentPosition != pathPositions[0].position)
        {
            if (fractionOfJourney >= 1 && PathDirection.Backward == pathDirection && currentPosition == pathPositions[pathProg - 1].position)
            {
                pathProg -= 1;
                startTime = Time.time;
                journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg - 1].position);
            }
        }

        if (currentPosition == pathPositions[lastPositionIndex].position)
        {
            pathProg = lastPositionIndex;
            pathDirection = PathDirection.Backward;
            startTime = Time.time;
            journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg - 1].position);
        }

        if (currentPosition == pathPositions[0].position)
        {
            pathProg = 0;
            pathDirection = PathDirection.Forward;
            startTime = Time.time;
            journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg + 1].position);
        }

        // Debug.Log($"PathProg: {pathProg}, PathDirection: {pathDirection}");
    }
    #endregion
}

enum PathDirection
{
    Forward,
    Backward
}
