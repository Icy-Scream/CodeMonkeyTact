using UnityEngine;


public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [field: SerializeField] public float speed { get; set;} = 4f;
    [field: SerializeField] public float rotationSpeed { get; set; } = 4f;
 
    private GridSystem.GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionArray;
    private void Awake() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);   
    }

    void Update()
    {
        bool isWalking = moveAction.GetMoveAction(this);
        unitAnimator.SetBool("Walk", isWalking);
        GridSystem.GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
       
        if(newGridPosition != gridPosition) {
           LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction() {
        return moveAction;
    }

    public SpinAction GetSpinAction() {  return spinAction; }

    public GridSystem.GridPosition GetGridPosition() {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray() {
        return baseActionArray;
    }
}
