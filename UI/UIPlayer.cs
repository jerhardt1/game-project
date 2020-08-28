using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : UICharacter
{
    [SerializeField]
    private Text _goldText = null;

    [SerializeField]
    private Animator _animator = null;

    private bool _goldChanged = false;


    protected override void Awake()
    {
        base.Awake();
        Enemy.OnDropGold += HandleGoldChanged;
        TreasureChest.OnDropGold += HandleGoldChanged;
    }

    private void HandleGoldChanged(int aValue)
    {
        _animator.SetTrigger("gold");
        _goldChanged = true;
    }

    private void CountUp()
    {
        int currentGold = int.Parse(_goldText.text);
        currentGold += 1;
        _goldText.text = currentGold.ToString();
    }

    public void Update()
    {
        if (_goldChanged)
        {
            if(int.Parse(_goldText.text) != LevelGenerator.instance.ReceivedGold)
            {
                CountUp();
            }
            else
            {
                _goldChanged = false;
            }
        }
    }

    private void OnDisable()
    {
        Enemy.OnDropGold -= HandleGoldChanged;
        TreasureChest.OnDropGold -= HandleGoldChanged;
    }

}
