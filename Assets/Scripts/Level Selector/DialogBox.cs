using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Level_Selector
{
    public class DialogBox : MonoBehaviour
    {
        public int levelImageNumber = 0;
        GameDataManager gameDataManager;

        Canvas myCanvas;
        [SerializeField]
        GameObject deleteButton;
        [SerializeField]
        GameObject editButton;
        
        void Start()
        {
            gameDataManager = GameObject.Find("/EventSystem").GetComponent<GameDataManager>();

            myCanvas = GetComponent<Canvas>();
            myCanvas.enabled = false;
        }

        public void DeleteLevel()
        {
            gameDataManager.DeleteLevel(levelImageNumber);
            myCanvas.enabled = false;
            gameDataManager.PopulateLevelSelector();
        }

        public void EditLevel()
        {
            myCanvas.enabled = false;
        }   
        public void CloseDialogBox()
        {
            myCanvas.enabled = false;
        }
    }
}