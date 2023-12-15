using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChange;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        if (UnitActionSystem.Instance.SelectedUnit == unit) {
            meshRenderer.enabled = true;
        }
        else
            meshRenderer.enabled = false;
    }
}
