using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour {
    [SerializeField] private Transform debugGridObject;
    private GridSystem gridSystem;

    public static LevelGrid Instance { get; private set; }


    private void Awake() {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(debugGridObject);
        if (Instance != null) {
            Debug.LogError("There is more than one LevelGrid!" + transform + " - "+ Instance);
        }
            Instance = this;
    }

    public void AddUnitAtGridPosition(GridSystem.GridPosition gridPosition, Unit unit) {
       gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridSystem.GridPosition gridPosition) { 
        return gridSystem.GetGridObject(gridPosition).GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridSystem.GridPosition gridPosition, Unit unit) {
        gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public GridSystem.GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridSystem.GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public int GetGridWidth() => gridSystem.GetWidth();

    public int GetGridHeight() => gridSystem.GetHeight();   
    public void UnitMovedGridPosition(Unit unit, GridSystem.GridPosition fromGridPosition, GridSystem.GridPosition toGridPosition) {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public bool IsValidGridPosition(GridSystem.GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition,gridSystem);

    public bool HasAnyUnitOnGridPosition(GridSystem.GridPosition gridPositions) {
       GridObject gridObject = gridSystem.GetGridObject(gridPositions);
        return gridObject.GetUnitList().Count > 0 ? true : false;
    }


}
