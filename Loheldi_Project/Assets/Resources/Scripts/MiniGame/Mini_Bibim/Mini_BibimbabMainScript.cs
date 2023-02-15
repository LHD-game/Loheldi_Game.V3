using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Mini_BibimbabMainScript : MonoBehaviour
{
    public int Level;
    public GameObject[] NowGuest = new GameObject[3];

    public GameObject[] TalkBallon = new GameObject[3];
    public GameObject[] food;
    public Material[] Egg;
    public Material[] Egg_material;

    public int[] FoodCook = new int[9];
    int[][] FoodOrders = new int[3][] { new int[9], new int[9], new int[9] };
    public GameObject[][] FoodImg = new GameObject[3][];//{ new GameObject[7], new GameObject[7], new GameObject[7] };
    public GameObject[] FoodImg1 =new GameObject[7];
    public GameObject[] FoodImg2 =new GameObject[7];
    public GameObject[] FoodImg3 =new GameObject[7];
    public string[] FoodName = new string[9];
    public bool EggFinish=false;
    public bool EggBurn=false;

    public Text BibimScore;

    public GameObject[] Guest;
    public GameObject[] Position;
    public int[] GuestNum = new int[3];


    IEnumerator coroutine;

    private void Start()
    {
        FoodImg[0] = FoodImg1;
        FoodImg[1] = FoodImg2;
        FoodImg[2] = FoodImg3;
        Egg_material = food[0].GetComponent<MeshRenderer>().materials;
    }
    public void GameReset()
    {
        coroutine = EggFrie();
        FoodImg[0] = FoodImg1;
        FoodImg[2] = FoodImg2;
        FoodImg[1] = FoodImg3;
        Egg_material = food[0].GetComponent<MeshRenderer>().materials;

        for(int i=0; i<3; i++)
        {
            NowGuest[i] = null;
        }

        foreach (GameObject o in Guest)
            o.SetActive(false);
        foreach (GameObject o in TalkBallon)
            o.SetActive(false);

        BibimScore.text = "0";
        BibimReset();
        GameStart();
        EggreSet();
    }
    void GameStart()
    {
        for (int i = 0; i < Level; i++)
        {
            orderPosition(i);
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("비빔");
            BibimDeco();
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
                //foodNum -= 1;
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
                        //BibimReset();
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
                        //Debug.Log(hit.collider.gameObject.name);
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

                    //Destroy(target);

                    Debug.Log("foodNum = " + foodNum);

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
        yield return new WaitForSecondsRealtime(5f);
        EggFinish = true;
        Egg_material[1] = Egg[1]; //0에 메테리얼 번호
        food[0].GetComponent<MeshRenderer>().materials = Egg_material;

        yield return new WaitForSecondsRealtime(5f);
        Egg_material[1] = Egg[2];
        Egg_material[0] = Egg[3];
        food[0].GetComponent<MeshRenderer>().materials = Egg_material;
        EggBurn = true;
    }

    void BibimReset()
    {
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
        GuestNum[i] = UnityEngine.Random.Range(0, 7);
        if (!Guest[GuestNum[i]].activeSelf)
        {
            NowGuest[i] = Guest[GuestNum[i]];
            NowGuest[i].SetActive(true);
            TalkBallon[i].SetActive(true);
            NowGuest[i].transform.position = Position[i].transform.position;
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
    bool success=true;
    void CheckMenu()
    {
        for (int i = 0; i < Level; i++)
        {
            success = true;
            for (int check = 0; check < FoodOrders.Length; check++)
            {
                //Debug.Log("주문 = " + FoodOrders[i][check] + "\n" + "요리 = " + FoodCook[check]);
                if ((int)FoodCook[check] != (int)FoodOrders[i][check])
                {
                    Debug.Log("매뉴 오류");
                    success = false;
                    break;
                }
                else
                    continue;
            }
            if (success)
            {
                NowGuest[i].SetActive(false);
                NowGuest[i] = null;
                BibimScore.text = (Int32.Parse(BibimScore.text) + 1).ToString();
                BibimReset();
                Debug.Log("조리성공 i = " + i);
                //Invoke("order(i)", 1f);  //요거요거
                order(i);


                return;
            }
        }
        
    }
}
