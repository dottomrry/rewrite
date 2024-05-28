using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Script_Login : MonoBehaviour
{

    public InputField txt_ID;
    public InputField txt_pwd;
    public Text txt_info;
    string filePath;
    private InputField res;
    private GraphicRaycaster graphicRaycaster;
    // EventSystem 用于处理UI事件
    private EventSystem eventSystem;
    // Use this for initialization
    void Start()
    {
        // txt_info = GameObject.Find("Txt_Info").GetComponent<Text>();
        filePath = Application.dataPath + "/" + "users.txt";
        // 获取当前画布的 GraphicRaycaster
        graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        // 获取当前的 EventSystem
        eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // 检查是否有鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 从鼠标位置发出一条射线ws
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            DetectUI();
            // 如果射线与物体碰撞
            if (Physics.Raycast(ray, out hit))
            {
                // 显示物体的名称
                Debug.Log("Clicked on: " + hit.transform.name);
                //txt_ID += hit.transform.name;
                res.text = res.text + hit.transform.name;
            }
        }
    }
    private void DetectUI()
    {
        // 创建一个 PointerEventData 对象来存储事件数据
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        // 创建一个列表来存储射线检测结果
        List<RaycastResult> results = new List<RaycastResult>();

        // 使用 GraphicRaycaster 进行射线检测
        graphicRaycaster.Raycast(pointerData, results);

        // 遍历所有射线检测结果
        foreach (RaycastResult result in results)
        {
            // 检查是否点击了 InputField
            InputField inputField = result.gameObject.GetComponent<InputField>();
            if (inputField != null)
            {
                // 显示 InputField 的名称
                Debug.Log("Clicked on UI: " + result.gameObject.name);
                //txt_ID.text = txt_ID.text + result.gameObject.name;

                res = GameObject.Find(result.gameObject.name).GetComponent<InputField>();
                if (res == null)
                {
                    Debug.LogWarning("ERROR");
                }
                return; // 如果点击了UI组件，不再继续检测3D物体
            }
        }
    }

    // ***********************************************************************
    public void Login()
    {
        if (txt_ID.text == "")
        {
            txt_info.text = "请输入账号";
            return;
        }
        if (txt_pwd.text == "")
        {
            txt_info.text = "请输入密码";
            return;
        }

        if (Check_Login(txt_ID.text, txt_pwd.text))
        {
            txt_info.text = "登录成功";
            //
        }
        else
        {
            txt_info.text = "账号或密码错误";
        }
    }

    bool Check_Login(string id, string psw)
    {
        string[] Users = File.ReadAllLines(filePath);
        for (int i = 0; i < Users.Length; i++)
        {
            string user_id = Users[i].Split(' ')[0];
            string user_psw = Users[i].Split(' ')[1];
            if (id == user_id && psw == user_psw)
            {
                return true;
            }
        }
        return false;
    }
}
