using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public float cameraSize;
    public GameObject characterGameObject;

    [SerializeField]
    private float thresholdY = 2;

    private bool CoroutineActive = false;
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
            float charactersY = characterGameObject.transform.position.y;
            float thisY = gameObject.transform.position.y;
            if (charactersY > thisY)
                gameObject.transform.position = new Vector3(0, charactersY, gameObject.transform.position.z);
            if (charactersY < thisY - thresholdY && !CoroutineActive)
            {
                Debug.Log("AAAAAAAAAA");
                StartCoroutine(FollowGameObjectInY());
            }
        }
    }
    IEnumerator FollowGameObjectInY()
    {

        for (float i = gameObject.transform.position.y; i > characterGameObject.transform.position.y; i -= 0.015f)
        {
            gameObject.transform.position = new Vector3(0, i, gameObject.transform.position.z);
            CoroutineActive = true;
            yield return new WaitForSeconds(.001f);
        }
        CoroutineActive = false;
    }
}