using UnityEngine;

public class Testing : MonoBehaviour
{
  private GridSystem gridSystem;

    private void Start() {
        gridSystem = new GridSystem(10, 10,1);
    }

    private void Update() {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetMouseWorldPosition()));
    }
}