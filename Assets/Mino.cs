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
    public static Transform[,] grid = new Transform[(int)(float)width, (int)(float)height];
    [SerializeField] Tilemap tilemap = default;
    GameObject test;
    Rigidbody2D rb;
    private Vector3 _velocity = new Vector3(0, -5, 0);
    private object currentBlocks;

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
        
        GameObject tilemapgameobj = GameObject.Find("Tilemap");
        GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        
        grid[cellPosition.x, cellPosition.y] = transform;
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
        //cellpositonのローカル座標を取得
        Bounds b = gridLayout.GetBoundsLocal(cellPosition);

        if (roundX <= 0 || roundX > 9.5 || roundY < 0)
        {
            transform.position += new Vector3(0, 0.5f, 0);
            transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
            return false;
        }
        else if ((grid[cellPosition.x, cellPosition.y] != null))
        {
            //ballのx　 == tilemapのcell(ball)のセンターのx
            if (transform.position.x == b.center.x)
            {
                //右下にballがない場合
                if (grid[cellPosition.x + 1, cellPosition.y] == null )
                {
                    Debug.Log("x + 1 = null" + transform.position + " , " + cellPosition + " , " + b);
                    transform.position += new Vector3(0.75f, -0.5f, 0);
                    transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
                    return false;
                }
                //左下にballがない場合
                if (grid[cellPosition.x - 1, cellPosition.y] == null)
                {
                    Debug.Log("x - 1= null" + transform.position + " , " + cellPosition + " , " + b);
                    transform.position += new Vector3(-0.75f, -0.5f, 0);
                    transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
                    return false;
                }
                //右下にも左下にもballがある場合
                else
                {
                    Debug.Log("else" + transform.position + " , " + cellPosition + " , " + b);
                    transform.position += new Vector3(0, 0.5f, 0);
                    transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
                    return false;
                }
            }
            //ballのx != tilemapのcell(ball)のセンターのx
            else
            {
                Debug.Log("え" + transform.position + " , " + cellPosition + " , " + b);
                transform.position += new Vector3(0, 0.5f, 0);
                transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
                return false;
            }
            
            
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
