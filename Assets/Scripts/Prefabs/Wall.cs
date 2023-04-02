using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    public GameObject wall;
    public SpriteRenderer sprite;
    public BoxCollider2D collider;

    public GameObject foreground;
    public SpriteRenderer fgSprite;

    public Wall(GameObject wall)
    {
        this.wall = wall;


        this.sprite = wall.GetComponent<SpriteRenderer>();
        this.collider = wall.GetComponent<BoxCollider2D>();


        if (wall.transform.childCount > 0)
        {
            this.foreground = wall.transform.GetChild(0).gameObject;
            this.fgSprite = foreground.GetComponent<SpriteRenderer>();
        }
    }

    public void CalculateWallSize(GameObject floor, GameObject goal)
    {
        Vector2 wallSize = new Vector2();

        float wallSizeWidth = sprite.size.x;
        float wallSizeHeight = 0.0f;

        if (floor.transform.localPosition.y < 0)
            wallSizeHeight = floor.transform.position.y * -1;
        else
            wallSizeHeight = floor.transform.position.y;

        wallSizeHeight += goal.transform.localPosition.y;

        wallSize.x = wallSizeWidth;
        wallSize.y = wallSizeHeight;

        sprite.size = wallSize;
        collider.size = wallSize;

        if(foreground != null)
        {
            fgSprite.size = wallSize;
        }

        CalculateWallPosition(floor,goal);
    }

    private void CalculateWallPosition(GameObject floor, GameObject goal)
    {
        float goalY = goal.transform.localPosition.y;
        float floorY = floor.transform.localPosition.y;

        float wallX = wall.transform.localPosition.x;
        float wallY = 0.0f;
        float wallZ = wall.transform.localPosition.z;

        if (floorY > 0 && goalY > 0)
            wallY = (goalY - floorY) / 2;
        else
            wallY = (goalY + floorY) / 2;

        wall.transform.localPosition = new Vector3(wallX, wallY, wallZ);
        //if (foreground != null)
        //{
        //    float fgX = foreground.transform.localPosition.x;
        //    float fgZ = foreground.transform.localPosition.z;
        //    foreground.transform.localPosition = new Vector3(fgX, wallY, fgZ);
        //}
    }
}
