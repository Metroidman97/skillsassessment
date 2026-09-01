using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float speed = 25f;

    public Vector3 centerPoint;

    public Vector3 movementRange = new Vector3(50f, 50f, 50f);

    public Vector3 targetPosition;

    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        if (centerPoint == Vector3.zero)
        {
            centerPoint = transform.position;
        }

        isMoving = false;

        StartCoroutine(MovementRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
        }
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            targetPosition = GetRandomPosition();

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                yield return null;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-movementRange.x / 2f, movementRange.x / 2f);
        float randomY = Random.Range(-movementRange.y / 2f, movementRange.y / 2f);
        float randomZ = Random.Range(-movementRange.z / 2f, movementRange.z / 2f);

        return centerPoint + new Vector3(randomX, randomY, randomZ);
    }

    public void StartTarget()
    {
        isMoving = true;
    }

    public void ResetTarget()
    {
        isMoving = false;
        transform.position = centerPoint;
    }
}
