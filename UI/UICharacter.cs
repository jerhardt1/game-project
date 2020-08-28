using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UICharacter : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar = null;
    [SerializeField]
    private Text _healthText = null;
    [SerializeField]
    private float _updateSpeedSeconds = 2;
    [SerializeField]
    private Character character = null;


    protected virtual void Awake()
    {
        if (character == null)
        {
            character = GetComponentInParent<Character>();
        }

        if (_healthBar == null)
        {
            _healthBar = transform.Find("HealthBar").GetComponent<Image>();
        }

        if (_healthText == null)
        {
            _healthText = transform.Find("HealthText").GetComponent<Text>();
        }

        _healthText.text = character.StatHealth.Value.ToString();

        character.OnHealthChange += HandleHealthChanged;

    }

    private void HandleHealthChanged(float aPct)
    {
        StartCoroutine(ChangeToPct(aPct));
        _healthText.text = character.currentHealth.ToString();
    }



    private IEnumerator ChangeToPct(float aPct)
    {
        float preChangePct = _healthBar.fillAmount;
        float elapsed = 0f;
        while (elapsed < _updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            _healthBar.fillAmount = Mathf.Lerp(preChangePct, aPct, elapsed / _updateSpeedSeconds);
            yield return null;
        }

        _healthBar.fillAmount = aPct;
    }

    private void OnDisable()
    {
       character.OnHealthChange -= HandleHealthChanged;
    }




}
