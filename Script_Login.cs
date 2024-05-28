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
    // EventSystem ���ڴ���UI�¼�
    private EventSystem eventSystem;
    // Use this for initialization
    void Start()
    {
        // txt_info = GameObject.Find("Txt_Info").GetComponent<Text>();
        filePath = Application.dataPath + "/" + "users.txt";
        // ��ȡ��ǰ������ GraphicRaycaster
        graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        // ��ȡ��ǰ�� EventSystem
        eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ƿ������������
        if (Input.GetMouseButtonDown(0))
        {
            // �����λ�÷���һ������ws
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            DetectUI();
            // ���������������ײ
            if (Physics.Raycast(ray, out hit))
            {
                // ��ʾ���������
                Debug.Log("Clicked on: " + hit.transform.name);
                //txt_ID += hit.transform.name;
                res.text = res.text + hit.transform.name;
            }
        }
    }
    private void DetectUI()
    {
        // ����һ�� PointerEventData �������洢�¼�����
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        // ����һ���б����洢���߼����
        List<RaycastResult> results = new List<RaycastResult>();

        // ʹ�� GraphicRaycaster �������߼��
        graphicRaycaster.Raycast(pointerData, results);

        // �����������߼����
        foreach (RaycastResult result in results)
        {
            // ����Ƿ����� InputField
            InputField inputField = result.gameObject.GetComponent<InputField>();
            if (inputField != null)
            {
                // ��ʾ InputField ������
                Debug.Log("Clicked on UI: " + result.gameObject.name);
                //txt_ID.text = txt_ID.text + result.gameObject.name;

                res = GameObject.Find(result.gameObject.name).GetComponent<InputField>();
                if (res == null)
                {
                    Debug.LogWarning("ERROR");
                }
                return; // ��������UI��������ټ������3D����
            }
        }
    }

    // ***********************************************************************
    public void Login()
    {
        if (txt_ID.text == "")
        {
            txt_info.text = "�������˺�";
            return;
        }
        if (txt_pwd.text == "")
        {
            txt_info.text = "����������";
            return;
        }

        if (Check_Login(txt_ID.text, txt_pwd.text))
        {
            txt_info.text = "��¼�ɹ�";
            //
        }
        else
        {
            txt_info.text = "�˺Ż��������";
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
