using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter thirdPersonCharacterController;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool isDirectMovementEnabled = false;

    private void Start() {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacterController = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.G)) {
            isDirectMovementEnabled = !isDirectMovementEnabled;
        }

        if (isDirectMovementEnabled) {
            ProcessDirectMovement();
        } else {
            ProcessMouseMovement();
        }
    }

    private void ProcessMouseMovement() {
        if (Input.GetMouseButton(0)) {

            switch (cameraRaycaster.currentLayerHit) {

                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;

                case Layer.Enemy:
                    break;

                default:
                    return;
            }
        }

        var playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
            thirdPersonCharacterController.Move(playerToClickPoint, false, false);
        } else {
            thirdPersonCharacterController.Move(Vector3.zero, false, false);
        }
    }

    private void ProcessDirectMovement() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacterController.Move(movement, false, false);
        currentClickTarget = transform.position;
    }
}