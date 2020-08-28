using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : UICharacter
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void LateUpdate()
    {
        Vector3 lookAt = (2 * transform.position - Camera.main.transform.position);
        transform.LookAt(lookAt);
    }
}
