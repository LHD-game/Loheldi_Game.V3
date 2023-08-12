using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTransparent : MonoBehaviour
{
    Camera MainCamera;
    RaycastHit rayhit;
    Ray ray;

    GameObject TempObject;
    GameObject Hit_Obj;

    float MaxDistance = 300f;
    public Material Trans_Material;
    Material Temp_Material;
    bool MaterialChange = false;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }
    void FixedUpdate()
    {
        //Debug.Log(MaterialChange);
        ray = MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(ray, out rayhit, MaxDistance))
        {
            Hit_Obj = rayhit.transform.gameObject;
            if (Hit_Obj.GetComponent<MeshRenderer>())
            {
                Debug.Log(" Hit_Obj : " + Hit_Obj);
                Debug.Log(" TempObject : " + TempObject);
                if (TempObject == null)
                {
                    TempObject = Hit_Obj;
                    Temp_Material = TempObject.GetComponent<MeshRenderer>().material;
                    TempObject.GetComponent<MeshRenderer>().material = Trans_Material;
                    MaterialChange = true;
                }
                else if (TempObject != Hit_Obj && MaterialChange)
                {
                    TempObject.GetComponent<MeshRenderer>().material = Temp_Material;
                    TempObject = null;
                    MaterialChange = false;
                }
            }
            else if (MaterialChange)
            {
                TempObject.GetComponent<MeshRenderer>().material = Temp_Material;
                TempObject = null;
                MaterialChange = false;
            }
        }
    }
}
