using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

struct MyTextoption
{
    public bool Enter;
    public bool repeat;
}

public class TextManager : Singleton<TextManager>
{
    public GameObject talkpanel;
    public GameManager gameManager;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI talkText;
    public bool Nextpage;
    public bool isAction;
    public bool ExitPage;

    float scriptspeed;

    // 읽는용 string
    private Dictionary<int, List<string>> arrText;
    private Dictionary<int, MyTextoption> arrbool;
    private List<string> str;

    // 쓰기용 string
    private string str1;

    private float Acctime;

    private int idx;
    private int[] arridx;
    private int idx2;
    private int check;


    public GameObject Player;
    private GameObject interObj;
    void Start()
    {
        scriptspeed = 0.095f;
        Nextpage = true;
        ExitPage = true;
        arrText = new Dictionary<int, List<string>>();
        arrbool = new Dictionary<int, MyTextoption>();
        idx2 = 0;
        StartScriptText();
        talkText.text = "";
        

    }
    private void FixedUpdate()
    {
        Player.GetComponent<Player>().Rock(isAction);
        if (str != null)
        {
            if (Nextpage)
            {
                // 스크립트 진행
                if (str.Count > idx + 1)
                {
                    ExitPage = false;
                    talkText.text = "";
                    name1.text = str[idx];
                    Nextpage = false;
                    StartCoroutine(Typing(str[idx + 1]));
                }
            }

           /* if (str.Count <= idx + 1)
            {
                // 요소가 남아있으면 진행
                if (arridx != null)
                    ++idx2;
                if (arridx.Length > idx2)
                {

                    Nextpage = true;
                    arrText.TryGetValue(arridx[idx2], out str);
                    idx = 0;
                }

            }*/
        }
    }

    // 찾는 텍스트 위치, 플레이어이름, 스크립트써지는 속도, 폰트 사이즈
    public void Action(int _textnum, GameObject _obj = null, int[] _Text = null, float _scriptspeed = 0.015f, int _font = 40)
    {
        if (!isAction)
        {            
            if (!ExitPage || !TextEnter(_textnum))
            {
                return;
            }            

            isAction = true;
            if (_Text != null)
            {
                arridx = _Text;               
            }

            if (_obj != Player)
                interObj = _obj;

            // 고유한 스크립트 찾기
            arrText.TryGetValue(_textnum, out str);
            // 스피드 설정
            scriptspeed = _scriptspeed;
            talkText.fontSize = _font;
            Nextpage = true;
            idx = 0;
        }
        talkpanel.SetActive(isAction);
    }

    public bool NextScript()
    {
        if (isAction)
        {
            // 스크립트가 남아 있으면 계속 진행
            if (ExitPage)
                Nextpage = true;

            // 끝났으면 다 초기화시키고 진행
            if (str.Count <= idx)
            { 
                ExitPage = true;
                initialize();
                isAction = false;
                talkpanel.SetActive(isAction);
            }

            return false;
        }
        return true;
    }
    bool TextEnter(int idx)
    {
        MyTextoption option = new MyTextoption();
        arrbool.TryGetValue(idx, out option);
        if (!option.Enter)
        {
            option.Enter = true;
            arrbool[idx] = option;
            return option.Enter;
        }
        if(option.repeat)
        {
            return option.repeat;
        }
        return false;
    }

    void StartScriptText()
    {
        // 1번 eba
        List<string> list = new List<string>();      
        list.Add("에바");
        list.Add("안녕! 이름이 뭐야?");
        list.Add(gameManager.Player.name);
        list.Add(WritePlayername("저는 ", gameManager.Player.name, "입니다"));
        list.Add("에바");
        list.Add(WritePlayername(gameManager.Player.name, "~좋은 이름이네!"));
        list.Add("에바");
        list.Add("행복한 이름이야");
        list.Add("에바");
        list.Add("누가 지어줬어?");
        list.Add(gameManager.Player.name);
        list.Add("지어준 사람의 이름은 데이터 내에 들어있지 않습니다.");
        arrText.Add(1, list);
        arrbool.Add(1, optioninit(false));

        // 2번
        list = new List<string>();
        list.Add("2");
        list.Add("2");
        arrText.Add(2, list);
        arrbool.Add(2, optioninit(false));

        // 3번 eba
        list = new List<string>();
        list.Add("3");
        list.Add("3");
        arrText.Add(3, list);
        arrbool.Add(3, optioninit(false));
    }
    string WritePlayername(string prt = "", string name = "", string str = "")
    {
        prt += name;
        prt += str;
        return prt;
    }
    void initialize()
    {
        // 초기화
        idx = 0;
        arridx = null;
        idx2 = 0;
        Nextpage = false;
        scriptspeed = 0.015f;
        name1.text = "";
        talkText.text = "";
        talkText.font = 40;
    }
    MyTextoption optioninit(bool repeat)
    {
        MyTextoption Textoption = new MyTextoption();
        Textoption.Enter = false;
        Textoption.repeat = repeat;
        return Textoption;
    }
    IEnumerator Typing(string text)
    {
        // 글씨 속도 조절
        foreach (char letter in text.ToCharArray())
        {
            talkText.text += letter;
            ExitPage = false;
            yield return new WaitForSeconds(scriptspeed);

        }
        if (idx < str.Count)
        {
            ExitPage = true;
            idx += 2;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    /*    void FixedUpdate()
    {
        Acctime += gameManager.AccTime;
        // 함수가 들어왔을 때 실행
        if (isAction)
        {
            // 글이 써지는 속도
            if (Acctime > scriptspeed)
            {
                Acctime = 0;
                // 만약에 페이지가 멈춰야된다면

                // 미리 설정해둔 인덱스를 찾아서 씀
                if (str.Count > idx)
                {
                    // 속도에 맞춰서 글이 써진다
                    if (str[idx].Length > size)
                    {
                        // 인덱스 찾기
                        str1 += string.Join("", str[idx][size]);
                        ++size;
                        talkText.text = str1;
                    }
                    else
                    {
                        // 하나의 글이 완성되면 다음 글 써짐
                        // 함수로 만들어서 기다리는 것도 만들어야될듯
                        str1 = null;
                        size = 0;
                        idx++;

                    }
                }
                else
                {
                    // 다 써지면 초기화
                    if(Nextpage)
                    {
                        ExitPage = true;
                        str1 = null;
                        str = null;
                        idx = 0;
                        scriptspeed = 0.095f;
                        talkText.fontSize = 12;
                    }
                }
            }
        }
    }*/
}
