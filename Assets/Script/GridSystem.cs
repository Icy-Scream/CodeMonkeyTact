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
    public Vector3 GetWorldPosition(int x, int z) {
        return new Vector3(x,0,z) * cellSize;
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
              var debugObject = GameObject.Instantiate(debugPrefab, GetWorldPosition(i, j), Quaternion.identity);
                debugObject.GetComponentInChildren<TextMeshPro>().text = $"[{i},{j}]";
            }
        }
    }


   public struct GridPosition
    {
       public int x { get; set; }
       public int z {get; set; }

        public GridPosition(int x, int z) {
            this.x = x; this.z = z;

        }

        public override string ToString() {
            return $"Grid POS: X: {x} , Z: {z}";
        }
    }
}
