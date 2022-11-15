using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    Button myButton;
    TextMeshProUGUI myText;
    public int cooldown = 5;
    private void Awake()
    {
        myButton = gameObject.GetComponent<Button>();
        myText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }
    void Update()
    {
    }
    public void InitiateCooldown()
    {
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {

        myButton.interactable = false;
        for (int time = cooldown; time > 0; time--)
        {
            myText.text = time.ToString();
            yield return new WaitForSeconds(1);
        }
        myText.text = "";
        myButton.interactable = true;
    }

}
