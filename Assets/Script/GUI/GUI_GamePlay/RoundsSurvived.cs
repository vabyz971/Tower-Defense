using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundtext;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundtext.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < PlayerStats.Rounds)
        {
            round++;
            roundtext.text = round.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }
}
