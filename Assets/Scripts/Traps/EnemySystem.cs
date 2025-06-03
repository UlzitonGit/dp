using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points;
    [SerializeField] private bool Cycle;
    [SerializeField] private float rotationSpeed, timeOnPoint;
    private Transform enemy;
    private Transform currentPoint;
    private int currentElement;
    private bool forward;
    private Vector3 direction;
    private float stopTimer;

    void Start()
    {
        enemy = transform.GetChild(0);
        forward = true;
        currentElement = 1;
        currentPoint = points[currentElement];
        stopTimer = timeOnPoint;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Handles.DrawLine(points[i].position, points[i + 1].position, 4);
        }
    }
#endif


    void Update()
    {
        direction = currentPoint.position - enemy.position;

        Vector3 newDirection = Vector3.RotateTowards(enemy.forward, direction, Time.deltaTime * rotationSpeed, 0);
        enemy.rotation = Quaternion.LookRotation(newDirection);

        enemy.position = Vector3.MoveTowards(enemy.position, currentPoint.position, speed * Time.deltaTime);

        if(enemy.position == currentPoint.position)
        {
            stopTimer -= Time.deltaTime;

            if(stopTimer <= 0)
            {
                stopTimer = timeOnPoint;

                if (forward)
                    currentElement++;
                else
                    currentElement--;

                if (currentElement >= points.Length && !Cycle)
                {
                    forward = false;
                    currentElement = points.Length - 2;
                }
                else if (currentElement >= points.Length && Cycle)
                {
                    currentElement = 0;
                }
                else if (currentElement < 0)
                {
                    forward = true;
                    currentElement = 1;
                }

                currentPoint = points[currentElement];
            }

            
        }
    }
}
