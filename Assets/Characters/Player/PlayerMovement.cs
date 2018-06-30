using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] const int walkableLayerNumber = 9;
    [SerializeField] const int enemyLayerNumber = 10;

    ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;

    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick; // adding to the set of observers of CameraRaycast
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit) {

            case walkableLayerNumber:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;

            case enemyLayerNumber:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;

            default:
                Debug.LogWarning("Don't know how to process mouse click");
                return;
        }
    }

    void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

}