using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScriptVisual : MonoBehaviour
{
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    private GridScriptVisualSingle[,] gridSystemVisualSingleArray;

    public static GridScriptVisual Instance { get; private set; }


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one LevelGrid!" + transform + " - " + Instance);
        }
        Instance = this;
    }

    private void Update() {
        UpdateGridVisual();
    }

    private void Start() {
        gridSystemVisualSingleArray = new GridScriptVisualSingle[LevelGrid.Instance.GetGridWidth()
            ,LevelGrid.Instance.GetGridHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetGridWidth(); x++) {
            for(int z = 0; z < LevelGrid.Instance.GetGridHeight(); z++) {
                 GridSystem.GridPosition gridPosition = new GridSystem.GridPosition(x, z);
               Transform gridSystemVisualSingle = 
                    Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
              var gridSystemVisual =  gridSystemVisualSingle.GetComponent<GridScriptVisualSingle>();
 
                gridSystemVisualSingleArray[x, z] = gridSystemVisual;
            }
        }
    }

    public void HideAllGridPositions() {
        foreach(var gridVisual in gridSystemVisualSingleArray) {
            gridVisual.Hide();
        }
    }

    public void ShowGridPositionsList(List<GridSystem.GridPosition> gridPositions) {
       foreach(var gridPosition in gridPositions) {
            gridSystemVisualSingleArray[gridPosition.x,gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual() {
        HideAllGridPositions();
        Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
        if (selectedUnit != null)
        ShowGridPositionsList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }
}
