using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuestScript : MonoBehaviour
{
    public bool Quest = false;               //현재 받은 퀘스트 메일이 있는지 확인하는 함수 (MailDontDestroy 211참조)
    [SerializeField]

    public MailLoad Mail;
    public LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;

    public bool note=false;
    public bool farm = false;

    private int QuestNum;
    public FlieChoice file;

    public QuestDontDestroy DontDestroy;

    
    public void QuestStart()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();

        if (PlayerPrefs.GetString("QuestPreg").Equals("0_0")) //튜토리얼
        {
            file.Tutorial();
            DontDestroy.QuestIndex = "0_1";
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("0_1")) //밭 튜토리얼
        {
            StartCoroutine("QFarmLoop");
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("6_1")) //양치게임 해보기 수정
        {
            Debug.Log("양치");
            DontDestroy.ToothQ = true;
        }
        else if (DontDestroy.LastDay != DontDestroy.ToDay)
        {
            QuestChoice();
        }

    }

    public void QuestChoice()
    {
        switch (DontDestroy.QuestIndex)
        {
            case "4_1": //자전거
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(4.2f, -6.39158726f, -9.8f), Quaternion.Euler(0, 146.6f, 0));
                goto case "ExMark";
            case "9_1": //스트레스
                note = true;
                GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(0, 0, 0);
                goto case "ExMark";
            case "14_1":case "17_1": case "22_1":case "29_1": case "6_1":case "13_1":case "31_1": //미량, 줄넘기, 음악, 내 삶의 주인, 양치, 도토리마을가기, 마법주문 (캐릭터 집 앞으로 데려오기)
                GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(3.8f, -6.41383314f, -0.1f);
                goto case "ExMark";
            case "16_1": //감사나무
                    if(SceneManager.GetActiveScene().name != "AcornVillage")
                {
                    GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(17.3f, -4.3f, 27.4f);
                    GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 77, 0));
                    goto case "ExMark";
                }
                break;
            case "23_1": // 사방치기 (힘찬이 옆 나리 가져다놓기)
                GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 157, 0));

                GameObject NariIm = GameObject.Find("Nari");
                NariIm.transform.position = GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position + new Vector3(2, 0, -1);
                NariIm.transform.rotation = Quaternion.Euler(new Vector3(0, 207, 0));
                goto case "ExMark";
            case "33_1":case "12_1":case "19_1": //건강해진 내모습, 김밥,자연의 아름다움
                Instantiate(Resources.Load<GameObject>("Models/NPC/npc/parents"), new Vector3(-0.2f, -6.4f, 0), Quaternion.Euler(new Vector3(0, 133, 0)));
                goto case "ExMark";
            case "30_1": //훌라후프
                chat.NPCHula.SetActive(true);
                goto case "ExMark";
            case "13_2": //도토리마을
                if (SceneManager.GetActiveScene().name == "MainField")
                {
                    GameObject.Find(DontDestroy.ButtonPlusNpc).SetActive(false);
                }
                else if (SceneManager.GetActiveScene().name == "AcornVillage")
                {
                    chat.Nari.position = new Vector3(-12.29f, 16.9f, 36.34f);
                    chat.NariMom.position = chat.Nari.position + new Vector3(-1, 0, 0);
                    GameObject.Find("NariDad").gameObject.transform.position = chat.Nari.position + new Vector3(-2, 0, 0);
                    goto case "ExMark";
                }
                break;
            case "ExMark":
                ExclamationMarkCreate();
                break;
                default: break;

        }


    }


    IEnumerator QFarmLoop()
    {
        while (true)
        {
            GameObject click = EventSystem.current.currentSelectedGameObject;
            if (click == null)
            { }
            else if (click.name.Equals("ItemBtn_"))
            {
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                PlayInfoManager.GetQuestPreg();
                farm = false;
                chat.QuestLoad.QuestLoadStart();
                StopCoroutine("QFarmLoop");
            }
            yield return null;
        }
    }
    private void ExclamationMarkCreate()
    {
        Debug.Log("퀘스트 느낌표 생성");
        if (DontDestroy.ButtonPlusNpc != null)
        {
            Transform Parent = GameObject.Find(DontDestroy.ButtonPlusNpc).GetComponent<Transform>();
            GameObject child;
            child = Instantiate(ExclamationMark[1], GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position + new Vector3(0, 1.5f, 0), GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation);
            child.transform.parent = Parent;
            file.EPin.SetActive(true);
            file.EPin.GetComponent<MapPin>().Owner = GameObject.Find(DontDestroy.ButtonPlusNpc);

            GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");
            if (clone.Length > 2)
                Destroy(clone[0]);
        }
    }

    public void ChangeDrawCamera()
    {
        if (DrawCamera.enabled == false)
        {
            Draw=true;
            MainCamera.enabled = false;
            DrawCamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = true;
            DrawCamera.enabled = false;
        }
    }
}
