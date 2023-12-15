using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem {

    private int width;
    private int height;
    private float cellSize;
    public GridSystem(int x, int height, float cellSize) {
        this.width = x;
        this.height = height;
        this.cellSize = cellSize;
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < this.height; j++) {
                Debug.DrawLine(GetWorldPosition(i,j), GetWorldPosition(i,j) + Vector3.right * .2f, Color.white,10000);
            }
        }

    }

    //Pass Grid Coords -> Grid's World Position
    public Vector3 GetWorldPosition(int x, int z) {
        return new Vector3(x,0,z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldposition) {
        return new GridPosition(
            Mathf.FloorToInt(worldposition.x/cellSize),
            Mathf.FloorToInt(worldposition.z/cellSize)
            );
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
