using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bibim_Level : MonoBehaviour
{
    public BibimTimer BibimT;
    public Mini_BibimbabMainScript Bibim;

    public void LelveReset()
    {
        Bibim.Game = false;
        BibimT.EndTimer();
        Bibim.GameReset();
    }

    public void Lelve1()
    {
        Bibim.DifficultyPanel.SetActive(false);
        foreach (var i in BibimT.WaitS)
            i.maxValue = 90f;
        Bibim.Level = 2;
        BibimT.time_current = 90;
        BibimT.WaitS[0].gameObject.SetActive(false);
        BibimT.WaitS[1].gameObject.SetActive(false);
        Bibim.GameStart();
    }
    public void Lelve2()
    {
        Bibim.DifficultyPanel.SetActive(false);
        foreach (var i in BibimT.WaitS)
            i.maxValue = 10f;
        Bibim.WaitTime = 10f;
        Bibim.Level = 3;
        BibimT.time_current = 90;
        BibimT.WaitS[0].gameObject.SetActive(true);
        BibimT.WaitS[1].gameObject.SetActive(true);

        Bibim.GameStart();
    }
    public void Lelve3()
    {
        Bibim.DifficultyPanel.SetActive(false);
        foreach (var i in BibimT.WaitS)
            i.maxValue = 7f;
        Bibim.WaitTime = 7f;
        Bibim.Level = 3;
        BibimT.time_current = 90;
        BibimT.WaitS[0].gameObject.SetActive(true);
        BibimT.WaitS[1].gameObject.SetActive(true);

        Bibim.GameStart();
    }
}
