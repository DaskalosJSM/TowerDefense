using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    public GameManager gameManagerOb;
    void Start()
    {
        gameManagerOb = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

       Button Game = root.Q<Button>("Button1");
       Button Instructions = root.Q<Button>("Button2");
       Button Start = root.Q<Button>("Button3");

        // Acciones a realizar 

        Game.clicked += () => GotoStart();
        Instructions.clicked += () => GotoInstructions() ;
        Start.clicked += () => GotoStart() ;

     }

     void Gotomenu()
     {
        // Acciones a realizar 
        gameManagerOb.SetGameState(GameState.PrincipalMenu);
     }
      void GotoInstructions()
     {
        // Acciones a realizar 
        gameManagerOb.SetGameState(GameState.GameOver);
     }
      void GotoStart()
     {
        // Acciones a realizar 
        gameManagerOb.SetGameState(GameState.Game);
     }
     
}
