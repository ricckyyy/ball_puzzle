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
    private static float width = 100;
    private static float height = 100;

    // mino 回転
    public Vector3 rotationPoint;

    private static Transform[,] grid = new Transform[(int)width, (int)height];
    private static Transform[,] tilegrid = new Transform[(int)width, (int)height];

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
    private Vector3 _velocity = new Vector3(0, -15, 0);
    // Update is called once per frame
    void Update()
    {
        MinoMovement();
        //transform.position = transform.position + (_velocity * Time.deltaTime);
        //Debug.Log("aaaaaaa");
        
    }
    void MinoMovement()
    {
        

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
            //Debug.Log("down");

            //Vector2 myGravity = new Vector2(0, -9.81f);
            //rb.AddForce(myGravity);

            transform.position = transform.position + (_velocity * Time.deltaTime);
            //transform.position += new Vector3(0, -1, 0);

            //Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
            //transform.position = gridLayout.CellToWorld(cellPosition);
            if (!CanMove())
            {
                //transform.position -= new Vector3(0, -1, 0);

                GameObject tilemapgameobj = GameObject.Find("Tilemap");
                GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
                
                transform.position -= gridLayout.WorldToCell(new Vector3Int(0, -1, 0));
                Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
                Debug.Log(cellPosition);
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
            float roundX = Mathf.RoundToInt(children.transform.position.x * 10.0f) / 10.0f;
            float roundY = Mathf.RoundToInt(children.transform.position.y * 10.0f) / 10.0f;

            //grid[(int)roundX, (int)roundY] = children;
            //Debug.Log(roundX + "," + roundY);
            GameObject tilemapgameobj = GameObject.Find("Tilemap");
            GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(children.transform.position);
            //children.transform.position = cellPosition;
            tilegrid[cellPosition.x, cellPosition.y] = children;
            //if (tilegrid[cellPosition.x, cellPosition.y - 1 ] == null && CanMove())
            //{
            //    children.transform.position += gridLayout.CellToWorld(new Vector3Int(0, -1, 0));
            //}
            Debug.Log(children.transform.position.x + "," + children.transform.position.y + " = " + cellPosition.x + "," + cellPosition.x);
        }
    }
    // minoの移動範囲の制御
    bool CanMove()
    {
        foreach (Transform children in transform)
        {
            GameObject tilemapgameobj = GameObject.Find("Tilemap");
            GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(children.transform.position);
            //children.transform.position = cellPosition;
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            

            if (roundX < 0 || roundX > width || roundY < 0)
            {
                Debug.Log("hani gai");
                return false;
            }
            //Debug.Log(grid[roundX, roundY]);
            if (tilegrid[cellPosition.x, cellPosition.y] != null)
            {
                Debug.Log("No null");
                return false;
            }

        }
        return true;
    }
}
