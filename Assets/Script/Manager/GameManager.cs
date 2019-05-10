using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    [Header("Setting Color Scene")]
    public Color[] ColorBaseScene;
    public Material[] ColorMat;


    private void Awake()
    {
        GeneratColorScence();
        
    }

    private void Start()
    {
        GameIsOver = false;

    }

    void Update () {
        if (GameIsOver)
            return;

        if(PlayerStats.Lives <= 0f)
        {
            EndGame();
        }
		
	}

    void EndGame()
    {
        GameIsOver = true;

        gameOverUI.SetActive(true);
    }


    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }


    // Changement de la couleur a Chaque Scence Lancer
    void GeneratColorScence()
    {

        for (int c = 0; c < ColorBaseScene.Length; c++)
        {
            ColorMat[c].color = ColorBaseScene[c];
        }
    }
}
