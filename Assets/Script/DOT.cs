using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DOT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosY(400, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
