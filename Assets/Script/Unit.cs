using UnityEngine;


public class Unit : MonoBehaviour
{
   [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;

    private void Awake() {
        targetPosition = transform.position;
    }
    void Update()
    {
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance) {
            Vector3 directionVector = (targetPosition - transform.position).normalized;
            Vector3 velocityVector = directionVector * speed * Time.deltaTime;
            float angle = Vector3.Angle(transform.forward, directionVector);

            var rotationAxis = Vector3.Cross(transform.forward, directionVector);

            int clockwise = 1;
            
            if(rotationAxis.y < 0 ) {
                clockwise = -1;
            }

            transform.Rotate(0, angle * clockwise * rotationSpeed * Time.deltaTime, 0);
            transform.position += velocityVector;
            unitAnimator.SetBool("Walk", true);
        }
        else
            unitAnimator.SetBool("Walk", false);
    }

    public void Movement(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
}
