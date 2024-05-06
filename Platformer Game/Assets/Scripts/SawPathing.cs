using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawPathing : MonoBehaviour
{
    public List<Transform> pathPositions = new();
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    private int ForBack = 0;
    private int pathProg = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(pathPositions[0].position, pathPositions[1].position);
        transform.position = pathPositions[0].position;
    }

    void Update()
    {
        for (int i = 0; i < pathPositions.Count; i++)
        {
            if (i != pathPositions.Count - 1)
            {
                Debug.DrawLine(pathPositions[i].position, pathPositions[i + 1].position, Color.red);
            }
        }

        float distCovered = (Time.time - startTime) * speed;

        float fractionOfJourney = distCovered / journeyLength;

        if (ForBack == 0 && pathProg != pathPositions.Count - 1)
        {
            transform.position = Vector3.Lerp(pathPositions[pathProg].position, pathPositions[pathProg + 1].position, fractionOfJourney);
        }

        if (ForBack == 1 && pathProg != 0)
        {
            transform.position = Vector3.Lerp(pathPositions[pathProg].position, pathPositions[pathProg - 1].position, fractionOfJourney);
        }

        if (transform.position != pathPositions[pathPositions.Count - 1].position)
        {
            if (fractionOfJourney >= 1 && ForBack == 0 && transform.position == pathPositions[pathProg + 1].position)
            {
                pathProg += 1;
                startTime = Time.time;
                journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg + 1].position);
            }
        }

        if (transform.position != pathPositions[0].position)
        {
            if (fractionOfJourney >= 1 && ForBack == 1 && transform.position == pathPositions[pathProg - 1].position)
            {
                pathProg -= 1;
                startTime = Time.time;
                journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg - 1].position);
            }
        }

        if (transform.position == pathPositions[pathPositions.Count - 1].position)
        {
            pathProg = pathPositions.Count - 1;
            ForBack = 1;
            startTime = Time.time;
            journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg - 1].position);
        }
        if (transform.position == pathPositions[0].position)
        {
            pathProg = 0;
            ForBack = 0;
            startTime = Time.time;
            journeyLength = Vector3.Distance(pathPositions[pathProg].position, pathPositions[pathProg + 1].position);
        }

        Debug.Log($"PathProg: {pathProg}, ForBack: {ForBack}");
    }

}
