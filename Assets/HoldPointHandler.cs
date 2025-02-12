using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPointHandler : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        gameObject.transform.SetParent(mainCamera.transform);
        gameObject.transform.position = mainCamera.transform.position + new Vector3(0f, 0, 1f);
        gameObject.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(-90f, 0, 90f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
