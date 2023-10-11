using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public StageValue stage;
    public GameObject other_potal;
    public EventWave wave;
    bool open = false;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,0.5f);
    }
    private void Update()
    {
        if (!open)
            if ((int)wave <= (int)EventManager.Inst.GetWave())
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,1);
                open = true;
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {



        if (open)
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameManager.Inst.ChangeStage((int)stage, other_potal.transform.position);
                }
            }
    }
}
