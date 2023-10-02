using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forbidden : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            GameManager.Inst.PlayerReset();
    }
}
