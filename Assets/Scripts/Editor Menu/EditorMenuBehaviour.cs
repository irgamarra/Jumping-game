using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EditorMenuBehaviour : MonoBehaviour
{
    // Card variables
    public float cardScale = 0.5f;

    //DeckMenu
    public GameObject deckMenu;
    public GameObject dmGrid;
    public GameObject dmText;

    void Start()
    {

        if (deckMenu == null)
            throw new System.Exception("Deck menu not set");
        if (dmGrid == null)
            throw new System.Exception("Deck menu's Grid not set");
        if (dmText == null)
            throw new System.Exception("Deck menu's Text not set");
    }
    public void AddCard(GameObject cardPrefab)
    {
        string cardName = GetCardName(cardPrefab);
        GameObject card = GetCardByName(cardName);
        
        if (card is null)
        {
            GameObject clone = Instantiate(cardPrefab);
            clone.transform.SetParent(dmGrid.transform, false);
            // To eliminate the OnClick functions inside the deck grid
            clone.GetComponent<Button>().enabled = false;
        }
        else
        {
            //To add a +1 on Number of cards
            CardManager cardManager = card.GetComponent<CardManager>();
            cardManager.AddCardNumber(1);
        }

    }
    public void CheckEmptyDeck()
    {

        if (dmGrid.transform.childCount == 0)
            dmText.SetActive(true);
        else
            dmText.SetActive(false);
    }

    public List<GameObject> GetCardsInGrid()
    {
        List<GameObject> cardsInGrid = new List<GameObject>();

        foreach(Transform child in dmGrid.transform.GetComponentsInChildren<Transform>())
        {
            if (child.name.Contains("Clone"))
            {
                cardsInGrid.Add(child.gameObject);
            }
        }
        return cardsInGrid;
    }

    public GameObject GetCardByName(string name)
    {
        foreach (GameObject card in GetCardsInGrid())
        {
            if (card.transform.childCount > 0)
            {
                string cardName = GetCardName(card);
                if (cardName == name) return card;
            }
        }
        return null;
    }

    public string GetCardName(GameObject cardPrefab)
    {
        return cardPrefab.GetComponent<CardManager>().card.cardName;
    }
}
