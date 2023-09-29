using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageValue : int
{
    setting, Village, Stage_1, Stage_2, EBA_Stage, HANS_Stage
}
public class GameManager : Singleton<GameManager>
{


    public float AccTime;
    public float time;
    public float Multipletime = 1;
    public TextManager TextManager;
    public TileManager TileManager;
    public GameObject Player;
    public GameObject[] Stages;
    public StageValue StageType;
    Rigidbody2D rigid;
    void Start()
    {
        AccTime = 0;
        Multipletime = 1;
        rigid = Player.GetComponent<Rigidbody2D>();
        StageType = StageValue.Village;
        SoundMgr.Inst.BgmPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // �ð� ����� ���������ν� ���߿� �ӵ��� ������ �� �� �ִ�.
        AccTime = Time.deltaTime * Multipletime;
        time += AccTime;
    }

    void MapGenerator()
    {
        Vector2 Pos = rigid.position;
    }

    public void ChangeStage(int Stage)
    {
        GameObject obj1 = GameObject.Find("Main");
        
        if (obj1 != null)
        {
            obj1.SetActive(false);
            Debug.Log(Stage);
            Debug.Log("��������ü���� ���� �Ϸ�");
        }
        if (Stages[(int)StageType] != null)
        {
            if(StageValue.Village == (StageValue)Stage)
            {
                Player.GetComponent<Player>().villige_Camera = true;
            }
            else
            {
                Player.GetComponent<Player>().villige_Camera = false;
            }
            Stages[(int)StageType].SetActive(false);
            StageType = (StageValue)Stage;
            if (StageType != StageValue.setting)            
                PlayerReset();
            
            Stages[(int)StageType].SetActive(true);
            return;
        }
        Debug.Log("�������� �̵� ����");
    }

    public void PlayerReset()
    {
        Player.SetActive(true);
        Player.transform.position = new Vector2(0f, 0f);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    protected override void Awake()
    {
        // �ڱ� �ڽ� ����
        base.Awake();
    }



}
