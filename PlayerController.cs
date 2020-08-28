using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance = null;
    public float MoveSpeed = 0.1f;
    public float RotationSpeed = 1;
    [SerializeField]
    private float offset = 2f;

    [SerializeField]
    private float heightOffset = 4.243f;

    private bool moving = false;
    private GameObject target;
    private Vector3 targetPosition = new Vector3(0, 0, 0);


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (moving)
        {
            Vector3 direction = target.transform.position - gameObject.transform.position;
            //direction.y = heightOffset;
            Quaternion toRotation = Quaternion.LookRotation(direction);

            if (Vector3.Distance(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z)) < 0.1f && (Quaternion.Angle(transform.rotation, toRotation) < 0.1f))
            {
                moving = false;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), MoveSpeed);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, targetPosition.z), MoveSpeed);
            }
        }
    }

    public void MoveToNext(GameObject positionToMoveTo, GameObject objectToLookAt)
    {
        moving = true;
        target = objectToLookAt;
        targetPosition = positionToMoveTo.transform.position;
        targetPosition.z += offset;
        targetPosition.y += heightOffset;

    }

    public void FocusAt(GameObject objectToLookAt)
    {
        target = objectToLookAt;
        transform.LookAt(target.transform.position);
    }

}
