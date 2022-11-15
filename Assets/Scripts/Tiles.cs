using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tiles : MonoBehaviour
    , IDragHandler
    , IBeginDragHandler
{
    float beginDragYpos = 0;
    public float dragMultiplier = 0.01f;
    public void OnDrag(PointerEventData eventData)
    {
        
        if (SceneManager.GetActiveScene().name == "Editor")
        {
            Vector2 mousePos = Input.mousePosition;
            
            GameObject camera = GameObject.Find("Main Camera");
            Vector3 mousePosWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));


            float mouseDragY = (mousePos.y - beginDragYpos) * dragMultiplier;
            float finalDragY = camera.transform.position.y - mouseDragY;
            camera.transform.position = new Vector3(camera.transform.position.x, finalDragY, camera.transform.position.z);
            
            beginDragYpos = mousePos.y;
            
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDragYpos = Input.mousePosition.y;
    }

}
