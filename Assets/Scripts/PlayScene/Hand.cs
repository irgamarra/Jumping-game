using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float decrementalPosition = -20;
    public float startingRotation;
    public float spaceBetweenCards = 20;
    public float incrementalRotation;
    public string cardName = "Card";
    public Vector3 parentPosition;
    public int numberOfCards;
    // Start is called before the first frame update
    void Start()
    {
        parentPosition = transform.parent.position;
        RefreshHand();
        numberOfCards = GetNumberOfCards();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetNumberOfCards() < numberOfCards)
            RefreshHand();
    }

    public void RefreshHand(GameObject cardToDestroy = null)
    {
        // The position of the first card. The more cards, the more to the left it will be
        float cardPosition = decrementalPosition * (gameObject.transform.childCount - 1);
        
        foreach(Transform card in gameObject.GetComponentInChildren<Transform>())
        {
            if (card.gameObject.name.StartsWith(cardName))
            {
                //To calculate the position of each card.
                card.position = new Vector3(cardPosition + parentPosition.x, 150, parentPosition.z);
                //To increament the position each time. (Not optimize as it calculates a number that is never used
                cardPosition += spaceBetweenCards;
            }
        }
        // This line of code might be more optimized inside the above if
        numberOfCards = GetNumberOfCards();
    }
    public int GetNumberOfCards()
    {
        int numberOfCards = 0;
        foreach (Transform card in gameObject.GetComponentInChildren<Transform>())
        {
            if (card.gameObject.name.StartsWith(cardName))
                numberOfCards++;
        }
        return numberOfCards;
    }
}
