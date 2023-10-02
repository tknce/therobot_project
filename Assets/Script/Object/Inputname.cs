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
        Debug.Log("��� ����");
    }

    private void Update()
    {
        //Ű����
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
            Debug.Log("�̸� ���");
            GameManager.Inst.ChangeStage(1);
        }
    }

    //���콺
    public void InputName()
    {
        playerName = playerNameInput.text;
        // PlayerPrefs.SetString("CurrentPlayerName", playerName);
        GameManager.Inst.Player.name = playerName;
        TextManager.Inst.StartScriptText();
    }
}
