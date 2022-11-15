using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
     , IPointerClickHandler
     , IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
    , IEndDragHandler
{
    SpriteRenderer sprite;
    Color target = Color.red;
    Camera cam;
    Vector3 mousePos;
    Vector3 selfPos;
    public GameObject myPrefab;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        cam = Camera.main;
        selfPos = gameObject.transform.position;
    }
    void Update()
    {
        if (sprite)
            sprite.color = Vector4.MoveTowards(sprite.color, target, Time.deltaTime * 10);
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        //gameObject.transform.position = mousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Input.mousePosition;
        gameObject.transform.position = mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject clone = Instantiate(myPrefab);
        mousePos = Input.mousePosition;
        Vector3 spawnPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        clone.transform.parent = GameObject.Find("/Path/Enemies").transform;
        clone.transform.position = spawnPoint;
        
        // Image goes back to the editor menu
        gameObject.transform.position = selfPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        target = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //gameObject.transform.position = selfPos;
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
    }
 }
