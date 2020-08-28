using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackState : BaseState
{
    private Enemy _enemy;
    private Player _player;
    private float _attackReadyTimer;

    public AttackState(Enemy aEnemy, Player aPlayer) : base(aEnemy.gameObject)
    {
        _enemy = aEnemy;
        _player = aPlayer;
    }

    public override Type Tick()
    {
        if (_enemy.isAlive)
        {
            _attackReadyTimer -= Time.deltaTime;

            if (_attackReadyTimer <= 0f)
            {
                Attack();        
            }
        }
        else
        {
            return typeof(DeathState);
        }
        return typeof(IdleState);
    }

    private void Attack()
    {
        float dmg = _enemy.finalDamage();
        animator.SetTrigger("attack");
        _player.ModifyHealth(dmg * -1);
        
    }

}
