using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenderator : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    public GameObject StonePrefeb; //GameObject 선언
    public QuestScript chat;
    public Transform trans;

    // Update is called once per frame
    private void Start()
    {
        chat = GameObject.Find("chatManager").GetComponent<QuestScript>();
        trans = this.GetComponent<Transform>();

    }
    void FixedUpdate()
    {
        if(chat.note)
        if (timer>2)
        {   //stone을 생성하고 발사!
            GameObject stone = Instantiate(StonePrefeb, new Vector3(trans.position.x, trans.position.y+1.5f, trans.position.z), Quaternion.Euler(-50, -90, 0));
            stone.GetComponent<Throw>().Shoot(new Vector3(UnityEngine.Random.Range(-300, 300), 40, -60));
            timer = 0;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("note");
            if (objs.Length > 3)
            {
                Destroy(objs[0]);
            }
        }
        timer += Time.deltaTime;
    }
}
