using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageValue : int
{
    setting, Village, Stage_1, Stage_2
}
public class GameManager : Singleton<GameManager>
{


    public float AccTime;
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
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 배수를 설정함으로써 나중에 속도를 빠름을 볼 수 있다.
        AccTime = Time.deltaTime * Multipletime;
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
            Debug.Log("스테이지체인지 실행 완료");
        }
        if (Stages[(int)StageType] != null)
        {

            Stages[(int)StageType].SetActive(false);
            StageType = (StageValue)Stage;
            if (StageType != StageValue.setting)            
                PlayerReset();
            
            Stages[(int)StageType].SetActive(true);
            return;
        }
        Debug.Log("스테이지 이동 실패");
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
        // 자기 자신 생성
        base.Awake();
    }



}
