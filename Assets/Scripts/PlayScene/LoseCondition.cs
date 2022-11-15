using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseCondition : MonoBehaviour
{

    public Hand hand;
    // Menu Manager
    public MenuManager menuManager;
    public int loseScreenPositionInMenuManager;
    public int countdownPositionInMenuManager;
   
    public int countdownSeconds;
    public TextMeshProUGUI loseCountdown;

    private bool countdownRunning = false;
    
    void Update()
    {
        if(hand.numberOfCards <= 0 && !countdownRunning)
        {
            menuManager.ShowMenu(countdownPositionInMenuManager);
            StartCoroutine(Countdown(countdownSeconds));
        }
        if(hand.numberOfCards >= 1)
        {
            countdownRunning = false;
            StopCoroutine(Countdown(0));
            Time.timeScale = 1;
        }
    }

    IEnumerator Countdown(int seconds)
    {
        countdownRunning = true;
        for (int i = seconds; i > 0; i--)
        { 
            loseCountdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        menuManager.ShowMenu(loseScreenPositionInMenuManager);
        menuManager.HideMenu(countdownPositionInMenuManager);

        Time.timeScale = 0;
    }
}
