using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetCards : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numberOfCards;
    public GameObject handGO;
    public Hand handManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            handGO = GameObject.Find("/Controls/Canvas/Hand");
            handManager = handGO.GetComponent<Hand>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "Circle") 
        {
            FileInfo[] arrayOfCards = GetCardsAssets();
            // Select 3 random game objects from the array
            FileInfo[] selectedObjects = new FileInfo[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
            {
                int index = Random.Range(0, arrayOfCards.Length);
                selectedObjects[i] = arrayOfCards[index];
            }

            // Destroy this script component
            Destroy(this.gameObject);

            // Return the selected game objects
            ReturnObjects(selectedObjects);
        }
    }

    void ReturnObjects(FileInfo[] selectedObjects)
    {
        GameObject[] assetFiles = new GameObject[selectedObjects.Length];
        // FETCH CARDS
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            string assetName = Path.GetFileNameWithoutExtension(selectedObjects[i].Name);
            GameObject cardGO = GetGameObjectByCardName(assetName);
            if (cardGO != null)
            {
                cardGO.GetComponent<CardManager>().AddCardNumber(1);
            }
            else
            {
                Transform parent = handGO.transform;
                cardGO = Instantiate(cardPrefab);
                CardManager cardManager = cardGO.GetComponent<CardManager>();
                cardManager.card = Resources.Load<CardPrefab>("Prefabs/Cards/" + assetName);

                cardGO.transform.SetParent(parent, false);

                cardManager.SetAttributes();
            }
        }
        handManager.RefreshHand();

        
    }

    public FileInfo[] GetCardsAssets()
    {
        DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/Prefabs/Cards/");
        FileInfo[] files = dirInfo.GetFiles("*.asset");
        return files;
    }
    GameObject GetGameObjectByCardName(string fileName)
    {
        try
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            GameObject[] cards = GameObjectTools.GetChildren(handGO);

            foreach (GameObject card in cards)
            {
                string cardName = card.GetComponent<CardManager>().card.name;
                if (fileName == cardName)
                    return card;
            }
        }
        catch
        {
           
        }

        return null;
    }
}
