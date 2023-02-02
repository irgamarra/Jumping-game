// Add System.IO to work with files!
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class GameDataManager : MonoBehaviour
{
    public GameObject deckGO;
    public GameObject enemiesParent;

    public GameObject cardPrefab;
    public GameObject handGO;

    //Level selector
    public GameObject levelImage;
    public GameObject levelsGrid;

    // Create a field for the save file.
    string saveFilesPath;
    string saveFile;
    string standardName = "gamedata.json";
    EditorLevelData editorLevelData = new EditorLevelData();

    void Awake()
    {
        // Update the path once the persistent path exists.
        saveFilesPath = Application.persistentDataPath;
        saveFile = saveFilesPath + "/" + standardName;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            ReadFile(SaveFile.fileToLoad);
        }
    }

    public string ReadFile(string loadFile = "")
    {
        if (loadFile == "" || loadFile == null)
        {
            if (SaveFile.fileToSave == "" || SaveFile.fileToSave == null)
                loadFile = saveFile;
            else
                loadFile = SaveFile.fileToSave;
        }

        string fileContents = "";
        // Does the file exist?
        if (File.Exists(loadFile))
        {
            // Read the entire file and save its contents.
            fileContents = File.ReadAllText(loadFile);

            // Fetch EDITOR LEVEL DATA
            editorLevelData = JsonConvert.DeserializeObject<EditorLevelData>(fileContents);

            DataPath dataPath = editorLevelData.levelPath;

            DataDeck deck = dataPath.deck;

            // FETCH CARDS
            foreach (DataCard card in deck.cards)
            {
                Transform parent = handGO.transform;
                GameObject cardGO = Instantiate(cardPrefab);
                CardManager cardManager = cardGO.GetComponent<CardManager>();
                cardManager.card = Resources.Load<CardPrefab>("Prefabs/Cards/" + card.name);

                cardGO.transform.SetParent(parent, false);

                cardGO.name = "Card";
                cardGO.transform.Find("NumberOfCards/Text").GetComponent<TextMeshProUGUI>().text = card.uses.ToString();

                cardManager.SetAttributes();
            }

            // FETCH ENEMIES
            foreach (DataEnemy enemy in dataPath.enemies)
            {
                Transform parent = enemiesParent.transform;
                GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/" + enemy.name);
                GameObject enemyGO = Instantiate(enemyPrefab);

                enemyGO.transform.SetParent(parent, false);

                enemyGO.transform.position = new Vector3(enemy.posX, enemy.posY, enemy.posZ);
            }

            // FETCH GOAL
            DataGoal dataGoal = editorLevelData.levelPath.goal;
            GameObject goalGO = GameObject.Find("/Path/Goal");
            goalGO.transform.position = new Vector3(dataGoal.posX, dataGoal.posY, dataGoal.posZ);

        }
        // Work with JSON
        return fileContents;
    }

    public void WriteFile(string saveFileName = "")
    {
        // To decide the name of the file
        DirectoryInfo savesDirectory = new DirectoryInfo(saveFilesPath);
        this.saveFile = saveFilesPath + "/" + savesDirectory.GetFiles().Length + standardName;
        SaveFile.fileToSave = this.saveFile;

        // FETCH PATH
        DataPath dataPath = new DataPath();
        // FETCH DECK
        dataPath.deck = new DataDeck();
        dataPath.deck.cards = new List<DataCard>();
        // FETCH CARDS
        List<DataCard> cards = new List<DataCard>();
        foreach (Transform card in FetchClones(deckGO))
        {
            DataCard cardData = new DataCard();
            cardData.name = card.gameObject.name;
            cardData.uses = int.Parse(card.Find("NumberOfCards/Text").GetComponent<TextMeshProUGUI>().text);

            dataPath.deck.cards.Add(cardData);
        }
        // FETCH ENEMY
        dataPath.enemies = new List<DataEnemy>();
        foreach (Transform enemy in FetchClones(enemiesParent))
        {
            DataEnemy dataEnemy = new DataEnemy();
            dataEnemy.name = enemy.gameObject.name;
            dataEnemy.posX = enemy.transform.position.x;
            dataEnemy.posY = enemy.transform.position.y;
            dataEnemy.posZ = enemy.transform.position.z;
            dataPath.enemies.Add(dataEnemy);

            Debug.Log(dataEnemy.name);
        }

        // FETCH GOAL
        DataGoal dataGoal = new DataGoal();
        Transform goalGO = GameObject.Find("/Path/Goal").transform;
        dataGoal.posX = goalGO.position.x;
        dataGoal.posY = goalGO.position.y;
        dataGoal.posZ = goalGO.position.z;

        dataPath.goal = dataGoal;

        // TO WRITE DATA
        EditorLevelData editorLevelData = new EditorLevelData();
        editorLevelData.levelPath = dataPath;

        string jsonString = JsonConvert.SerializeObject(editorLevelData);

        // Write JSON to file.
        if(saveFileName == "")
            File.WriteAllText(SaveFile.fileToSave, jsonString);
        else
            File.WriteAllText(saveFileName, jsonString);
    }

    private List<Transform> FetchClones(GameObject parent)
    {
        List<Transform> clones = new List<Transform>();
        foreach (Transform clone in parent.GetComponentsInChildren<Transform>())
        {
            if (clone.gameObject.name.Contains("(Clone)"))
            {
                // To remove "(Clone)"
                int cloneString = "(Clone)".Length;
                int substractCloneString = clone.gameObject.name.Length - cloneString;
                clone.name = clone.gameObject.name.Substring(0, substractCloneString);

                clones.Add(clone);
            }
        }
        return clones;
    }

    public void PopulateLevelSelector()
    {
        DeleteLevelImages();
        RenameFiles();
        DirectoryInfo savesDirectory = new DirectoryInfo(saveFilesPath);
        foreach(FileInfo file in savesDirectory.GetFiles())
        {
            GameObject levelImageClone = Instantiate(levelImage);
            levelImageClone.transform.SetParent(levelsGrid.transform);
            levelImageClone.GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveFile.fileToLoad = file.FullName;
                SceneManager.LoadScene("Play");

            });
            
        }
    }
    public void DeleteLevelImages()
    {
        foreach(Transform levelImage in levelsGrid.GetComponentInChildren<Transform>())
        {
            Destroy(levelImage.gameObject);
        }
    }
    public void RenameFiles()
    {
        int fileNumberIterator = 0;
        DirectoryInfo savesDirectory = new DirectoryInfo(saveFilesPath);
        foreach (FileInfo file in savesDirectory.GetFiles())
        {
            Debug.Log(file.FullName);
            file.MoveTo(saveFilesPath + "/" + fileNumberIterator + standardName);

            Debug.Log(file.FullName);
            fileNumberIterator++;
        }
    }
    public void DeleteLevel(int numberOfLevel)
    {
        Debug.Log(saveFilesPath + numberOfLevel + standardName);
        File.Delete(saveFilesPath + "/" + numberOfLevel + standardName);
    }

}