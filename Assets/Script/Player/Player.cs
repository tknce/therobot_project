using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager GameManager;
    public TextManager TextMgr;

    public GameObject shadow;
    public GameObject playerCamera;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator animator;
    RaycastHit2D ray;

    public float MaxSpeed;

    public float MoveSpeed;
    public float JumpPower;

    public float MaxJumpCount;
    public float JumpCount;

    float h;
    float posirion_y;

    float Acctime;

    public bool jumprock;
    public bool rock;
    public bool villige_Camera;

    public float test;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        MaxSpeed = 50;
        MoveSpeed = 10;
        JumpPower = 350;
        MaxJumpCount = 1;
        jumprock = false;
        rock = false;
        //villige_Camera = false;
    }
    void Update()
    {
        Acctime += GameManager.AccTime;
        
        if(!Input.anyKey)
           rigid.velocity = new Vector2(0f, rigid.velocity.y);

        if(rigid.velocity.y < -250)
            rigid.velocity = new Vector2(0f, -250f);

        if (!rock)
        {
            Horizontal();

            if (!jumprock)
                Jump();

        }
        else
            if (Input.GetButtonDown("Jump"))
        {
            TextMgr.NextScript();
        }
        player_animation();

        ChildControl_Camera();
    }

    void Horizontal()
    {
        h = Input.GetAxisRaw("Horizontal");

        // 왼쪽 오른쪽 움직이기
        rigid.AddForce(Vector2.right * h * MoveSpeed, ForceMode2D.Impulse);

        // 왼쪽 오른쪽 애니메이션 구분
        if (0 > h)
        {
            sprite.flipX = true;
        }

        else if (0 < h)
        {
            sprite.flipX = false;
        }

        // Right Max Speed
        if (rigid.velocity.x > MaxSpeed)
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        // Left Max Speed
        else if (rigid.velocity.x < -MaxSpeed)
            rigid.velocity = new Vector2(-MaxSpeed, rigid.velocity.y);

        // 저항 없애기
        if (Input.GetButtonUp("Horizontal"))
        {
            // 단위벡터화를 이용해 반대방향으로 힘을 준다.
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 1f, rigid.velocity.y);
        }

    }
    void Jump()
    {
        Vector3 scale = new Vector3(0, -7.5f, 0);
        // RayCast
        Debug.DrawRay(rigid.position, scale, new Color(0, 1, 0));
        ray = Physics2D.Raycast(rigid.position, scale.normalized, Mathf.Abs(scale.y), LayerMask.GetMask("Tile"));

        // 충돌된 물체의 정보 출력
        /*if ( Acctime > 1)
        {          
            if(rayHit.collider.gameObject != null)
            Debug.Log("Hit object: " + rayHit.collider.gameObject.name);

            Acctime = 0;
        }*/


        if (ray.collider != null)
        {
            if (rigid.velocity.y <= 0)
                JumpCount = MaxJumpCount;
        }

        Animator anim = shadow.GetComponent<Animator>();
        if (JumpCount > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // 시험용 텍스트
                // TextMgr.Action(1, gameObject);
                --JumpCount;                
                rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);

                shadow.SetActive(true);
                shadow.transform.SetPositionAndRotation(new Vector3(transform.position.x, posirion_y, 0), Quaternion.identity);
                anim.SetBool("jump", true);
                SoundMgr.Inst.PlaySfx(SoundMgr.Sfx.Jump2);
            }
        }
        if (JumpCount == MaxJumpCount)
        {
            //anim.SetBool("jump", false);
            
            shadow.SetActive(false);
        }

    }
    void player_animation()
    {
        Animator anim = shadow.GetComponent<Animator>();
        // 달리기값
        if (0 == h)
        {
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("run", true);
        }
    }

    void ChildControl_Camera()
    {
        Vector3 cameramove = Vector3.zero;
        if (villige_Camera)
        {
            cameramove = new Vector3(this.transform.position.x,
            this.transform.position.y,
            playerCamera.transform.position.z);
        }
        else
        {
            cameramove = new Vector3(0,
                             this.transform.position.y,
                        playerCamera.transform.position.z);
        }

        playerCamera.transform.SetPositionAndRotation(cameramove, Quaternion.identity);
    }

    public void CameraSet()
    {
        Vector3 cameramove = Vector3.zero;
        cameramove = new Vector3(0,
        this.transform.position.y,
        playerCamera.transform.position.z);
    }

    public void JumpRock(bool _bool)
    {
        jumprock = _bool;
    }
    public void Rock(bool _bool)
    {
        rock = _bool;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit object: " + collision.gameObject.name);
        
        //if (collision.gameObject.name == "Tilemap" && collision.contacts[0].point.y < this.gameObject.transform.position.y + this.gameObject.transform.localScale.y / 2 )
        if(ray.collider !=null && collision.gameObject.layer == LayerMask.NameToLayer("Tile"))
        {
            //JumpCount = MaxJumpCount;
            posirion_y = collision.contacts[0].point.y + 4;
        }
        

        // Debug.Log(collision.gameObject.name);



    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        --JumpCount;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
     
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        
    }
}


