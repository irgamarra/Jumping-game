using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardPrefab card;

    public TextMeshProUGUI numberOfCards;
    public GameObject numberOfCardsGO;

    //public TextMeshProUGUI nameText;
   //public TextMeshProUGUI descriptionText;
    public Image artwork;
    public UnityEvent cardEffect;
    public GameObject hand;

    private void Awake()
    {
        SetAttributes();
    }
    public void SetAttributes()
    {
        if (card != null)
        {

            AddCardNumber(0);
            //nameText.text = card.cardName;
            //descriptionText.text = card.description;
            artwork.sprite = card.artwork;
            cardEffect = card.cardEffect;

            // To remove On Click on the Editor screen
            if (SceneManager.GetActiveScene().name != "Editor")
            {
                Button myButton = GetComponent<Button>();
                myButton.onClick.AddListener(() =>
                {
                    cardEffect.Invoke();

                    AddCardNumber(-1);
                });
            }


        }
    }

    public void AddCardNumber(int number)
    {
        int noc = Int32.Parse(numberOfCards.text);
        noc += number;
        numberOfCards.text = noc.ToString();

        if (noc > 1)
            numberOfCardsGO.SetActive(true);
        else
            numberOfCardsGO.SetActive(false);

        if (noc < 1)
            Destroy(gameObject);
    }
}
