using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Ranking : MonoBehaviour
{
    public static List<Tuple<string, string>> RankingBoard = new List<Tuple<string , string>>();

    public static void ShowRanking(CanvasGroup imageCanvasGroup, TMP_Text[] textElements)
    {
        imageCanvasGroup.alpha = 0.8f;
        ScoreSet(textElements);
    }

    public static void ScoreSet(TMP_Text[] textElements)
    {
        RankingBoard.Clear();

        string timeElapsed = Timer.TimeElapsed;
        string currentName = NameInput.playerName;

        RankingBoard.Add(Tuple.Create<string, string>(currentName, timeElapsed));

        for (int i = 0; i < 5; i++)
            RankingBoard.Add(Tuple.Create<string, string>(PlayerPrefs.GetString(i + "BestName"), PlayerPrefs.GetString(i + "BestScore")));

        RankingBoard.Sort((a, b) => string.Compare(b.Item2, a.Item2));

        PlayerPrefs.DeleteAll();

        for (int i = 0; i < 5; i++)
        {
            var rankingItem = RankingBoard[i].Item1 + "\t" + RankingBoard[i].Item2;
            textElements[i].text = rankingItem;

            if (RankingBoard[i].Item1 == currentName && RankingBoard[i].Item2 == timeElapsed)
            {
                Color specialColor = new Color(255, 255, 0, 255);
                textElements[i].color = specialColor;
            }

            PlayerPrefs.SetString(i + "BestName", RankingBoard[i].Item1);
            PlayerPrefs.SetString(i + "BestScore", RankingBoard[i].Item2);
        }

        Debug.Log("Before");
        Debug.Log(GameEnding.RankPresenter);
        GameEnding.RankPresenter = true;
        Debug.Log("After");
        Debug.Log(GameEnding.RankPresenter);
    }
}
