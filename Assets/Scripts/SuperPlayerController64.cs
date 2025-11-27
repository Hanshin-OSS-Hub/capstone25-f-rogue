using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class SuperPlayerController64 : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprend;
    Animator anim;
    public float h;
    public float maxSpeed;

    /* 추가된 변수들 */
    public float dashPower = 15f;
    public float dashTime = 0.2f;
    private bool isDashing = false;
    private int facingDir = 1;
    private float defaultGravity;
    /* 추가된 변수 */

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprend = GetComponent<SpriteRenderer>(); // 뒤집기용
        h = 0f; // 이전 프레임 유도

        defaultGravity = rigid.gravityScale;//시작할 때 설정된 중력값 저장
    }

    // Update is called once per frame
    void Update()
    {
        /* 추가된 변수들 */
        if (isDashing) return; //대쉬 중일때 조작 x
        /* 추가된 변수들 */

        Vector2 vec;
        float i = h; // 전 프레임 입력값 가져와서 회전 자연스럽게 하기
        if (Input.GetButtonUp("Horizontal") || Input.GetButton("Horizontal")) // 스프라이트 뒤집기
            sprend.flipX = i < 0;
        h = Input.GetAxisRaw("Horizontal"); // 순서상 갱신 되기 전에 위의 i에서 값을 가져옴 -> 1프레임 짤짤이는 방향을 꼬아버릴 가능성. 다른 방식으로 플립하기 고려

        /* 추가된 변수들 */
        if (h > 0) facingDir = 1;//방향
        else if (h < 0) facingDir = -1;
        /* 추가된 변수들 */

        /* 추가된 변수들 */
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            StartDash();
            return;
        }
        /* 추가된 변수들 */

        vec = new Vector2(h * 3, rigid.velocity.y);
        // rigid.AddForce(vec, ForceMode2D.Impulse); // 이동
        rigid.velocity = vec;

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 vec2 = new Vector2(rigid.velocity.x, 10);
            rigid.velocity = vec2;
        }
        if (rigid.velocity.normalized.y <= 0.01f && rigid.velocity.normalized.y >= -0.01f) // 약간의 y값 이동오차를 무시, 나중에 점프 판정은 조금 수정 필요함
        {
            anim.SetBool("Jump", false);
        }
        else
            anim.SetBool("Jump", true);

        if (rigid.velocity.normalized.x == 0) // 속도 단위벡터 값이 0 이면 정지로 취급
        {
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attack", true);
        }
        else
            anim.SetBool("Attack", false);

    }
    void FixedUpdate()
    {
        /* 추가된 변수들 */
        if (isDashing) return;//대쉬 중일 때 속도제한 x
        /* 추가된 변수들 */

        if (rigid.velocity.x > maxSpeed && anim.GetBool("Attack") == false) // 오른쪽이동
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -1 * maxSpeed && anim.GetBool("Attack") == false) //왼쪽이동
            rigid.velocity = new Vector2(-1 * maxSpeed, rigid.velocity.y);
    }

    /* 추가된 변수들 */
    void StartDash()
    {
        isDashing = true;
        defaultGravity = rigid.gravityScale;
        rigid.velocity = new Vector2(facingDir * dashPower, 0);
        rigid.gravityScale = 0;
        Invoke("EndDash", dashTime);
    }

    void EndDash()
    {
        isDashing = false;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = defaultGravity;
    }
    /* 추가된 변수들 */
}