using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_BibimbabMainScript : MonoBehaviour
{
    public GameObject[] food;
    public Material[] Egg;
    public Material[] Egg_material;
    public bool[] Foodorder;
    private bool EggFinish=false;

    private void Start()
    {
        Egg_material = food[0].GetComponent<MeshRenderer>().materials;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("비빔");
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
            foodNum -= 1;
            if (!food[foodNum].activeSelf)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name.Equals("1"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    StartCoroutine(EggFrie());
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
                }
                else if (hit.collider.gameObject.name.Equals("9"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    if (!EggFinish)
                    {
                        Debug.Log("계란 안구워짐");

                        return;
                    }

                    EggFinish = false;
                    food[0].SetActive(false);
                    Egg_material[1] = Egg[0]; //0에 메테리얼 번호
                    food[0].GetComponent<MeshRenderer>().materials = Egg_material;
                }
                else if (hit.collider.gameObject.name.Equals("10"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    foreach (GameObject i in food)
                    {
                        i.SetActive(false);
                    }
                    return;
                }
                else if (hit.collider.gameObject.name.Equals("11"))
                {
                    GameObject target = hit.collider.transform.parent.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    foreach (GameObject i in food)
                    {
                        i.SetActive(false);
                    }
                    return;
                }
                //Destroy(target);

                Debug.Log(foodNum);

                food[foodNum].SetActive(true);
            }
            else
                return;
        }

        IEnumerator EggFrie()
        {
            yield return new WaitForSecondsRealtime(5f);
            EggFinish=true;
            Egg_material[1] = Egg[1]; //0에 메테리얼 번호
            food[0].GetComponent<MeshRenderer>().materials = Egg_material;
        }

        void order()
        {
            int RamdomorderCount;
            RamdomorderCount = UnityEngine.Random.Range(0, 9);

            for(int i=0; i< RamdomorderCount; i++)
            {
                int foodNum = UnityEngine.Random.Range(0, 9);
                if (Foodorder[foodNum])
                    ;
                else
                    Foodorder[foodNum] = false;
            }
        }

        void CheckMenu()
        {

        }
    }
}
