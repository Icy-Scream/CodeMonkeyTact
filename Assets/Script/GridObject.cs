using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridSystem.GridPosition gridPosition;

    public GridObject(GridSystem.GridPosition gridPosition, GridSystem gridSystem) {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
    }
}
