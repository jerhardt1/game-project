using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class BaseState
{
    public BaseState (GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        this.animator = gameObject.GetComponentInChildren<Animator>();
    }

    protected Animator animator;
    protected GameObject gameObject;
    protected Transform transform;

    public abstract Type Tick();
}
