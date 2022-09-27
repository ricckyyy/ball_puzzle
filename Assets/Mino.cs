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
    public static Transform[,] grid = new Transform[(int)width, (int)height];
    
    GameObject test;
    Rigidbody2D rb;
    private Vector3 _velocity = new Vector3(0, -2, 0);

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
            transform.position += new Vector3(-0.5f, 0, 0);
            if (!CanMove())
            {
                transform.position -= new Vector3(-0.5f, 0, 0);
            }
        }
        //右入力
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.5f, 0, 0);
            if (!CanMove())
            {
                transform.position -= new Vector3(0.5f, 0, 0);
            }
        }
        //下入力　＋　自動下移動
        else if (Input.GetKeyDown(KeyCode.DownArrow)|| Time.time > 0
            )
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -0.5f, 0);

            }
            transform.position = transform.position + (_velocity * Time.deltaTime);

            if (!CanMove())
            {
                Debug.Log("down");
                transform.position -= new Vector3(0, -0.5f, 0);
                
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
        int roundZ = Mathf.RoundToInt(transform.position.z);
        GameObject tilemapgameobj = GameObject.Find("Tilemap");
        GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition);
        grid[cellPosition.x, cellPosition.y] = transform;
        //Debug.Log(cellPosition.x + " , " + cellPosition.y);
    }
    // minoの移動範囲の制御
    bool CanMove()
    {
        int roundX = Mathf.RoundToInt(transform.position.x);
        int roundY = Mathf.RoundToInt(transform.position.y);
        GameObject tilemapgameobj = GameObject.Find("Tilemap");
        GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        //transform.position = gridLayout.CellToWorld(cellPosition);
        //Debug.Log(cellPosition.x + ",,," + cellPosition.y + " = " + transform.position);
        
        if (cellPosition.x <= 0 || roundX > width || cellPosition.y < 0)
        {
            return false;
        }
        else if (grid[cellPosition.x, cellPosition.y] != null)
        {
            Debug.Log(transform.position);
            return false;
        }
        else
        {
            return true;
        }
    }
}
