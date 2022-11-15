using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    public float cameraSize;
    public GameObject characterGameObject;
    void Start()
    {
        //Default Character's Game Object
        if (characterGameObject == null)
            characterGameObject = GameObject.Find("/Ball/Circle");

        //To adjust camera depending on Screen's resolution
        if (Screen.width < 1000)
            gameObject.GetComponent<Camera>().orthographicSize = 5;
        if (Screen.width > 1000)
            gameObject.GetComponent<Camera>().orthographicSize = 6;
    }

    private void Update()
    {
        if (characterGameObject != null)
        {
            if (characterGameObject.transform.position.y > gameObject.transform.position.y)
                gameObject.transform.position = new Vector3(0, characterGameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}