using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GetCards : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numberOfCards;
    public GameObject handGO;

    private void Awake()
    {
        handGO = GameObject.Find("/Controls/Canvas/Hand");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            GameObject[] arrayOfCards = GetCardsArray();
            // Select 3 random game objects from the array
            GameObject[] selectedObjects = new GameObject[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
            {
                int index = Random.Range(0, arrayOfCards.Length);
                selectedObjects[i] = arrayOfCards[index];
            }

            // Destroy this script component
            Destroy(this);

            // Return the selected game objects
            GameObject[] selectedObjectsArray = selectedObjects.ToArray();
            ReturnObjects(selectedObjectsArray);
        }
    }

    void ReturnObjects(GameObject[] selectedObjects)
    {
        // Do something with the selected objects, for example:
        foreach (GameObject obj in selectedObjects)
        {
            Debug.Log("Selected object: " + obj.name);
        }
    }

    public GameObject[] GetCardsArray()
    {
        DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/Prefabs/Cards/");
        FileInfo[] files = dirInfo.GetFiles("*.asset");

        GameObject[] assetFiles = new GameObject[files.Length];
        // FETCH CARDS
        for (int i = 0; i < files.Length; i++)
        {
            Debug.Log(files[i].Name);
            //Transform parent = handGO.transform;
            //GameObject cardGO = Instantiate(cardPrefab);
            //CardManager cardManager = cardGO.GetComponent<CardManager>();
            //cardManager.card = Resources.Load<CardPrefab>("Prefabs/Cards/" + files[i].Name);

            //cardGO.transform.SetParent(parent, false);

            //cardGO.name = "Card";
            //cardGO.transform.Find("NumberOfCards/Text").GetComponent<TextMeshProUGUI>().text = card.uses.ToString();

            //cardManager.SetAttributes();
        }

        return assetFiles;
    }
}
