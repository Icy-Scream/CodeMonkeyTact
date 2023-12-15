using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour {
    [field: Header("Selected Unit")]
    [field: SerializeField] public Unit SelectedUnit { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public static UnitActionSystem Instance {get; private set;}

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There's more than one unit action system" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update() {


        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (TryHandleUnitSelection())
                return;
            SelectedUnit.Movement(MouseWorld.GetMouseWorldPosition());
        }
    }

    private bool TryHandleUnitSelection() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << 7);
            try {
                hitInfo.transform.TryGetComponent<Unit>(out Unit selectedUnit);
                SetSelectedUnit(selectedUnit);
                return true;
            }
            catch (System.NullReferenceException) {
                Debug.Log("Not a valid unit");
            return false;    
        }
    }

    private void SetSelectedUnit(Unit unit) {
        SelectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

}
