using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    PrincipalMenu,
    Game,
    GameOver
}
public class GameManager : MonoBehaviour
{
    // Inicializo el singleton en el primer script 
    public static GameManager sharedInstance;
    public GameState currentGameState = GameState.PrincipalMenu;

    public void Awake()
    {
        // que despierte y enfatizo con el siguiente fragmento
        // Singleton
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Función encargado de iniciar la scena menú principal
    public void PrincipalMenu()
    {   
        SetGameState(GameState.PrincipalMenu);
    }
    // Función encargado de iniciar la scena Game
    public void Game()
    {   
        SetGameState(GameState.Game);
    }

    // Función encargado de iniciar la scena de final de juego
    public void GameOver()
    {   
        SetGameState(GameState.GameOver);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void SetGameState(GameState newGameState)
    {
        this.currentGameState = newGameState;

        if (newGameState == GameState.PrincipalMenu)
        {
            SceneManager.LoadScene("MainMenu");
            //TODO: colocar la logica del menu
        }
        else if (newGameState == GameState.Game)
        {
            SceneManager.LoadScene("PrincipalGame");
            //TODO: colocar la logica del level game
        }
        else if (newGameState == GameState.GameOver)
        {
            SceneManager.LoadScene("GameOver");
            //TODO: colocar la logica del game over
        }
    }
}