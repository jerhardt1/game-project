using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathState : BaseState
{
    private Enemy _enemy;
    private float dissapearTime = 2;
    private bool triggerSet = false;

    public DeathState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
    }

    public override Type Tick()
    {
        if (!triggerSet)
        {          
            animator.SetTrigger("die");
            _enemy.characterCollider.enabled = false;
            triggerSet = true;
            LevelGenerator.instance.removeObjectFromList("enemy", _enemy.gameObject);
            LevelGenerator.instance.checkForEnemies();
        }

        dissapearTime -= Time.deltaTime;

        if (dissapearTime <= 0)
        {

            _enemy.gameObject.SetActive(false);
            

            return null;
        }
        return null;
    }

}
