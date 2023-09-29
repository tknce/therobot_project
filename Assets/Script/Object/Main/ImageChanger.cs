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

        /*// ��ư ã��
        button = GetComponent<Button>();    
        // onclick�� �Լ� ����
        button.onClick.AddListener(delegate { Changeimg(Chage_img); });
        // onclick�� ����
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
        // ��ư�� ���̶���Ʈ�� �� ������ �ڵ带 ���⿡ �ۼ��մϴ�.
        SoundMgr.Inst.PlaySfx(SoundMgr.Sfx.button);
        Changeimg(Chage_img);
    }

}
