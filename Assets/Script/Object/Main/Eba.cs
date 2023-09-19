using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eba : MonoBehaviour
{
    public TextManager Textmgr;
    int[] TextNum;
    int idx;
    // Start is called before the first frame update
    void Start()
    {
        TextNum = new int[] {1,3 };
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
            if (Input.GetButtonDown("Jump"))
            {
                Textmgr.Action(TextNum[idx], this.gameObject, TextNum);
                
            }
            collision.gameObject.GetComponent<Player>().jumprock = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().jumprock = false;
    }

}
