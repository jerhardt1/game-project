using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WalkState : BaseState
{
    private Enemy _enemy;
    private bool _triggerSet = false;

    public WalkState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }


    public override Type Tick()
    {


        if (_enemy.isAlive)
        {
            if (tooDeep())
            {
                transform.localPosition = new Vector3(0, 0, 0);
                Debug.Log("pos reseted");
            }
            if (tooFarLeft() || tooFarRight())
            {
                Vector3 target =  Camera.main.transform.position;
                target.z -= 10f;
                target.y = 0.0f;
                LookAt(target);
                Walk();
                return typeof(WalkState);
            }


            else
            {

                if (Vector3.Distance(transform.position, Player.instance.transform.position) >= 10f)
                {
                    if (!_triggerSet)
                    {
                        animator.SetTrigger("run");
                        _triggerSet = true;
                    }
                    LookAt(Player.instance.transform.position);
                    Walk();
                    return typeof(WalkState);
                }
                else
                {
                    return typeof(IdleState);
                }
            }

        }
        else
        {
            return typeof(DeathState);
        }
        

    }

    private void LookAt(Vector3 aTarget)
    {
        Vector3 direction = aTarget - transform.position;
        direction.y = 0.0f;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 4 * Time.deltaTime);
    }


    private void Walk()
    {    
        _enemy.transform.Translate(Vector3.forward * Time.deltaTime * 8);

        Vector3 origin = transform.position;
        origin.y += 0.5f;

        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.tag == "ramp")
            {
                
                Vector3 rampVector = Vector3.Lerp(transform.position, hit.point, 0.1f);
                transform.position = new Vector3(transform.position.x, rampVector.y, transform.position.z);
            }
        }
    }

    private bool tooFarRight()
    {
        Camera camera = Camera.main;
        Vector3 objectPos = camera.WorldToViewportPoint(transform.position);

        if (objectPos.x >= 0.9f)
        {
            Debug.Log("Too far right");
            return true;
        }

        return false;
    }

    private bool tooFarLeft()
    {
        Camera camera = Camera.main;
        Vector3 objectPos = camera.WorldToViewportPoint(transform.position);

        if (objectPos.x <= 0.1f)
        {
            Debug.Log("Too far left");
            return true;
        }

        return false;
    }

    private bool tooDeep()
    {
        Camera camera = Camera.main;
        Vector3 objectPos = camera.WorldToViewportPoint(transform.position);

        if (objectPos.z <= 6f)
        {
            Debug.Log("Too deep");
            return true;
        }

        return false;
    }

}
