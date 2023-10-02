using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ballon_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rotate();
    }

    void rotate()
    {
        if (this != null)
        {
            float result = Random.Range(0.1f, 3);
            transform.DOLocalRotate(new Vector3(0, 0, -5f), result).onComplete = delegate { rotate1(); };
        }
    }
    void rotate1()
    {
        if (this != null)
        {
            float result = Random.Range(0.1f, 3);
            transform.DOLocalRotate(new Vector3(0, 0, 5f), result).onComplete = delegate { rotate(); };
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Event_condition event_Condition = new Event_condition();
            event_Condition.balon = true;
            EventManager.Inst.SetEvent_condition(event_Condition);
            EventManager.Inst.SetWave(EventWave.Stage1_clear);
            transform.gameObject.SetActive(false);
        }
    }
}
