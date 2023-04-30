using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;
    public GameObject EPin;

    [SerializeField]
    private UIButton UI;
    public QuestDontDestroy QDD;
    public Interaction Inter;
    private void Awake()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        if (SceneManager.GetActiveScene().name == "Quiz")
        {
            Quest(); 
        }
    }
    public void Tutorial()
    {
        Debug.Log("튜토리얼");
        //Save_Log.instance.SaveQStartLog();  //퀘스트 시작 시간 로그
        chat.Main_UI.SetActive(false);
        chat.FileAdress = "Scripts/Quest/Dialog/Korean/" + QDD.QuestIndex;
        chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");
        chat.Num = "0_1";
        
        chat.NewChat();
    }
    public void Quest()
    {
        if (SceneManager.GetActiveScene().name == "MainField" || SceneManager.GetActiveScene().name == "AcornVillage")
        {
            EPin.SetActive(false);
            Inter.NpcNameTF = false;
            chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest" + chat.DontDestroy.QuestIndex.Substring(0, chat.DontDestroy.QuestIndex.IndexOf("_")));
        }

            chat.FileAdress = "Scripts/Quest/Dialog/Korean/" + QDD.QuestIndex;
        chat.Num = chat.DontDestroy.QuestIndex;
        chat.NewChat();
        if (SceneManager.GetActiveScene().name == "Quiz") ;
        else
            Save_Log.instance.SaveQStartLog();  //퀘스트 시작 시간 로그
    }

    public void NpcChoice(string NameNPC) //npc와 대화 선택하는 함수
    {
        chat.NPCButton = 0;
        chat.FileAdress = "Scripts/Quest/DialogNPC";

        string[] QuestF = chat.DontDestroy.QuestIndex.Split('_');
            switch (NameNPC)
        {
            case "Himchan":
                if (chat.DontDestroy.From.Equals("실천해보기")&& chat.DontDestroy.ButtonPlusNpc.Equals("Himchan"))
                    chat.Num = "14";
                else
                    chat.Num = "1";
                chat.NPCButton += 2;
                goto case "i";
            case "Markatman":
                chat.Num = "2";
                chat.NPCButton += 2;
                goto case "i";
            case "Hami":
                if (chat.DontDestroy.From.Equals("퀴즈") && chat.DontDestroy.ButtonPlusNpc.Equals("Hami"))
                    chat.Num = "11";
                else if (chat.DontDestroy.From.Equals("실천해보기") && chat.DontDestroy.ButtonPlusNpc.Equals("Hami"))
                    chat.Num = "15";
                else
                    chat.Num = "3";
                chat.NPCButton += 1;
                goto case "i";
            case "Suho":
                if (chat.DontDestroy.From.Equals("퀴즈") && chat.DontDestroy.ButtonPlusNpc.Equals("Suho"))
                    chat.Num = "12";
                else if (chat.DontDestroy.QuestIndex.Equals("15_1"))
                    chat.Num = "18";
                else if (chat.DontDestroy.From.Equals("실천해보기") && chat.DontDestroy.ButtonPlusNpc.Equals("Suho"))
                    chat.Num = "16";
                else
                    chat.Num = "4";
                chat.NPCButton += 1;
                goto case "i";
            case "Nari":
                chat.Num = "5";
                chat.NPCButton += 1;
                goto case "i";
            case "Mei":
                if (chat.DontDestroy.From.Equals("실천해보기") && chat.DontDestroy.ButtonPlusNpc.Equals("Mei"))
                    chat.Num = "17";
                else if (chat.DontDestroy.QuestIndex.Equals("9_1"))
                    chat.Num = "19";
                else
                chat.Num = "6";
                chat.NPCButton += 1;
                goto case "i";
            case "Yomi":
                chat.Num = "7";
                chat.NPCButton += 2;
                goto case "i";
            case "Yeomi":
                chat.Num = "8";
                chat.NPCButton += 2;
                goto case "i";
            case "Mu":
                chat.Num = "9";
                chat.NPCButton += 2;
                goto case "i";
            case "WallMirror":
                chat.Num = "10";
                chat.NPCButton += 3;
                goto case "i";
            case "parents(Clone)":
                chat.Num = "13";
                chat.NPCButton += 2;
                goto case "i";
            case "GachaMachine":
                chat.Num = "20";
                chat.NPCButton += 2;
                goto case "i";
            case "ThankApplesTree":
                int nowTime = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                if (UI.time == nowTime)
                { 
                    chat.Num = "22";
                    chat.NPCButton += 1;
                }
                else
                {
                    chat.Num = "21";
                    chat.NPCButton += 2;
                }
                goto case "i";
            case "Kangteagom":

                if (SceneManager.GetActiveScene().name == "AcornVillage")
                    chat.Num = "24";
                else
                    chat.Num = "23";
                chat.NPCButton += 2;
                goto case "i";
            case "Bibim":
                chat.Num = "25";
                chat.NPCButton += 2;
                goto case "i";
            case "Wood":

                    chat.Num = "26";
                chat.NPCButton += 1;
                goto case "i";
            case "Fruit":

                    chat.Num = "27";
                chat.NPCButton += 1;
                goto case "i";
            case "InfoSign":
                chat.Num = "28";
                goto case "i";
            case "i":
                Debug.Log("i");
                chat.NPCNewChat();
                chat.Buttons();
                break;
            default:
                Debug.Log("디폴트");
                break;
        }

    }
    
}
