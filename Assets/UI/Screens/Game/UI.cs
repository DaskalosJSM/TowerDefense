using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public Selectiles SelecTile;
    
    public GameObject PillPrefab;
    public GameObject SoilderPrefab;
    public GameObject TowerPrefab;
    public GameObject TankPrefab;

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

       Button Pill = root.Q<Button>("ButtonElement1");
       Button Tower = root.Q<Button>("ButtonElement2");
       Button Soilder = root.Q<Button>("ButtonElement3");
       Button Tank = root.Q<Button>("ButtonElement4");
   
        Pill.clicked += () =>  Debug.Log("Presiono" + PillPrefab.name);
        Tower.clicked += () => Debug.Log("Presiono" + SoilderPrefab.name);
        Soilder.clicked += () => Debug.Log("Presiono" + TowerPrefab.name);
        Tank.clicked += () => Debug.Log("Presiono" + TankPrefab.name);
     }
}
