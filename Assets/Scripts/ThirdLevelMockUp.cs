using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Events;

public class ThirdLevelMockUp : MonoBehaviour
{
    private LevelContext _levelContext;

    void Awake()
    {
        _levelContext = FindObjectOfType<LevelContext>();
        Debug.Log(_levelContext);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Player"))
            SkipThirdLevel();
    }

    public void SkipThirdLevel()
    {
        Debug.Log("SkipThirdLevel");
        _levelContext.IncrementRelativityFloor();
    }
}
