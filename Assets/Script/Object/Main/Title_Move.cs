using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Title_Move : MonoBehaviour
{
    RectTransform rttrans;

    // Start is called before the first frame update
    void Start()
    {
        rttrans = GetComponent<RectTransform>();
        rttrans.DOAnchorPos(new Vector2(0,-1220), 0);
        rttrans.DOAnchorPosY(200, 1);
        rttrans.DOScale(new Vector2(12, 5), 1).onComplete = delegate { rttrans.DOScale(new Vector2(8.5f, 3.8f), 2).SetEase(Ease.OutBounce).onComplete = delegate { DotScaleBounce(); }; };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DotScaleBounce()
    {
        rttrans.DOScale(new Vector2(9, 4f), 1).onComplete = delegate { rttrans.DOScale(new Vector2(8.5f, 3.8f), 0.5f).SetEase(Ease.OutBounce).onComplete = delegate { DotScaleBounce(); }; };
    }
}
