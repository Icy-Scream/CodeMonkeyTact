using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    private void Awake() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
    }

    private void Instance_OnSelectedUnitChanged(object sender, System.EventArgs e) {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons() {
      for(int i = 0; i < actionButtonContainerTransform.childCount; i++) {
           GameObject uiButton = actionButtonContainerTransform.GetChild(i).gameObject;
            Destroy(uiButton);
        }

      Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray()) {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }
}
