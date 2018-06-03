using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start() {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.layerChangeObservers += OnLayerChange; // adding to the set of observers of CameraRaycast
    }

    void OnLayerChange(Layer newLayer) {
        switch (newLayer) {

            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, transform.position, CursorMode.Auto);
                break;

            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, transform.position, CursorMode.Auto);
                break;

            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, transform.position, CursorMode.Auto);
                break;

            default:
                Debug.Log("Don't know what cursor to show");
                return;
        }

    }

}
