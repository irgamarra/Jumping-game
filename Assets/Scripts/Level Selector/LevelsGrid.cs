using Assets.Scripts.Level_Selector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelsGrid : MonoBehaviour
{
    GameDataManager gameDataManager;
    GameObject eventSystem;
    List<Transform> gameObjects = new List<Transform>();
    GameObject levelsGrid;
    void Start()
    {
        eventSystem = GameObject.Find("/EventSystem");
        gameDataManager = eventSystem.GetComponent<GameDataManager>();
        levelsGrid = GameObject.Find("/Main Canvas/Levels grid");

        gameDataManager.PopulateLevelSelector();

        int levelImageIterator = 0;
        foreach (Transform levelSelector in levelsGrid.GetComponentInChildren<Transform>())
        {
            LevelImage levelImage = levelSelector.GetComponent<LevelImage>();
            levelImage.levelImageNumber = levelImageIterator;
            levelImageIterator++;
        }
    }
}
