using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject[] Cloud_list;
    public float minimum;
    public float maximum;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ranIndex = Random.Range(0, 0.01f);
        for (int i = 0; i < Cloud_list.Length; ++i)
        {
            Cloud_list[i].transform.position = new Vector2(Cloud_list[i].transform.position.x + speed + ranIndex, Cloud_list[i].transform.position.y) ;
            if (maximum < Cloud_list[i].transform.position.x)
                Cloud_list[i].transform.position = new Vector2(minimum, Cloud_list[i].transform.position.y);
        }
    }
}
