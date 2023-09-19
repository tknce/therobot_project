using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos();
    }
    void mousePos()
    {
        if (Cursor.visible)
            Cursor.visible = false;

        Vector3 pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y, 5f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        this.transform.position = worldPos;



        //Debug.Log(worldPos.ToString());
    }
}
