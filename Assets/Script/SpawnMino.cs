using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System.Linq;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;
    int[] delline1 = new int[] { 1, 1, 1, 1, 1, 1};
    int[] delline2 = new int[9] { 0, 1, 1, 1, 1, 1, 1, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        //bool a = delline2.Contains(delline1);
        //Debug.Log(a);
        //await SendWebRequest();
        // 通信完了を待ってから以下の処理が呼ばれる
        //Debug.Log("Do something");
    }

    //public void NewMino()
    //{
    //    Instantiate(Minos[Random.Range(0, Minos.Length)], transform.position, Quaternion.identity);
    //}
    //async UniTask SendWebRequest()
    //{
    //    var request = UnityWebRequest.Get("https://www.google.com/");
    //    await request.SendWebRequest();
        // 以下は通信が終わった後に処理される
    //}
}
