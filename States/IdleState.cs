using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IdleState : BaseState
{

    private Enemy _enemy;
    private Player _player;
    private float _actionTime;
    private bool _triggerSet = false;
    private bool _alerted = false;
    private Vector3 _direction;
    private float _alertDelay = 0.3f;

    public IdleState(Enemy enemy, Player player) : base(enemy.gameObject)
    {
        _enemy = enemy;
        _player = player;
    }

    private void Start()
    {
        _actionTime = _enemy.StatAttackSpeed.Value;


    }

    public override Type Tick()
    {
        if (!_alerted)
        {
            if (_alertDelay >= 0)
            {
                _alertDelay -= Time.deltaTime;           
            }
            else
            {
                animator.SetTrigger("alert");
                _alerted = true;
            }
            return null;

        }


        if (!inPosition())
        {
            return typeof(WalkState);
        }

        LookAtPlayer();

        if (!_triggerSet)
        {
            animator.SetTrigger("idle");
            _triggerSet = true;
        }

        if (!_player.isAlive)
        {
            return null;
        }



        if (_enemy.isAlive)
        {

            _actionTime -= Time.deltaTime;

            if (_actionTime <= 0)
            {
                if (_enemy.isAlive && _player.isAlive)
                {
                    _actionTime = _enemy.StatAttackSpeed.Value;
                    return typeof(AttackState);
                }
            }
        }
        else
        {
            return typeof(DeathState);
        }
        

        return null;
    }

    public void LookAtPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        direction.y = 0.0f;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 8 * Time.deltaTime);
    }

    private bool inPosition()
    {
        if(transform.parent != null)
        {

            if (Vector3.Distance(transform.position, Player.instance.transform.position) >= 10f || tooFarLeft() || tooFarRight())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    private bool facingPlayer()
    {
        _direction = Vector3.Normalize(transform.position - _player.transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(_direction);
        if(Quaternion.Angle(transform.rotation, lookRotation) > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool tooFarRight()
    {
        Camera camera = Camera.main;
        Vector3 objectPos = camera.WorldToViewportPoint(transform.position);

        if (objectPos.x >= 1)
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

        if (objectPos.x <= 0)
        {
            Debug.Log("Too far left");
            return true;
        }

        return false;
    }
}
