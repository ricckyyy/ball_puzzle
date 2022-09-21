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
    Transform obj;
    private static Transform[,] grid = new Transform[width, height];
    private static Transform[,] tilegrid ;

    GameObject test;
    Rigidbody2D rb;

    void Start()
    {
    }
    private Vector3 _velocity = new Vector3(0, -15, 0);
    // Update is called once per frame
    void Update()
    {
        MinoMovement();

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
            )
        {
            transform.position += new Vector3(0, -1, 0);
            if (!CanMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                this.enabled = false;
                GameObject.FindObjectOfType<Spawner>().SpawnBlock();
 
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1, 0);
            //transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }
    }
    void AddToGrid()
    {  
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        grid[roundX, roundY] = this.transform;
    }
    // minoの移動範囲の制御
    bool CanMove()
    {
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        //GameObject tilemapgameobj = GameObject.Find("Tilemap");
        //GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
        //Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        //transform.position = gridLayout.CellToWorld(cellPosition);

        if (roundX < 0 || roundX > width || roundY < 0)
        {
            return false;
        }
        else if (grid[roundX, roundY] != null)
        {
            Debug.Log("No null");
            return false;
        }
        else
        {
            return true;
        }
    }
}
