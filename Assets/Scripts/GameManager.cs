using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    //Store the current state of the game
    public GameState currentState;
    //Store the previous state
    public GameState previousState;

    [Header("Screen")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;


    [Header("Current Stat Displays")]
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    //check if the game is over
    public bool isGameOver = false; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }
        DisableScreen();    
    }

    void Update()
    {
        //define behavior of each state
        switch (currentState)
        {
            case GameState.Gameplay:
                //code
                CheckForPauseAndResume();
                break;

            case GameState.Paused:
                //code
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                //code
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("GAME OVER");
                    DisplayResults();
                }
                break;
            default:
                Debug.LogWarning("State does not exit");
                break;
        }    
    }

    //define the method to change state
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("GAME PAUSED");
        } 
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("GAME RESUMED");
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Chosen weapons and passive items data lists have different lengths");
            return;
        }

        //assign chosen weapon data to UI
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            //check if the sprite of corresponding element on chosenWeaponsData is not null
            if (chosenWeaponsData[i].sprite)
            {
                //enable the corresponding element in chosenWeaponUI and set its sprite to the correspondong sprite in chosenWeaponsData
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                //disable the corresponding element if the sprite is null
                chosenWeaponsUI[i].enabled = false;
            }
        }

        //assign chosen passive item data to UI
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            //check if the sprite of corresponding element on chosenPassiveItemsData is not null
            if (chosenPassiveItemsData[i].sprite)
            {
                //enable the corresponding element in chosenPassiveItemsUI and set its sprite to the correspondong sprite in chosenPassiveItemssData
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                //disable the corresponding element if the sprite is null
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }
}
