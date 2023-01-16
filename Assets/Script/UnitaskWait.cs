using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class UnitaskWait : MonoBehaviour
{
    public async void Start()
    {
        Debug.Log("Start UniTask!");
        await Wait1Sec();
        Debug.Log("Hello World UniTask!");
    }

    // 1秒待機するUniTask  
    public async UniTask Wait1Sec()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }
	
}
