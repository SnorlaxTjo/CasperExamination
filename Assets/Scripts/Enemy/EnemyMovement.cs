using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Vector2 distancesToMove;
    [SerializeField] float timeToMove;
    [SerializeField] float timeToWait;

    bool isWalkingBack;
    bool isWaiting;
    float timeSpentOnCurrentThing;
    Vector3 originalPosition;

    public Vector2 DistancesToMove { get { return distancesToMove; } set { distancesToMove = value; } }
    public float TimeToMove { get {  return timeToMove; } set {  timeToMove = value; } }
    public float TimeToWait { get { return timeToWait; } set { timeToWait = value; } }

    private void Start()
    {
        originalPosition = transform.position;
    }

    //Moves the enemy between point A and point B, and then back from point B to point A
    private void Update()
    {
        timeSpentOnCurrentThing += Time.deltaTime;

        if (!isWalkingBack && !isWaiting)
        {
            transform.position += new Vector3(distancesToMove.x / timeToMove * Time.deltaTime, 0, distancesToMove.y / timeToMove * Time.deltaTime);

            if (timeSpentOnCurrentThing >= timeToMove)
            {
                transform.position = new Vector3(originalPosition.x + distancesToMove.x, originalPosition.y, originalPosition.z + distancesToMove.y);
                timeSpentOnCurrentThing = 0;
                isWaiting = true;
                isWalkingBack = true;
            }
        }
        else if (isWalkingBack && !isWaiting)
        {
            transform.position -= new Vector3(distancesToMove.x / timeToMove * Time.deltaTime, 0, distancesToMove.y / timeToMove * Time.deltaTime);

            if (timeSpentOnCurrentThing >= timeToMove)
            {
                transform.position = originalPosition;
                timeSpentOnCurrentThing = 0;
                isWaiting = true;
                isWalkingBack = false;
            }
        }
        else if (isWaiting)
        {
            if (timeSpentOnCurrentThing >= timeToWait)
            {
                timeSpentOnCurrentThing = 0;
                isWaiting = false;
            }
        }
    }

    //Shows a nice sphere where point B is
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + distancesToMove.x, 0, transform.position.z + distancesToMove.y), 1);
    }
}
