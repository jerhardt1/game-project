using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    public float RotationSpeed = 1;
    private bool move = false;
    private GameObject target;
    private Vector3 targetPosition = new Vector3 (0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Vector3 direction = target.transform.position - gameObject.transform.position;
            direction.y = 0.0f;
            Quaternion toRotation = Quaternion.LookRotation(direction);

            if (Vector3.Distance(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z)) < 1.0f && (Quaternion.Angle(transform.rotation, toRotation) < 1.0f))
            {
                move = false;
            }
            else
            {
                
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), MoveSpeed);
            }
            
            
        }
    }

    public void MoveToNext(GameObject positionToMoveTo, GameObject objectToLookAt)
    {
        move = true;
        target = objectToLookAt;
        targetPosition = positionToMoveTo.transform.position;


    }

    public void FocusAt(GameObject objectToLookAt)
    {
        target = objectToLookAt;
        transform.LookAt(target.transform.position);
    }
}
