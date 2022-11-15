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

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
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
            nameText.text = card.cardName;
            descriptionText.text = card.description;
            artwork.sprite = card.artwork;
            cardEffect = card.cardEffect;

            if (SceneManager.GetActiveScene().name != "Editor")
            {
                Button myButton = GetComponent<Button>();
                myButton.onClick.AddListener(() =>
                {
                    cardEffect.Invoke();
                    Destroy(gameObject);
                });
            }
        }
    }
}
