using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousingCategory : MonoBehaviour
{
    GameObject itemBtn;
    JsonData myInven_rows = new JsonData();

    [SerializeField]
    private GameObject c_gagu;
    public List<GameObject> gagu_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수
    public List<Dictionary<string, object>> gaguItem = new List<Dictionary<string, object>>();

    //---init list---//
    //itemTheme 별로 모아서 저장
    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
    }
    public void GetChartContents(string itemChart)  //전체 아이템 목록과 보유 아이템 목록을 가져온다.
    {
        var allItemChart = Backend.Chart.GetChartContents(itemChart); //서버의 엑셀파일을 불러온다.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData allItem_rows = allItemChart.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0;

        for (int i = 0; i < allItem_rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(allItem_rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("wood") || data.Category.Equals("modern") || data.Category.Equals("kitsch") || data.Category.Equals("classic") || data.Category.Equals("wallpaper") || data.Category.Equals("gacha"))
            {
                gaguItem.Add(new Dictionary<string, object>());
                initItem(gaguItem[s], data);
                s++;
            }
        }
    }

    public void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject, string ItemType)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/InvenItemforHousing");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            if (ItemType.Equals(dialog[i]["ItemType"].ToString()))
            {
                GameObject child;
                //create caltalog box
                child = Instantiate(itemBtn);    //create itemBtn instance
                child.transform.SetParent(category.transform, false);  //move instance: child
                                                                       //아이템 박스 크기 재설정
                RectTransform rt = child.GetComponent<RectTransform>();
                rt.localScale = new Vector3(1f, 1f, 1f);

                itemObject.Add(child);

                GameObject ItemBtn = child.transform.Find("ItemBtn").gameObject;

                //change catalog box img
                GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
                Image img = item_img.GetComponent<Image>();
                img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + dialog[i]["ICode"] + "_catalog");


                //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
                GameObject item_name = ItemBtn.transform.Find("ItemName").gameObject;
                Text txt = item_name.GetComponent<Text>();
                txt.text = dialog[i]["IName"].ToString();

                //change catalog box item code
                GameObject item_code = ItemBtn.transform.Find("ItemCode").gameObject;
                Text item_code_txt = item_code.GetComponent<Text>();
                item_code_txt.text = dialog[i]["ICode"].ToString();

                GameObject disable_img = child.transform.Find("Disable").gameObject;
                disable_img.SetActive(true);
                for (int j = 0; j < myInven_rows.Count; j++)
                {
                    MyItem data = pj.ParseBackendData<MyItem>(myInven_rows[j]);
                    if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                    {
                        //비활성 창 오브젝트(Disable)를 비활성화
                        disable_img.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}
