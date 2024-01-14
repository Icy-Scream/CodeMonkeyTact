using UnityEngine;
using System;
public abstract class BaseAction : MonoBehaviour {
    protected bool isActive;
    protected Unit unit;
    protected Action onActionComplete;
    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();
}