using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebuObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TMP_Text gridPositionText;
   public void SetGridObject(GridObject gridObject) {
        this.gridObject = gridObject;
    }

    private void Update() {
       gridPositionText.text = gridObject.ToString();
    }
}
