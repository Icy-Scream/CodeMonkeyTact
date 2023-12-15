using UnityEngine;

public class Testing : MonoBehaviour
{
  private GridSystem gridSystem;
 [SerializeField] private Transform gridDebugObject;

    private void Start() {
        gridSystem = new GridSystem(10, 10,2);
        gridSystem.CreateDebugObjects(gridDebugObject);
    }

    private void Update() {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetMouseWorldPosition()));
    }
}
