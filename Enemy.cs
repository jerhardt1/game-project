using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Character
{

    public string _enemyName { get; private set; }

    [SerializeField]
    private int _minGold = 0;

    [SerializeField]
    private int _maxGold = 100;

    private Player _player;
    public Collider characterCollider;

    public static event Action<GameObject> OnDeath = delegate { };
    public static event Action<int> OnDropGold = delegate { };

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(enemy: this, player: _player) },
            {typeof(WalkState), new WalkState(enemy: this) },
            {typeof(AttackState), new AttackState (aEnemy: this, aPlayer: _player) },
            {typeof(DeathState), new DeathState (enemy: this) },

        };

        gameObject.GetComponent<StateMachine>().SetStates(states);
    }

    public override void Awake()
    {

        base.Awake();
        currentHealth = StatHealth.Value;
        _player = Player.instance;
        characterCollider = GetComponent<BoxCollider>();
        InitializeStateMachine();
    }

    private void OnMouseDown()
    {
        if (isAlive)
        {
            ModifyHealth((_player.finalDamage()) * -1);
            PlayerAttackVisualizer.instance.triggerFX(aType: _player.attackType);
            Player.instance.triggerSound();
            if (currentHealth <= 0)
            {
                OnDeath(gameObject);
                OnDropGold(_goldDrop());
            }
        }
    }

    private int _goldDrop()
    {
        int goldAmount = UnityEngine.Random.Range(_minGold, _maxGold);
        return goldAmount;
    }


    private void Update()
    {

    }







}
