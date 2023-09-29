using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ImageChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Sprite Chage_img;
    public Sprite Origin_img;
    Image         img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        /*// 버튼 찾기
        button = GetComponent<Button>();    
        // onclick에 함수 연결
        button.onClick.AddListener(delegate { Changeimg(Chage_img); });
        // onclick에 끊기
        button.onClick.RemoveListener(delegate { Changeimg(Chage_img); });*/
    }

    
    public void Changeimg(Sprite _img)
    {
        img.sprite = _img;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Exit Event
        Changeimg(Origin_img);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼이 하이라이트될 때 실행할 코드를 여기에 작성합니다.
        SoundMgr.Inst.PlaySfx(SoundMgr.Sfx.button);
        Changeimg(Chage_img);
    }

}
