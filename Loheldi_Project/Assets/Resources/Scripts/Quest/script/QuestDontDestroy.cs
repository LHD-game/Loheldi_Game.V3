using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};

    public string ButtonPlusNpc;  //버튼 추가되는 NPC이름(퀘스트)
    public string From;  //실천해보기 구분 용
    //public int QuestSubNum;         //퀘스트 스크립트 앞번호
    public string QuestIndex;   //몇번째 퀘스트차례인지 (서버저장)
    //public string ReQuestIndex;   //몇번째 퀘스트차례인지 (다시하기)

    public int LastDay=0;  //마지막으로 퀘스트를 클리어한 날짜(서버 저장)
    public int ToDay=0;      //오늘 날짜
    public List<int> badgeList = new List<int>();  //획득한 벳지 

    public bool NtoSControl = false;
    public bool ToothQ = false;
    //public bool weekend = false;
    //public bool SDA = false;
    public bool tutorialLoading=false; //튜토리얼 진행 중인지(하우징갔다가 메인왔을 때 진행 시키는 용)

    public string Language = "Korean";

    public bool ReQuest = false;
    public bool QuestNF = false;
    //public bool WeekQMail = true; //주말, 평일 퀘 메일 구분용
    //public bool RiciveQuest = false; //퀘스트를 받았는지 확인하는 bool값

    public GameObject LastPlayerTransform;
    //public GameObject[] Mails;
    /// </summary>

    void Awake()
    {
        Dontdestroy();
    }
    public void Dontdestroy()
    {
        //Debug.Log("돈디");
        LastPlayerTransform = this.gameObject;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");
        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }/*
        else if (SceneManager.GetActiveScene().name == "Game_Tooth")
        {
            Debug.Log("양치");
            Instantiate(Resources.Load<GameObject>("Prefabs/Tooth/QTooth/Canvas"), new Vector3(0, 0, -0), Quaternion.Euler(0, 0, 0));
            Instantiate(Resources.Load<GameObject>("Prefabs/Tooth/QTooth/QToothBrush"), new Vector3(0, 0, -0), Quaternion.Euler(0, 0, 0));
            GameObject.Find("Canvas").SetActive(false);
            GameObject.Find("Player").SetActive(false);
            GameObject.Find("mouth").SetActive(false);
        }*/
        DontDestroyOnLoad(this.gameObject);
        Language = PlayerPrefs.GetString("Language");
    }
    
}
