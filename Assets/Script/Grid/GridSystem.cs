using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridSystem {

    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;
    public GridSystem(int width, int height, float cellSize) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        
        gridObjectArray = new GridObject[width, height];


        for (int i = 0; i < this.width; i++) {
            for(int j = 0; j < this.height; j++) {
                GridPosition gridPosition = new GridPosition(i, j);
                gridObjectArray[i,j] = new GridObject(gridPosition, this);
            }
        }

    }

    //Pass Grid Coords -> Grid's World Position
    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return new Vector3(gridPosition.x,0,gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldposition) {
        return new GridPosition(
            Mathf.RoundToInt(worldposition.x/cellSize),
            Mathf.RoundToInt(worldposition.z/cellSize)
            );
    }

    public void CreateDebugObjects(Transform debugPrefab) {
        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < this.height; j++) {
              var debugObject = GameObject.Instantiate(debugPrefab, GetWorldPosition(new GridPosition(i, j)), Quaternion.identity);
                debugObject.GetComponent<GridDebuObject>().SetGridObject(GetGridObject(new GridPosition(i, j)));
                debugObject.GetComponentInChildren<TMP_Text>().text = debugObject.ToString();
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }
    public bool IsValidGridPosition(GridSystem.GridPosition gridPosition,GridSystem gridSystem) {
        if ((gridPosition.x < gridSystem.width && gridPosition.x >= 0) && (gridPosition.z < gridSystem.height && gridPosition.z >= 0)) {
            return true;
        }
        else return false;
    }

    public int GetWidth() => width;
    public int GetHeight() => height; 
    public struct GridPosition : IEquatable<GridPosition>
    {
       public int x { get; set; }
       public int z {get; set; }

        public GridPosition(int x, int z) {
            this.x = x; this.z = z;

        }

        public override string ToString() {
            return $"{x},{z}";
        }

        public override bool Equals(object obj) {
            return obj is GridPosition position &&
                   x == position.x &&
                   z == position.z;
        }

        public override int GetHashCode() {
            return HashCode.Combine(x, z);
        }

        public bool Equals(GridPosition other) {
            return this == other;
        }

        public static bool operator ==(GridPosition a, GridPosition b) {
            return a.x == b.x && a.z == b.z ;
        }

        public static bool operator != (GridPosition a, GridPosition b) 
            { return !(a == b); }

        public static GridPosition operator +(GridPosition a, GridPosition b) { return new GridSystem.GridPosition(a.x + b.x, a.z + b.z); }
        public static GridPosition operator -(GridPosition a, GridPosition b) { return new GridSystem.GridPosition(a.x - b.x, b.z - b.z); }
    }
}
