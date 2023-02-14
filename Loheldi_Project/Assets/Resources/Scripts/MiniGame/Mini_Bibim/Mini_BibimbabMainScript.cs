using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mini_BibimbabMainScript : MonoBehaviour
{
    public GameObject[] food;
    public Material[] Egg;
    public Material[] Egg_material;
    public bool[] Foodorder;
    public bool[] FoodCook;
    private bool EggFinish=false;

    private void Start()
    {
        Foodorder = new bool[9];
        FoodCook = new bool[9];
        Egg_material = food[0].GetComponent<MeshRenderer>().materials;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("���");
            BibimDeco();
        }
    }
    void BibimDeco()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            int foodNum = Int32.Parse(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.name);
            //foodNum -= 1;
            if (foodNum > 8)
            {
                if (hit.collider.gameObject.name.Equals("9"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    BibimReset();
                    return;
                }
                else if (hit.collider.gameObject.name.Equals("10"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
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
                    StartCoroutine(EggFrie());
                    return;
                }
                else if (hit.collider.gameObject.name.Equals("1"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("2"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("3"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("4"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("5"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("6"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("7"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                }
                else if (hit.collider.gameObject.name.Equals("8"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    if (!EggFinish)
                    {
                        Debug.Log("��� �ȱ�����");

                        return;
                    }

                    EggFinish = false;
                    EggreSet();
                }

                //Destroy(target);

                Debug.Log(foodNum);

                food[foodNum].SetActive(true);
                if (FoodCook[foodNum])
                    FoodCook[foodNum] = false;
                else
                    FoodCook[foodNum] = true;
            }
            else
                return;
        }
    }

    IEnumerator EggFrie()
    {
        food[0].SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        EggFinish = true;
        Egg_material[1] = Egg[1]; //0�� ���׸��� ��ȣ
        food[0].GetComponent<MeshRenderer>().materials = Egg_material;
    }

    void BibimReset()
    {
        foreach (GameObject i in food)
        {
            if (i != food[0])
                i.SetActive(false);
        }
        FoodCook = (bool[])Foodorder.Clone();
    }
    void EggreSet()
    {
        food[0].SetActive(false);
        Egg_material[1] = Egg[0]; //0�� ���׸��� ��ȣ
        food[0].GetComponent<MeshRenderer>().materials = Egg_material;
    }

    public void order()
    {
        Debug.Log("���ο� �ֹ�");
        Foodorder = Enumerable.Repeat(true, 9).ToArray();
        int RamdomorderCount;
        RamdomorderCount = UnityEngine.Random.Range(3, 4);  //�������� �������� ��� ���� a~b-1
        for (int i = 0; i < RamdomorderCount; i++)
        {
            int foodNum = UnityEngine.Random.Range(1, 9);  //�������� �������� ������ ���
            Debug.Log(foodNum);
            if (!Foodorder[foodNum])
            {
                Debug.Log("�̹̹����ֹ��Դϴ�");
                i--;
                continue;
            }
            else
            {
                Foodorder[foodNum] = false;

                Debug.Log("�ֹ� = " + food[foodNum] + " ���ּ���");
            }
        }

        //Array.Copy(Foodorder, FoodCook, 9);
        //Foodorder.CopyTo(FoodCook, 9);
        FoodCook = (bool[])Foodorder.Clone();
    }

    void CheckMenu()
    {
        foreach (bool check in FoodCook)
        {
            if (!check)
            {
                Debug.Log("�Ŵ� ����");
                return;
            }
            else
                continue;
        }
        Debug.Log("��������");
        BibimReset();
        order();
    }
}
