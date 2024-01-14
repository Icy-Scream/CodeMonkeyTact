using System;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    protected override void Awake() {
        base.Awake();
    }

    private void Update() {
        if (!isActive) { return; }
            float rotateAngle;
            rotateAngle = 360 * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, rotateAngle, 0);
            totalSpinAmount += rotateAngle;
            if (totalSpinAmount >= 360f) {
                transform.eulerAngles = new Vector3(0,0,0);
                isActive = false;
                onActionComplete?.Invoke();
            }
    }

    public override string GetActionName() {
        return "Spin";
    }

    public void Spin(Action onSpinComplete) {
        isActive = true;
        this.onActionComplete = onSpinComplete;
        totalSpinAmount = 0;
    }
}
