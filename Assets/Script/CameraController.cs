using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    [SerializeField] float _speed = 1f;
    [SerializeField] CinemachineVirtualCamera _camera;

    private Vector3 targetFollowOffest;
    private CinemachineTransposer cinemachineTransposer;
    private void Start() {
        cinemachineTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffest = cinemachineTransposer.m_FollowOffset;
    }
    private void Update() {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    public void HandleMovement() {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
       
        if (Input.GetKey(KeyCode.W)) {
            inputMoveDir.z = +1f;
        }
        else if (Input.GetKey(KeyCode.S)) { inputMoveDir.z = -1f; }
        else if (Input.GetKey(KeyCode.D)) { inputMoveDir.x = +1f; }
        else if (Input.GetKey(KeyCode.A)) { inputMoveDir.x = -1f; }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * _speed * Time.deltaTime;
   

     
    }

    private void HandleRotation() {
        float angle = 0;
        if (Input.GetKey(KeyCode.E)) {
            angle = -0.1f;
        }
        else if (Input.GetKey(KeyCode.Q)) { angle = +0.1f; }
        Vector3 rotationVector = Vector3.up;
        transform.Rotate(rotationVector, angle);

    }

    private void HandleZoom() {

        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0) {
            targetFollowOffest.y -= zoomAmount;
        }

        if (Input.mouseScrollDelta.y < 0) {
            targetFollowOffest.y += zoomAmount;
        }
        targetFollowOffest.y = Mathf.Clamp(targetFollowOffest.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffest, Time.deltaTime * zoomSpeed);
    }
}
