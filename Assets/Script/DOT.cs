using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class DOT : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        // transform.DOMove(Vector3.up,5); // 첫번째 인자 방향, 두번째 시간
        rectTransform = GetComponent<RectTransform>(); // UI 움직일 때 사용
        rectTransform.DOAnchorPosX(-770, 2).onComplete = delegate { testcollback(-1100); };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void testcollback(float _x)
    {
        rectTransform.DOAnchorPosX(_x, 2).onComplete = delegate { testcollback1(770); };
        //DOTween.To(() => rectTransform.position, x => rectTransform.position = x, new Vector3(3, 4, 8), 6).From();
    }
    void testcollback1(float _x)
    {
        //rectTransform.position = new Vector3(1100, rectTransform.position.y, rectTransform.position.z);
        rectTransform.DOAnchorPosX(1100, 0);
        rectTransform.DOAnchorPosX(_x, 2).onComplete = delegate { testcollback2(1100); };
        //rectTransform.DOAnchorPosX(_x, 3).SetEase(Ease.OutBack).onComplete = delegate { testcollback2(1100); };
    }
    void testcollback2(float _x)
    {
        rectTransform.DOAnchorPosX(_x, 2).onComplete = delegate { testcallback3(1100); }; ;
    }
    void testcallback3(float _y)
    {
        rectTransform.DOAnchorPos(new Vector2(0,1200) , 0);
        rectTransform.DOAnchorPosY(200, 4);
    }

}
