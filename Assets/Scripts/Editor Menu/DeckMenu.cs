using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMenu : MonoBehaviour
{
    public GameObject grid;
    public GameObject deckEmptyText;
    private void Start()
    {
    }
    void OnEnable()
    {
        if(grid.transform.childCount == 0)
            deckEmptyText.SetActive(true);
        else
            deckEmptyText.SetActive(false);
    }

}
