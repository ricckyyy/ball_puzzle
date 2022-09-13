using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Mino : MonoBehaviour
{
    //[SerializeField]
    //private Tilemap tilemap;

    public float previosTime;

    // 落ちる時間
    public float fallTime = 1f;

    // ステージサイズ
    private static int width = 100;
    private static int height = 100;

    // mino 回転
    public Vector3 rotationPoint;

    private static Transform[,] grid = new Transform[width, height];
    GameObject test;
    Rigidbody2D rb;

    //public Vector3 target = new Vector3(0, -300, 0);
    //public float speed = 0f;
    void Start()
    {
        //test = GameObject.Find("0(Clone)");
        //gameObject.AddComponent<Rigidbody2D>();
        //rb = gameObject.GetComponent<Rigidbody2D>();
        //Debug.Log(rb);
        //Vector2 myGravity = new Vector2(0, 9.81f);
    }
    private Vector3 _velocity = new Vector3(0, -0.02f, 0);
    // Update is called once per frame
    void Update()
    {
        MinoMovement();
        //transform.position = transform.position + (_velocity * Time.deltaTime);
        //Debug.Log("aaaaaaa");
        
    }
    void MinoMovement()
    {
        //GameObject tilemapgameobj = GameObject.Find("Tilemap (1)");
        //GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();

        // 左入力
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CanMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        //右入力
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!CanMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        //下入力　＋　自動下移動
        else if (Input.GetKeyDown(KeyCode.DownArrow)
            || Time.time > 0
            )
        {
            //Vector2 myGravity = new Vector2(0, -9.81f);
            //rb.AddForce(myGravity);

            //transform.position = transform.position + (_velocity * Time.deltaTime);
            //transform.position += new Vector3(0, -1, 0);

            //Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
            //transform.position = gridLayout.CellToWorld(cellPosition);
            if (!CanMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                //Debug.Log("CanMove False");
                AddToGrid();
                this.gameObject.transform.DetachChildren();
                GameObject.FindObjectOfType<Spawner>().SpawnBlock();

                Destroy(this.gameObject, 1.0f);

                this.enabled = false;
            }
            //previosTime = Time.time;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1, 0);
            //transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }
    }


    //bool HasLine(int i)
    //{
    //    for(int j = 0;j < width; j++)
    //    {
    //        if (grid[j, i] == null)
    //            return false;
    //    }
    //    return true;
    //}
    ////列を消す
    //void DeleteLine(int i)
    //{
    //    for(int j = 0; j < width; j++)
    //    {
    //        Destroy(grid[j, i].gameObject);
    //        grid[j, i] = null;
    //    }
    //}
    ////列を下げる
    //public void RawDown(int i)
    //{
    //    for(int y = i; y < height; y++)
    //    {
    //        for(int j = 0; j < width; j++)
    //        {
    //            if(grid[j, y] != null)
    //            {
    //                grid[j, y - 1] = grid[j, y];
    //                grid[j, y] = null;
    //                grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
    //            }
    //        }
    //    }
    //}
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundX, roundY] = children;
            Debug.Log(roundX　+ "," + roundY);
            //Vector3Int cellPosition = gridLayout.WorldToCell(roundX, roundY,0);
        }
    }
    // minoの移動範囲の制御
    bool CanMove()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundX < 0 || roundX > width || roundY < 0)
            {
                return false;
            }
            //Debug.Log(grid[roundX, roundY]);
            if (grid[roundX, roundY] != null)
            {
                return false;
            }

        }
        return true;
    }
}
