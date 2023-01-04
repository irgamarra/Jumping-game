using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Goal : MonoBehaviour
    , IDragHandler
{
    public string characterName = "Circle";
    public GameObject winScreen;

    GameObject leftWall;
    GameObject rightWall;
    GameObject floor;

    Vector3 selfPos;
    Vector3 mousePos;
    Camera cam;

    private void Awake()
    {
        leftWall = GameObject.Find("/Path/Left wall");
        rightWall = GameObject.Find("/Path/Right wall");
        floor = GameObject.Find("/Path/Floor");
    }

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
        
        if(spawnPoint.y > floor.transform.position.y)
            gameObject.transform.position = new Vector3 (selfPos.x, spawnPoint.y, selfPos.z);

        SpriteRenderer lwSpriteRenderer = leftWall.GetComponent<SpriteRenderer>();
        CalculateWallSize(leftWall);
        CalculateWallSize(rightWall);
    }

    private void CalculateWallSize(GameObject wall)
    {
        Vector2 wallSize = new Vector2();

        SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
        BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();

        float wallSizeWidth =  wallSpriteRenderer.size.x;
        float wallSizeHeight = 0.0f;
        
        if (floor.transform.localPosition.y < 0)
            wallSizeHeight = floor.transform.position.y * -1;
        else
            wallSizeHeight = floor.transform.position.y;

        wallSizeHeight += this.transform.localPosition.y;
            
        wallSize.x = wallSizeWidth;
        wallSize.y = wallSizeHeight;
        
        wallSpriteRenderer.size = wallSize;
        wallCollider.size = wallSize;

        CalculateWallPosition(wall);
    }

    private void CalculateWallPosition(GameObject wall)
    {
        float goalY = this.transform.localPosition.y;
        float floorY = floor.transform.localPosition.y;

        float wallX = wall.transform.localPosition.x;
        float wallY = 0.0f;
        float wallZ = wall.transform.localPosition.z;

        if (floorY > 0 && goalY > 0)
            wallY = (goalY - floorY) / 2;
        else
            wallY = (goalY + floorY) / 2;

        wall.transform.localPosition = new Vector3 (wallX,wallY,wallZ);
    }
}
