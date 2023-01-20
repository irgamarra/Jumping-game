using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogBoxActions : MonoBehaviour
{
    public GameObject dialogBox;
    public Button acceptButton;
    public GameObject textDialogBox;

    // As OnClick only accepts one parameter, this method will use strings following the next structure:
    // "Scene;Text
    public void OpenScene(string textAndScene)
    {
        int charsToColon = 0;
        foreach (char c in textAndScene)
        {
            charsToColon++;
            if (c == ';')
            {
                break;
            }
        }
        string scene = textAndScene.Substring(0, charsToColon -1);
        string text = textAndScene.Substring(charsToColon);
        dialogBox.SetActive(true);
        textDialogBox.GetComponent<TextMeshProUGUI>().SetText(text);
        UnityAction acceptAction = () => SceneManager.LoadScene(scene);
        acceptButton.onClick.AddListener(() => { acceptAction(); });
    }
}
