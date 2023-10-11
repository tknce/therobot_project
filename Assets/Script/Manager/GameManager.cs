using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

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
    public GameObject setting;
    public StageValue StageType;
    public GameObject light;
    Rigidbody2D rigid;

    float delay;

    public bool bactive;
    void Start()
    {
        AccTime = 0;
        Multipletime = 1;
        rigid = Player.GetComponent<Rigidbody2D>();
        StageType = StageValue.Village;
        SoundMgr.Inst.PlayBGM(true);
        bactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 배수를 설정함으로써 나중에 속도를 빠름을 볼 수 있다.
        AccTime = Time.deltaTime * Multipletime;
        time += AccTime;
        delay += AccTime;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bactive = !bactive;
            setting.SetActive(bactive);
        }
            

    }

    void MapGenerator()
    {
        Vector2 Pos = rigid.position;
    }

    public void ChangeStage(int Stage, Vector2 _pos = default(Vector2))
    {
        GameObject obj1 = GameObject.Find("Main");


        if (delay < 1)
            return;

        delay = 0;

        if (obj1 != null)
        {
            obj1.SetActive(false);
            Debug.Log(Stage);
            Debug.Log("스테이지체인지 실행 완료");
        }
        if (Stages[(int)StageType] != null)
        {
            Stages[(int)StageType].SetActive(false);
            StageType = (StageValue)Stage;
            if (StageType != StageValue.setting)            
                PlayerReset(_pos);

            SetLight(new Color(1, 1, 1));
            Player.GetComponent<Player>().villige_Camera = false;
            SoundMgr.Inst.PlaySfx(SoundMgr.Sfx.Potal);
            switch (StageType)
            {
                
                case StageValue.Village:
                    Player.GetComponent<Player>().villige_Camera = true;
                    break;                    
                case StageValue.Stage_1:
                    break;
                case StageValue.Stage_2:
                    break;
                case StageValue.setting:
                    break;
                case StageValue.EBA_Stage:
                    break;
                case StageValue.HANS_Stage:
                    SetLight(new Color(0.25f, 0.25f, 0.25f));
                    break;

            }


            Stages[(int)StageType].SetActive(true);
            return;
        }
        Debug.Log("스테이지 이동 실패");
    }
    public void ChangeStage_new(int Stage)
    {
        GameObject obj1 = GameObject.Find("Main");

        if (obj1 != null)
        {
            obj1.SetActive(false);
            Debug.Log(Stage);
            Debug.Log("스테이지체인지 실행 완료");
        }
        if (Stages[(int)StageType] != null)
        {
            Stages[(int)StageType].SetActive(false);
            StageType = (StageValue)Stage;
            if (StageType != StageValue.setting)
                PlayerReset(new Vector2(0,0));
            SetLight(new Color(1, 1, 1));
            Player.GetComponent<Player>().villige_Camera = false;
            switch (StageType)
            {

                case StageValue.Village:
                    Player.GetComponent<Player>().villige_Camera = true;
                    break;
                case StageValue.Stage_1:
                    break;
                case StageValue.Stage_2:
                    break;
                case StageValue.setting:
                    break;
                case StageValue.EBA_Stage:
                    break;
                case StageValue.HANS_Stage:
                    SetLight(new Color(0.25f, 0.25f, 0.25f));
                    break;

            }


            Stages[(int)StageType].SetActive(true);
            return;
        }
        Debug.Log("스테이지 이동 실패");
    }

    public void PlayerReset(Vector2 pos = default(Vector2))
    {        
            Player.SetActive(true);
            Player.transform.position = pos;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            // Player.GetComponent<Player>().CameraSet();
    }
    public void SetLight(Color _light)
    {
        light.GetComponent<Light2D>().color = _light;
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
        // 자기 자신 생성
        base.Awake();
    }



}
