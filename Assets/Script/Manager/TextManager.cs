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

    // �д¿� string
    private Dictionary<int, List<string>> arrText;
    private Dictionary<int, MyTextoption> arrbool;
    private List<string> str;

    // ����� string
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
                // ��ũ��Ʈ ����
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
                // ��Ұ� ���������� ����
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

    // ã�� �ؽ�Ʈ ��ġ, �÷��̾��̸�, ��ũ��Ʈ������ �ӵ�, ��Ʈ ������
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

            // ������ ��ũ��Ʈ ã��
            arrText.TryGetValue(_textnum, out str);
            // ���ǵ� ����
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
            // ��ũ��Ʈ�� ���� ������ ��� ����
            if (ExitPage)
                Nextpage = true;

            // �������� �� �ʱ�ȭ��Ű�� ����
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
        // 1�� eba
        List<string> list = new List<string>();      
        list.Add("����");
        list.Add("�ȳ�! �̸��� ����?");
        list.Add(gameManager.Player.name);
        list.Add(WritePlayername("���� ", gameManager.Player.name, "�Դϴ�"));
        list.Add("����");
        list.Add(WritePlayername(gameManager.Player.name, "~���� �̸��̳�!"));
        list.Add("����");
        list.Add("�ູ�� �̸��̾�");
        list.Add("����");
        list.Add("���� �������?");
        list.Add(gameManager.Player.name);
        list.Add("������ ����� �̸��� ������ ���� ������� �ʽ��ϴ�.");
        arrText.Add(1, list);
        arrbool.Add(1, optioninit(false));

        // 2��
        list = new List<string>();
        list.Add("2");
        list.Add("2");
        arrText.Add(2, list);
        arrbool.Add(2, optioninit(false));

        // 3�� eba
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
        // �ʱ�ȭ
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
        // �۾� �ӵ� ����
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
        // �Լ��� ������ �� ����
        if (isAction)
        {
            // ���� ������ �ӵ�
            if (Acctime > scriptspeed)
            {
                Acctime = 0;
                // ���࿡ �������� ����ߵȴٸ�

                // �̸� �����ص� �ε����� ã�Ƽ� ��
                if (str.Count > idx)
                {
                    // �ӵ��� ���缭 ���� ������
                    if (str[idx].Length > size)
                    {
                        // �ε��� ã��
                        str1 += string.Join("", str[idx][size]);
                        ++size;
                        talkText.text = str1;
                    }
                    else
                    {
                        // �ϳ��� ���� �ϼ��Ǹ� ���� �� ����
                        // �Լ��� ���� ��ٸ��� �͵� �����ߵɵ�
                        str1 = null;
                        size = 0;
                        idx++;

                    }
                }
                else
                {
                    // �� ������ �ʱ�ȭ
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
