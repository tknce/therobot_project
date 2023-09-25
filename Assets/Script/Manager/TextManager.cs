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
        idx2 = 1;
        scriptspeed = 0.095f;
        Nextpage = true;
        ExitPage = true;
        arrText = new Dictionary<int, List<string>>();
        arrbool = new Dictionary<int, MyTextoption>();
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
    public bool Action(int _textnum, GameObject _obj = null, int[] _Text = null, float _scriptspeed = 0.015f, int _font = 40)
    {
        if (!isAction)
        {            
            if (!ExitPage || !TextEnter(_textnum))
            {
                return false;
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
        else
        {
            talkpanel.SetActive(isAction);
            return false;
        }
        talkpanel.SetActive(isAction);
        return true;
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
        list.Add("에바");
        list.Add("아쉽네~");
        list.Add("에바");
        list.Add("있지, 나랑 놀자! 재밌을거야!");
        list.Add(gameManager.Player.name);
        list.Add("재미는 학습데이터에 있지 않습니다.");
        list.Add("에바");
        list.Add("그게 뭐야~ 그럼 내가 가르쳐줄게!");
        list.Add("에바");
        list.Add("간단한 것부터 해볼까! 일단 웃어봐");
        list.Add(gameManager.Player.name);
        list.Add("어떻게 웃습니까?");
        list.Add("에바");
        list.Add("나를 따라해봐!");   // 미소 이모티콘
        list.Add(gameManager.Player.name);
        list.Add("웃고 있습니다.");
        list.Add("에바");
        list.Add("잘했어!!! 충분해");
        list.Add("에바");
        list.Add("놀기 전에 부탁이 있어.... 내가 놀다가 풍선을 놓쳤는데");
        list.Add("에바");
        list.Add("쓰레기 산 중턱에 걸려버렸어");
        list.Add("에바");
        list.Add("풍선 좀 가져다 줄 수 있어?");
        list.Add(gameManager.Player.name);
        list.Add("알겠습니다.");
        SetText(list); //1

        // 2번 쓰레기 산 근처로 간 후
        list = new List<string>();
        list.Add("???");
        list.Add("오호... 드디어 감정을 배우기 시작한건가...");
        SetText(list); //2

        // 3번 풍선을 얻었으면
        list = new List<string>();
        list.Add("에바");
        list.Add("우리 풍선놀이 하자!");
        list.Add(gameManager.Player.name);
        list.Add("알겠습니다.");
        SetText(list); //3

        // 4번 재밌게 논 후..
        list = new List<string>();
        list.Add("에바");
        list.Add("재미란 무엇인지 알겠어?");
        list.Add(gameManager.Player.name);
        list.Add("이러한 행동이 재미입니까?");
        list.Add("에바");
        list.Add("아니! 놀면서 느낀 감정말이야!");
        list.Add(gameManager.Player.name);
        list.Add("..............");
        list.Add(gameManager.Player.name);
        list.Add("재미.");
        list.Add(gameManager.Player.name);
        list.Add("등록했습니다.");
        list.Add("에바");
        list.Add("그래~");
        list.Add("에바");
        list.Add("배고파...");
        list.Add("에바");
        list.Add("이제 집에 갈게!");
        list.Add("에바");
        list.Add("내일 또 여기서 봐!");
        list.Add(gameManager.Player.name);
        list.Add("항상 여기 있을 겁니다.");
        list.Add("에바");
        list.Add("진짜? 그럼 내일 봐!");
        SetText(list); //4
        // 1스테이지 과학자
        list = new List<string>();
        list.Add("과학자");
        list.Add("어이 거기 로봇");
        list.Add(gameManager.Player.name);
        list.Add("누구십니까?");
        list.Add("과학자");
        list.Add("그런건 알 필요없고 저기 쓰레기 산에서 날카로운 것 좀 가져와바");
        list.Add(gameManager.Player.name);
        list.Add("무엇에 쓰실 겁니까?");
        list.Add("과학자");
        list.Add("로봇따위가 무슨 말대꾸야!!!!");
        list.Add("가져오라면 가져와!");
        list.Add(gameManager.Player.name);
        list.Add("저의 이름은 "+gameManager.Player.name + "입니다.");
        list.Add(gameManager.Player.name);
        list.Add("정당한 사유 없이 명령을 들을 순 없습니다.");
        list.Add("과학자");
        list.Add("그래?");
        list.Add("과학자");
        list.Add("좋아.. 그럼 가져온다면 너의 성능을 업그레이드 시켜주지");
        SetText(list); //5

        // 가져온다
        list = new List<string>();
        list.Add(gameManager.Player.name);
        list.Add("알겠습니다.");
        list.Add("과학자");
        list.Add("좋아...");
        list.Add("과학자");
        list.Add("쓸만한 로봇이군");
        SetText(list); //6

        // 안 가져온다
        list = new List<string>();
        list.Add(gameManager.Player.name);
        list.Add("그것은 정당한 사유가 아닙니다.");
        list.Add(gameManager.Player.name);
        list.Add("명령을 취소합니다.");
        list.Add("과학자");
        list.Add("칫..");
        list.Add("과학자");
        list.Add("깡통로봇이잖아");
        SetText(list); //7
         

        // 2스테이지 진입
        list = new List<string>();
        list.Add("에바");
        list.Add("안녕!!" + gameManager.Player.name);
        list.Add(gameManager.Player.name);
        list.Add("어서오세요");
        list.Add("에바");
        list.Add("오늘은 뭐하고 놀까~");
        list.Add("에바");
        list.Add("어! 저기에 공이 있다!");
        list.Add("에바");
        list.Add("공놀이하자!");
        list.Add(gameManager.Player.name);
        list.Add("알겠습니다.");
        SetText(list); //8

        // 공을 얻은 뒤
        list = new List<string>();
        list.Add("에바");
        list.Add("가져와줘서 고마워!!");
        SetText(list); //9

        // 공놀이 한 뒤
        list = new List<string>();
        list.Add("에바");
        list.Add("헥헥...");
        list.Add("에바");
        list.Add("재밌었다");
        list.Add("에바");
        list.Add("(안아준다)");
        list.Add("에바");
        list.Add("차가워..");
        list.Add(gameManager.Player.name);
        list.Add("......");
        list.Add("에바");
        list.Add("행복하지 않아?");
        list.Add(gameManager.Player.name);
        list.Add("행복이 무엇입니까?");
        list.Add("에바");
        list.Add("지금 이 감정! 기쁨이 넘쳐나고 뭉클해지는거야");
        list.Add(gameManager.Player.name);
        list.Add("등록했습니다.");
        list.Add("에바");
        list.Add("좋아! 그럼 내일 또 놀자~");
        SetText(list); //10

        // 밤... 과학자
        list = new List<string>();
        list.Add("과학자");
        list.Add("어이! 로봇!");
        list.Add(gameManager.Player.name);
        list.Add("저는" + gameManager.Player.name + "입니다.");
        list.Add("과학자");
        list.Add("알게 뭐야");
        list.Add("과학자");
        list.Add("로봇 주제에");
        list.Add("과학자");
        list.Add("어이 저기 쓰레기 산 꼭대기에 둥근 물체가 있거든?");
        list.Add("과학자");
        list.Add("딱 보면 알거야");
        list.Add("과학자");
        list.Add("그것 좀 가져다 줘");
        list.Add("과학자");
        list.Add("그러면 성능을 더욱 좋게 만들어주지");
        SetText(list); //11
        // 선택지 선택 후
        // 승락
        list = new List<string>();
        list.Add(gameManager.Player.name);
        list.Add("알겠습니다.");
        SetText(list); // 12

        // 거절
        list = new List<string>();
        list.Add(gameManager.Player.name);
        list.Add("정당한 사유 없이 저에게 명령할 순 없습니다.");
        list.Add("과학자");
        list.Add("칫");
        // 과학자는 뒤돌아서 간다
        list.Add("과학자");
        list.Add("잘 있어라 로봇");
        SetText(list); //13

    }

    void SetText(List<string> list, bool _setopton = false)
    {
        arrText.Add(idx2, list);
        arrbool.Add(idx2, optioninit(_setopton));
        ++idx2;
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
        idx2 = 1;
        Nextpage = false;
        scriptspeed = 0.015f;
        name1.text = "";
        talkText.text = "";
        talkText.fontSize = 40f;
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
