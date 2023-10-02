using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public StageValue stage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameManager.Inst.ChangeStage((int)stage);
            }
        }
    }
}
