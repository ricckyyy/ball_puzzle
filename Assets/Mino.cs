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
    private Vector3 _velocity = new Vector3(0, -5, 0);

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
                //transform.position -= new Vector3(0, -0.5f, 0);
                
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
        grid[cellPosition.x, cellPosition.y] = transform;
        transform.position = gridLayout.CellToWorld(cellPosition);

        //if ((grid[cellPosition.x, cellPosition.y] != null) && grid[cellPosition.x + 1, cellPosition.y -1] != null)
        //{

        //    grid[cellPosition.x + 1, cellPosition.y -1] = transform;
        //    transform.position = gridLayout.CellToWorld(cellPosition);
        //}
        //else

        //{

        //    grid[cellPosition.x, cellPosition.y] = transform;
        //    transform.position = gridLayout.CellToWorld(cellPosition);
        //}
        //Debug.Log(grid[cellPosition.x, cellPosition.y]);
        //HasLine(cellPosition.y);
        //Debug.Log(grid[cellPosition.x, cellPosition.y]);
        Debug.Log("AddGrid : "+ cellPosition.x + " , " + cellPosition.y);
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
        
        if (roundX <= 0 || roundX > 9.5 || cellPosition.y < 0)
        {
            transform.position -= new Vector3(0, -0.5f, 0);
            Debug.Log(" 0 " + cellPosition.x + " , " + cellPosition.y + "," + transform.position);
            return false;
        }
        else if ((grid[cellPosition.x, cellPosition.y] != null))
        {
            Debug.Log(" 1 " + cellPosition.x + " , " + cellPosition.y + "," + transform.position);

            transform.position -= new Vector3(0, -0.5f, 0);
            if (grid[cellPosition.x + 1, cellPosition.y] == null)
            {
                transform.position += new Vector3(1, -0.5f, 0);
                Debug.Log(" 2-1 " + cellPosition.x + " , " + cellPosition.y + "," + transform.position);
                return false;
            }
            return false;
        }
        else
        {
            return true;
        }
    }
	void HasLine(int i)
    {
    for(int j = 0;j < 10; j++)
		{
			 //Debug.Log(j);
			// Debug.Log(grid[j,i]);

            //if (grid[j, i] == null)
            //return false;
        }
       //return true;
    }
}
