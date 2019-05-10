using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

    public Text MoneyText;


    void Update () {
        MoneyText.text = "$ " + PlayerStats.Money;
		
	}
}
