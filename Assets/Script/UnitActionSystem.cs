using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour {
    [field: Header("Selected Unit")]
    [field: SerializeField] public Unit SelectedUnit { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public static UnitActionSystem Instance {get; private set;}

    private GameInput gameInput;

    private bool isBusy;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There's more than one unit action system" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameInput = GetComponent<GameInput>();
    }

    private void Start() {
        gameInput.OnRightClickPerformed += GameInput_OnRightClickPerformed;
    }

    private void Update() {
        if(isBusy) { return; }
        if (Input.GetMouseButtonDown(1)) {
            SetBusy();
            SelectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private void SetBusy() {
        isBusy = true;
    }

    private void ClearBusy() {
        isBusy = false; 
    }

    private void GameInput_OnRightClickPerformed(object sender, EventArgs e) {
        if (TryHandleUnitSelection() || isBusy)
            return;
        GridSystem.GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMouseWorldPosition());
        if (SelectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)) {
            SetBusy();
            SelectedUnit.GetMoveAction().Movement(mouseGridPosition,ClearBusy);
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
