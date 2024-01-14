using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveAction : BaseAction {

    private Vector3 targetPosition;
    [SerializeField] private int maxMoveDistance = 4;
    
    protected override void Awake() {
        base.Awake();
        targetPosition = transform.position;
    }

    public bool GetMoveAction(Unit unit) {
        if(!isActive) { return false; }

        float stoppingDistance = 0.1f;
        Vector3 directionVector = (targetPosition - unit.transform.position).normalized;
        Quaternion rotationQ = Quaternion.LookRotation(new Vector3(directionVector.x, 0, directionVector.z));
        float rotationSpeed = 5f;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(targetPosition, unit.transform.position) > stoppingDistance) {
            
                Vector3 velocityVector = directionVector * unit.speed * Time.deltaTime;
                transform.position += velocityVector;

            return true;
        }
        else {
                isActive = false;
                onActionComplete?.Invoke();
                return false;
        }


    }

    public void Movement(GridSystem.GridPosition gridPosition, Action onActionComplete) {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    public override string GetActionName() {
        return "Move";
    }

    public bool IsValidActionGridPosition(GridSystem.GridPosition gridPosition) {

        return GetValidActionGridPositionList().Contains(gridPosition);
    }

    public List<GridSystem.GridPosition> GetValidActionGridPositionList() {
        
        List<GridSystem.GridPosition> validGridPositionList = new List<GridSystem.GridPosition>();
        GridSystem.GridPosition unitGridPosition = unit.GetGridPosition();
        
        //Starts grid search from left to right
        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++) { 
            for(int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridSystem.GridPosition offsetGridPosition = new GridSystem.GridPosition(x,z);
                GridSystem.GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition) )
                    continue;

                if (unitGridPosition == testGridPosition)
                    //Grid Position equals the unit
                    continue;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    // Grid Position already occupied by another unit
                    continue;
                validGridPositionList.Add(testGridPosition);
            }
        
        }

        return validGridPositionList;
    }
}