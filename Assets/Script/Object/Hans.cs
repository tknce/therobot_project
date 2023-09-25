using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hans : MonoBehaviour
{

    public int Textnum;
    // Start is called before the first frame update
    void Start()
    {
        
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
                TextManager.Inst.Action(Textnum, this.gameObject);
                Debug.Log("Hans 텍스트 인덱스 : " + Textnum);

            }
            collision.gameObject.GetComponent<Player>().jumprock = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().jumprock = false;
    }
}
