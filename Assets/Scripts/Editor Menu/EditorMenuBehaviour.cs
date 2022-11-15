using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject clone = Instantiate(cardPrefab);
        clone.transform.SetParent(dmGrid.transform, false);
        // To eliminate the OnClick functions inside the deck grid
        clone.GetComponent<Button>().enabled = false;
    }

    public void CheckEmptyDeck()
    {

        if (dmGrid.transform.childCount == 0)
            dmText.SetActive(true);
        else
            dmText.SetActive(false);
    }
}
