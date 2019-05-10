﻿using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint {

    public GameObject prefab;
    public int cost;

    public GameObject upgradePrefab;
    public int upgradeCost;

    public Text showCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
