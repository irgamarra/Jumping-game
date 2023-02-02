using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Level_Selector
{
    public class LevelImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public int levelImageNumber = 0;

        GameDataManager gameDataManager;
        GameObject eventSystem;
        GameObject dialogBox;
        Canvas boxDialogCanvas;
        Button myButton;
        DialogBox dialogBoxScript;

        public float secondsForDialogBox = 0.75f;
        void Awake()
        {
            eventSystem = GameObject.Find("/EventSystem");
            gameDataManager = eventSystem.GetComponent<GameDataManager>();
            dialogBox = GameObject.Find("/Main Canvas/Dialog box");
            boxDialogCanvas = dialogBox.GetComponent<Canvas>();
            myButton = gameObject.GetComponent<Button>();
            dialogBoxScript = dialogBox.GetComponent<DialogBox>();
        }
        void Update()
        {
            myButton.interactable = !boxDialogCanvas.enabled;
        }
        private IEnumerator OpenDialogBox()
        {
            
            yield return new WaitForSeconds(secondsForDialogBox);
            dialogBoxScript.levelImageNumber = levelImageNumber;
            boxDialogCanvas.enabled = true;

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
            StartCoroutine(OpenDialogBox());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine(OpenDialogBox());
        }
    }
}