using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // 캔버스로 건드려야하는거 깜빡해서 그냥 오브젝트로 다 만듬. 타이틀은 나중에 갈아엎어야한다
    // Start is called before the first frame update
    public int cursorP; // 커서 위치 표시용
    
    void Start()
    {
        cursorP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 위 키가 눌렸을때 커서를 위로, 아래면 아래로. 과하게 넘어가려 하면 다시 반대편으로 보내기
        {
            if (cursorP == 0)
                cursorP = 2;
            else if (cursorP <= 2)
                cursorP--;
        } else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cursorP == 2)
                cursorP = 0;
            else if(cursorP >= 0)
                cursorP++;
        }
        transform.position = new Vector2 (-2.65f, -2.55f + (float)cursorP*-1.13f);

    }
}

