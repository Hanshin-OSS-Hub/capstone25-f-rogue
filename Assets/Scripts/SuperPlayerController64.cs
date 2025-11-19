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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprend = GetComponent<SpriteRenderer>(); // 뒤집기용
        h = 0f; // 이전 프레임 유도

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec;
            float i = h; // 전 프레임 입력값 가져와서 회전 자연스럽게 하기
            if (Input.GetButtonUp("Horizontal") || Input.GetButton("Horizontal")) // 스프라이트 뒤집기
                sprend.flipX = i > 0;
            h = Input.GetAxisRaw("Horizontal");
            vec = new Vector2(h*3, rigid.velocity.y);
        // rigid.AddForce(vec, ForceMode2D.Impulse); // 이동
        rigid.velocity = vec;

        if(Input.GetButtonDown("Jump"))
        {
            Vector2 vec2 = new Vector2(rigid.velocity.x, 10);
            rigid.velocity = vec2;
        }
        if (rigid.velocity.normalized.y == 0)
        {
            anim.SetBool("Jump", false);
        }
        else
            anim.SetBool("Jump", true);

        if (rigid.velocity.normalized.x == 0) // 속도 단위벡터 값이 0 이면 정지
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
        } else
            anim.SetBool("Attack", false);

    }
    void FixedUpdate()
    {
        if (rigid.velocity.x > maxSpeed) // 오른쪽이동
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); 
        else if (rigid.velocity.x < -1 * maxSpeed) //왼쪽이동
            rigid.velocity = new Vector2(-1 * maxSpeed, rigid.velocity.y); }
}
