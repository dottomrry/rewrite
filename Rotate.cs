using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GameObject code;
    public float rotationSpeed = 90f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        code.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

