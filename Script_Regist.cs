using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Script_Regist : MonoBehaviour
{
    public InputField txt_ID;
    public InputField txt_pwd;
    public InputField txt_pwd_2;
    bool b_IfLoginSuccess;
    public Text txt_info;
    string filePath;
    StreamWriter sw;
    // GraphicRaycaster ���ڼ��UIԪ��
    private GraphicRaycaster graphicRaycaster;
    // EventSystem ���ڴ���UI�¼�
    private EventSystem eventSystem;
    private InputField res;

    // Use this for initialization
    void Start()
    {

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
    public void Regist()
    {
        // txt_ID = txt_ID.text;
        // txt_Psw = txt_pwd.text;
        // txt_Psw_2 = txt_Psw_2.text;

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
        if (txt_pwd_2.text == "")
        {
            txt_info.text = "����������";
            return;
        }

        if (txt_pwd.text != txt_pwd.text)
        {
            txt_info.text = "������������벻һ��";
        }
        else
        {
            if (CheckID(txt_ID.text) == false)
            {
                txt_info.text = "�Ѵ�����ͬ�˺�";
            }
            else
            {
                txt_info.text = "ע��ɹ�";
                //
                WriteUserInfo(txt_ID.text, txt_pwd.text);
            }
        }
    }

    void WriteUserInfo(string id, string psw)
    {
        if (!File.Exists(filePath))
        {
            sw = File.CreateText(filePath);
        }

        sw = File.AppendText(filePath);
        sw.WriteLine(id + " " + psw);
        sw.Close();
    }

    bool CheckID(string id)
    {
        string[] Users = File.ReadAllLines(filePath);
        for (int i = 0; i < Users.Length; i++)
        {
            string user_id = Users[i].Split(' ')[0];
            //string user_psw = Users[i - 1].Split( )[1];
            if (id == user_id)
            {
                return false;
            }
        }
        return true;
    }
}
