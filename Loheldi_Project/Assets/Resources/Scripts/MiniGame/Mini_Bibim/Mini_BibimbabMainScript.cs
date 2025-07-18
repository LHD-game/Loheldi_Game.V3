using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using static UnityEngine.Tilemaps.TilemapRenderer;

public class Mini_BibimbabMainScript : MonoBehaviour
{
    //public GameObject[] FoodNameTag;
    public BibimTimer BibimT;

    public float WaitTime = 0;

    public int Level;  //나오는 인원 수
    public GameObject[] NowGuest = new GameObject[3];  //주문하는 NPC
    public bool StartOrder = false;

    public GameObject[] TalkBallon = new GameObject[3];  //주문 창
    public GameObject[] food;
    public Material[] Egg;
    public Material[] Egg_material;

    public int[] FoodCook = new int[9];
    int[][] FoodOrders = new int[3][] { new int[9], new int[9], new int[9] };
    public GameObject[][] FoodImg = new GameObject[3][]; //주문창들 배열
    public GameObject[] FoodImg1 =new GameObject[7];  //1번 주문창 X이미지
    public GameObject[] FoodImg2 =new GameObject[7];  //2번 주문창 X이미지
    public GameObject[] FoodImg3 =new GameObject[7];  //3번 주문창 X이미지
    public string[] FoodName = new string[9];
    public bool EggFinish=false;
    public bool EggBurn=false;

    public bool Game = false;

    public Text BibimScore;

    public GameObject[] Guest;
    public GameObject[] Position;
    public int[] GuestNum = new int[3];

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject HPDisablePanel;   //hp 부족으로 인한 팝업 패널 오브젝트
    public GameObject DifficultyPanel;  // 난이도 선택 화면 패널 오브젝트

    public Text CoinTxt;  //코인 보상
    public Text ExpTxt;   //경험치 보상

    IEnumerator coroutine;
    public IEnumerator[] Timecoroutine = new IEnumerator[3];

    IEnumerator[] WaitTimer = new IEnumerator[3];

    private void Start()
    {
        float i_width = Screen.width;
        float i_hight = Screen.height;
        float i_ratio = i_width / i_hight;
        if (i_ratio < 2)
            Camera.main.fieldOfView = 42;
        else;

        Debug.Log("가로: " + i_width + "\n세로: " + i_hight + "\n계산결과: " + i_ratio);
        FoodImg[0] = FoodImg1;
        FoodImg[1] = FoodImg2;
        FoodImg[2] = FoodImg3;
        coroutine = EggFrie();
        Egg_material = food[0].GetComponent<MeshRenderer>().materials;
        GameReset();
    }
    public void GameReset()
    {   BibimT.isPause = false;
        BibimT.isRun = false;
        StartOrder = false;
        Game = false;
        for(int i=0; i<3; i++)
        {
            NowGuest[i] = null;
        }

        foreach (GameObject o in Guest)
            o.SetActive(false);
        foreach (GameObject o in TalkBallon)
            o.SetActive(false);

        foreach (IEnumerator i in WaitTimer)
        {
            if (i == null)
                continue;
            else
                StopCoroutine(i);
        }
        BibimReset();
        EggreSet();

        BibimT.isPause = false;

        GameOverPanel.SetActive(false);
        WelcomePanel.SetActive(true);
        PausePanel.SetActive(false);

        //player.velocity = new Vector3(0, 0, 0);

        //FinishSound = false;
        //GameStart();
    }
    public void GameEnd()
    {   BibimT.isPause = false;
        BibimT.isRun = false;
        StartOrder = false;
        Game = false;
        for(int i=0; i<3; i++)
        {
            NowGuest[i] = null;
        }

        foreach (GameObject o in Guest)
            o.SetActive(false);
        foreach (GameObject o in TalkBallon)
            o.SetActive(false);

        foreach (IEnumerator i in WaitTimer)
        {
            if (i == null)
                continue;
            else
                StopCoroutine(i);
        }
        BibimReset();
        EggreSet();

        BibimT.isPause = false;

        //보상창
        CoinTxt.text = BibimScore.text;

        float get_exp = 10f;
        int get_coin = Int32.Parse(BibimScore.text);

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);
        GameOverPanel.SetActive(true);

