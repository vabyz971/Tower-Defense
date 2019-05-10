﻿using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelButtons;

    private void Start()
    {

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 1; i < levelButtons.Length ; i++)
        {

            if(i < levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
