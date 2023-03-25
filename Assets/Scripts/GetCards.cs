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
            CardPrefab[] arrayOfCards = GetCardsAssets();
            // Select 3 random game objects from the array
            CardPrefab[] selectedObjects = new CardPrefab[numberOfCards];
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

    void ReturnObjects(CardPrefab[] selectedObjects)
    {
        // FETCH CARDS
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            string assetName = Path.GetFileNameWithoutExtension(selectedObjects[i].cardName);
            GameObject cardGO = GetGameObjectByCardName(assetName);
            // If card exists, add a use
            if (cardGO != null)
            {
                cardGO.GetComponent<CardManager>().AddCardNumber(1);
            }
            // If card does not exists, create one
            else
            {
                Transform parent = handGO.transform;
                cardGO = Instantiate(cardPrefab);
                CardManager cardManager = cardGO.GetComponent<CardManager>();
                cardManager.card = selectedObjects[i];

                cardGO.transform.SetParent(parent, false);

                cardManager.SetAttributes();
            }
        }
        handManager.RefreshHand();

        
    }

    public CardPrefab[] GetCardsAssets()
    {
        CardPrefab[] cards = Resources.LoadAll<CardPrefab>("Prefabs/Cards/");

        return cards;
    }
    GameObject GetGameObjectByCardName(string name)
    {
        try
        {
            GameObject[] cards = GameObjectTools.GetChildren(handGO);

            foreach (GameObject card in cards)
            {
                
                string cardName = card.GetComponent<CardManager>().card.cardName;
                if (name == cardName)
                    return card;

                Debug.Log(name + cardName);
            }
        }
        catch
        {
           
        }

        return null;
    }
}
