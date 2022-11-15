using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Goal : MonoBehaviour
    , IDragHandler
{
    public string characterName = "Circle";
    public GameObject winScreen;

    Vector3 selfPos;
    Vector3 mousePos;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        selfPos = gameObject.transform.position;

        if (winScreen == null)
        {
            winScreen = GameObject.Find("WinScreen");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Circle")
        {
            winScreen.SetActive(true);
            collision.gameObject.SetActive(false);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Input.mousePosition;
        Vector3 spawnPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        gameObject.transform.position = new Vector3 (selfPos.x, spawnPoint.y, selfPos.z);
    }
}
