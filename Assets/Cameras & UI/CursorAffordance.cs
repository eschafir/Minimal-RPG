using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        switch (cameraRaycaster.layerHit) {

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
