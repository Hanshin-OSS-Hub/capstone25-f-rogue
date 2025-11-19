using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// 빌드할 때에 에디터 종료 커맨드 꼭 빼고 만들것
public class GameDirector : MonoBehaviour // 사실상 메뉴 관리자 느낌일듯
{
    GameObject cursor;
    int menu;
    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("cursor"); // cursor 오브젝트를 찾아서
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit")) // 선택 버튼을 입력받았을때
        {
            switch (cursor.GetComponent<Cursor>().cursorP){ // cursor 오브젝트의 Cursor 스크립트에서, 공개변수 cursorP값을 찾아, 그 값에 따라 메뉴 실행
                case 0:
                    GameStart();
                    break;
                case 1:
                    GameSetting();
                    Debug.Log("세팅 미구현");
                    break;
                case 2:
                    GameExit();
                    break;
            }
            if(cursor.GetComponent<Cursor>().cursorP == 0) // 커서값이 0 == 스타트에 놓여있다면
            {

            } else if (cursor.GetComponent<Cursor>().cursorP == 1)
            {

            } else if (cursor.GetComponent<Cursor>().cursorP == 2)
            {

            }
        }
    }

    void GameExit()
    {
        // UnityEditor.EditorApplication.isPlaying = false; // 에디터 종료. 실 게임에선 빼줘야함
        Application.Quit(); // 게임 종료
    }
    void GameStart()
    {
        SceneManager.LoadScene("Tutorial");
    }
    void GameSetting()
    {

    }
}