        //player.velocity = new Vector3(0, 0, 0);

        //FinishSound = false;
        //GameStart();
    }
    
    public void GameStartButton()
    {
        int now_hp = PlayerPrefs.GetInt("HP");

        if (now_hp > 0)  //현재 hp가 0보다 크다면
        {
            //hp 1 감소
            PlayInfoManager.GetHP(-1);
            WelcomePanel.SetActive(false);
            DifficultyPanel.SetActive(true);
        }
        else    //0 이하라면: 게임 플레이 불가
        {
            // hp가 부족합니다! 팝업 띄우기
            HPDisablePanel.SetActive(true);
        }

    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        BibimT.isRun = true;
        BibimScore.text = "0";

        orderPosition(0);
        for (int i = 1; i < Level; i++)
        {
            Timecoroutine[i] = NextGuest(i);
            StartCoroutine(Timecoroutine[i]);
            StartOrder = true;
        }
        Game = true;

    }
    void Update()
    {
        if (Game)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("비빔");
                BibimDeco();
            }
        }
    }
    void BibimDeco()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "BibimFood")
            {
                int foodNum = Int32.Parse(hit.collider.gameObject.name);
                if (foodNum > 8)
                {
                    if (hit.collider.gameObject.name.Equals("9"))
                    {
                        GameObject target = hit.collider.transform.parent.gameObject;
                        BibimReset();
                        return;
                    }
                    else if (hit.collider.gameObject.name.Equals("10"))
                    {
                        GameObject target = hit.collider.transform.parent.gameObject;
                        CheckMenu();
                        return;
                    }
                }
                else if (!food[foodNum].activeSelf)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.name.Equals("0"))
                    {
                        GameObject target = hit.collider.transform.parent.gameObject;
                        coroutine = EggFrie();
                        StartCoroutine(coroutine);
                        return;
                    }
                    else if (hit.collider.gameObject.name.Equals("6"))
                    {
                        GameObject target = hit.collider.transform.parent.gameObject;
                        if (!EggFinish)
                        {
                            return;
                        }
                        else if (EggBurn)
                        {
                            EggFinish = false;
                            EggreSet();
                            return;
                        }

                        EggreSet();
                    }

                    food[foodNum].SetActive(true);
                    if (FoodCook[foodNum] == 0)
                        FoodCook[foodNum] = 1;
                    else
                        FoodCook[foodNum] = 0;
                }
                else
                {
                    if (hit.collider.gameObject.name.Equals("6"))
                    {
                        GameObject target = hit.collider.transform.parent.gameObject;
                            //Debug.Log("계란 안구워짐");
                            if (EggBurn)
                            {
                                //Debug.Log("계란 탐"); 
                                EggreSet();
                            }
                    }
                    return;
                }
            }
            else
                return;
        }
    }

    IEnumerator EggFrie()
    {
        EggFinish = false;
        EggBurn = false;
        food[0].SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        EggFinish = true;
        Egg_material[1] = Egg[1]; //0에 메테리얼 번호
        food[0].GetComponent<MeshRenderer>().materials = Egg_material;
        if (Level > 2)
        {
            yield return new WaitForSecondsRealtime(5f);
            Egg_material[1] = Egg[2];
            Egg_material[0] = Egg[3];
            food[0].GetComponent<MeshRenderer>().materials = Egg_material;
            EggBurn = true;
        }
    }

    void BibimReset()
    {
        EggFinish = false;
        foreach (GameObject i in food)
        {
            if (i != food[0])
                i.SetActive(false);
        }
        FoodCook = Enumerable.Repeat(1, 9).ToArray();
        FoodCook[0] = 0;
    }

    void EggreSet()
    {
        StopCoroutine(coroutine);
        food[0].SetActive(false);
        Egg_material[1] = Egg[0]; //0에 메테리얼 번호
        Egg_material[0] = Egg[4];
        food[0].GetComponent<MeshRenderer>().materials = Egg_material;
    }

    void NpcCoice(int i)
    {
        foreach (GameObject j in FoodImg[i])
        {
            j.SetActive(false);
        }
        GuestNum[i] = UnityEngine.Random.Range(0, 7);
        if (!Guest[GuestNum[i]].activeSelf)
        {
            NowGuest[i] = Guest[GuestNum[i]];
            NowGuest[i].SetActive(true);
            TalkBallon[i].SetActive(true);
            NowGuest[i].transform.position = Position[i].transform.position;
            //Debug.Log(WaitTime);
            WaitTimer[i] = BibimT.WaitTimer(i);
            StartCoroutine(WaitTimer[i]);
        }
        else
            NpcCoice(i);
    }

    void orderPosition(int i)
    {
        order(i);
    }
    public void order(int i)  //레벨링
    {
            Debug.Log("for문 i = " + i);
            Debug.Log("초기화됐나= " + NowGuest[i]==null);

            if (NowGuest[i] == null)
            {
                NpcCoice(i);
            }

            foreach (GameObject j in FoodImg[i])
            {
                j.SetActive(false);
            }
            Debug.Log("새로운 주문");
            FoodOrders[i] = Enumerable.Repeat(0, 9).ToArray();
            int RamdomorderCount;
            RamdomorderCount = UnityEngine.Random.Range(1, Level + 1);  //랜덤으로 정해지는 재료 갯수 a~b-1
            for (int a = 0; a < RamdomorderCount; a++)
            {
                int foodNum = UnityEngine.Random.Range(1, 7);  //랜덤으로 정해지는 빠지는 재료
                                                               //Debug.Log(foodNum);
                if (FoodOrders[i][foodNum] == 1)
                {
                    //Debug.Log("이미받은주문입니다");
                    a--;
                    continue;
                }
                else
                {
                    FoodOrders[i][foodNum] = 1;
                    FoodImg[i][foodNum].SetActive(true);
                    //Debug.Log("주문 = " + FoodName[foodNum] + " 빼주세요");
                }
        }
        //Array.Copy(Foodorder, FoodCook, 9);
        //Foodorder.CopyTo(FoodCook, 9);
    }
    bool success;
    void CheckMenu()
    {
        for (int i = 0; i < Level; i++)
        {
            success = true; 
            //Debug.Log(i+"번째 매뉴와 비교");
            for (int check = 0; check < FoodOrders[i].Length; check++)
            {
                //Debug.Log("주문 = " + FoodOrders[i][check] + "\n" + "요리 = " + FoodCook[check]);
                if ((int)FoodCook[check] != (int)FoodOrders[i][check])
                {
                    //Debug.Log("매뉴 오류");
                    success = false;
                    break;
                }
                else
                    continue;
            }
            if (success)
            {
                ResetGuest(i);
                BibimReset();
                BibimScore.text = (Int32.Parse(BibimScore.text) + 1).ToString();
                //Debug.Log("조리성공 i = " + i);
                return;
            }
        }
    }

    public void ResetGuest(int i)
    {
        NowGuest[i].SetActive(false);
        TalkBallon[i].SetActive(false);
        NowGuest[i] = null;
        Timecoroutine[i] = NextGuest(i);
        StartCoroutine(Timecoroutine[i]);
        StopCoroutine(WaitTimer[i]);
    }

    IEnumerator NextGuest(int i)
    {
        while (StartOrder)
        {
            yield return null;//new WaitForSeconds(2f);
            //StartOrder = false;
        }
        yield return new WaitForSeconds(2f);
        if (Game)
            order(i);
        StartOrder=false;
    }

    public void BallonesPositioning()
    {
        foreach (GameObject i in Position)
            this.transform.position = Camera.main.WorldToScreenPoint(i.transform.position + new Vector3(0, 1.5f, 0));
    }
}
