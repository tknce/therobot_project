using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eba : MonoBehaviour
{
    public TextManager Textmgr;
    int[] TextNum;
    int idx;
    public int Textnum;
    bool Nexttalk;
    // Start is called before the first frame update
    void Start()
    {
        TextNum = new int[] {1,3,4 };
        idx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Nexttalk = Textmgr.Action(Textnum, this.gameObject, TextNum);
                if(Nexttalk)
                {
                    if(TextNum.Length -1 > idx)
                        idx++;
                }
                Debug.Log("Eba 텍스트 인덱스 : " + idx);
                
            }
            collision.gameObject.GetComponent<Player>().jumprock = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().jumprock = false;
    }

}
