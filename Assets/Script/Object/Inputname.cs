using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inputname : MonoBehaviour
{
    
    public  TMP_InputField playerNameInput;
    private string playerName = null;

    private void Awake()
    {
        playerName = new string("player");
        Debug.Log("등록 성공");
    }

    private void Update()
    {
        //키보드
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
            Debug.Log("이름 등록");
            GameManager.Inst.ChangeStage(1);
        }
    }

    //마우스
    public void InputName()
    {
        playerName = playerNameInput.text;
        // PlayerPrefs.SetString("CurrentPlayerName", playerName);
        GameManager.Inst.Player.name = playerName;
        TextManager.Inst.StartScriptText();
    }
}
