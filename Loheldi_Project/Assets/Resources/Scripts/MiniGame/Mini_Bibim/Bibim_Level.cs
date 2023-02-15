using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bibim_Level : MonoBehaviour
{
    public BibimTimer BibimT;
    public Mini_BibimbabMainScript Bibim;

    public void Lelve1()
    {
        Bibim.Level = 1;
        BibimT.time_current = 30;

        Bibim.GameReset();
    }
    public void Lelve2()
    {
        Bibim.Level = 2;
        BibimT.time_current = 30;

        Bibim.GameReset();
    }
    public void Lelve3()
    {
        Bibim.Level = 3;
        BibimT.time_current = 30;

        Bibim.GameReset();
    }
}
