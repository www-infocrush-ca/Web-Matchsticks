using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState { Busy, New, PickFirstPlayer, PlayersTurn, ComputersTurn, PlayerLost, ComputerLost };


    private GameState currentGameState;
    RandomMatchstick rmg;

    public Text displayMessage;
    public Text matchesRemainingMessage;
    public Text questionMessage;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Button newGameButton;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(()=>StartCoroutine(RemoveMatches(1)));
        button2.onClick.AddListener(()=>StartCoroutine(RemoveMatches(2)));
        button3.onClick.AddListener(()=>StartCoroutine(RemoveMatches(3)));
        button4.onClick.AddListener(()=>StartCoroutine(RemoveMatches(4)));
        newGameButton.onClick.AddListener(()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        rmg = GetComponent<RandomMatchstick>();
        SetRemainingMatchesText();
        currentGameState = GameState.New;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState){
            case GameState.New:
                currentGameState = GameState.Busy;
                Debug.Log( "New!");
                StartCoroutine(NewGame());
                break;
            case GameState.PickFirstPlayer:
                currentGameState = GameState.Busy;
                StartCoroutine(PickFirstPlayer());
                break;
            case GameState.PlayersTurn:
                currentGameState = GameState.Busy;
                StartCoroutine(ShowPlayerOptions());
                break;
            case GameState.ComputersTurn:
                currentGameState = GameState.Busy;
                StartCoroutine(ComputersTurn());
                break;
            case GameState.PlayerLost:
                currentGameState = GameState.Busy;
                StartCoroutine(ShowEndofGameMessage("You Lost :("));
                break;
            case GameState.ComputerLost:
                currentGameState = GameState.Busy;
                StartCoroutine(ShowEndofGameMessage("You Won!"));
                break;
        }
        
    }
    IEnumerator NewGame(){
        yield return ChangeDisplayMessage(" Welcome to Game of Sticks(21)!", 4f);
        yield return new WaitForSeconds(1f);
        currentGameState = GameState.PickFirstPlayer;
    }

    IEnumerator PickFirstPlayer(){
        int nextPlayer = new System.Random().Next(2);

        if(nextPlayer == 0) {
            yield return ChangeDisplayMessage("Computer goes first", 3f);
            currentGameState = GameState.ComputersTurn;
        } else {
            yield return ChangeDisplayMessage ("Player goes first", 3f);
            currentGameState = GameState.PlayersTurn;
        }
    }

    IEnumerator ShowPlayerOptions(){
        yield return new WaitForSeconds(0.1f);
        if(rmg.GetRemainingMatches() == 1){
            currentGameState = GameState.PlayerLost;
        } else{
            questionMessage.gameObject.SetActive(true);
            if(rmg.GetRemainingMatches() == 2){
                button1.gameObject.SetActive (true);
            }
            else if(rmg.GetRemainingMatches() == 3){
                questionMessage.gameObject.SetActive(true);
                button1.gameObject.SetActive (true);
                button2.gameObject.SetActive (true);
            }
            else if(rmg.GetRemainingMatches() == 4){
                questionMessage.gameObject.SetActive(true);
                button1.gameObject.SetActive (true);
                button2.gameObject.SetActive (true);
                button3.gameObject.SetActive (true);
            }
            else{
                questionMessage.gameObject.SetActive(true);
                 button1.gameObject.SetActive (true);
                 button2.gameObject.SetActive (true);
                 button3.gameObject.SetActive (true);
                 button4.gameObject.SetActive (true);

            }
        }
        
    }

    IEnumerator RemoveMatches(int numMatches){
        questionMessage.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
        yield return ChangeDisplayMessage("Removing "+numMatches+" match(es)", 2f);
        rmg.RemoveMatches(numMatches);
      
        SetRemainingMatchesText();
        currentGameState = GameState.ComputersTurn;
        
    }

    void SetRemainingMatchesText() {
       
        
        matchesRemainingMessage.text = "Matches Remaining: "+ rmg.GetRemainingMatches();
    }

    IEnumerator ComputersTurn() {
        if (rmg.GetRemainingMatches() == 1) {
            currentGameState = GameState.ComputerLost;
        } else {
            yield return new WaitForSeconds(1f);
            //yield return ChangeDisplayMessage("Computer's Turn", 2f);
            int pickMatches = NimHelper.CalculateMatchestoRemove(rmg.GetRemainingMatches());
            yield return ChangeDisplayMessage("Computer picks "+pickMatches+ " match(es)", 2f);
            rmg.RemoveMatches(pickMatches);
            PlaySound.GetInstance().playThisSoundEffect();
            yield return new WaitForSeconds(1f);
            SetRemainingMatchesText();
            currentGameState = GameState.PlayersTurn;
        }
    }

    IEnumerator ChangeDisplayMessage(string newMessage, float messageDuration) {
        displayMessage.gameObject.SetActive(true);
        displayMessage.text = newMessage;
        yield return new WaitForSeconds(messageDuration);
        displayMessage.gameObject.SetActive(false);

    }

    IEnumerator ShowEndofGameMessage(string message) {
        yield return ChangeDisplayMessage(message, 3f);
        newGameButton.gameObject.SetActive(true);
    }
}

