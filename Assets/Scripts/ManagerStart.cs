using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerStart : MonoBehaviour
{
    private ParameterController parameter;
    private GameObject[] GuideObjects;
    // Start is called before the first frame update


    void Start()
    {
        parameter = GameObject.Find("ParameterSystem").GetComponent<ParameterController>();
    }

    /*ボタンが押された時の処理*/
    public void EasyGame()
    {       
        parameter.SetParameterEasy();
        SceneManager.LoadScene("Main");
    }

    public void NormalGame()
    {
        parameter.SetParameterNormal();
        SceneManager.LoadScene("Main");
    }

    public void HardGame()
    {
        parameter.SetParameterHard();
        SceneManager.LoadScene("Main");
    }

    //操作説明
    public void GuideGame()
    {
        GuideObjects = GameObject.FindGameObjectsWithTag("Guide");

        foreach(GameObject GuideObject in GuideObjects)
        {
            GuideObject.GetComponent<Text>().enabled = !GuideObject.GetComponent<Text>().enabled;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
