using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject _PauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                _PauseUI.SetActive(false); //時間を動かしボタンを非アクティブ
            }else
            {
                Time.timeScale = 0f;
                _PauseUI.SetActive(true);
            }
        }
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void ContinueGame()
    {

    }
}
