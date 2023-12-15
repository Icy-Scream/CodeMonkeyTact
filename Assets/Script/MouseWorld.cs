using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour {
    [SerializeField] private bool debug;
    
    public static MouseWorld instance;
  
    private void Awake() {
       instance = this;
    }

    // Update is called once per frame
    void Update() {
        if(debug)
         transform.position = MouseWorld.GetMouseWorldPosition();
    }

    public static Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << 6);
            return hitInfo.point;
    }

}
