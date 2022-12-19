using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

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
    //Transform obj;
    public static Transform[,] grid = new Transform[(int)(float)width, (int)(float)height];
    [SerializeField] Tilemap tilemap = default;
    //GameObject test;
    //Rigidbody2D rb;
    private Vector3 _velocity = new Vector3(0, -5, 0);
    //private object currentBlocks;
    List<int> evennumberlist = new List<int>() {1,1,2,2,3,3,4,4};
    List<int> white = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    List<int> blue = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    List<int> red = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    List<int> green = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    int[] delline1 = new int[9] { 1, 1, 1, 1, 1, 1, 0, 0, 0 };
    int[] delline2 = new int[9] { 0, 1, 1, 1, 1, 1, 1, 0, 0 };
    int[] delline3 = new int[9] { 0, 0, 1, 1, 1, 1, 1, 1, 0 };
    int[] delline4 = new int[9] { 0, 0, 0, 1, 1, 1, 1, 1, 1 };
    private void Start()
    {
    }
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
                AddToGrid();
                this.enabled = false;
                GameObject.FindObjectOfType<Spawner>().SpawnBlock();
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1, 0);
        }
    }
    void AddToGrid()
    {  
        GameObject tilemapgameobj = GameObject.Find("Tilemap");
        GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        grid[cellPosition.x, cellPosition.y] = transform;
        //Debug.Log("AddGrid : "+ cellPosition.x + " , " + cellPosition.y);
        LineJudge(cellPosition.y, "white", white);
        LineJudge(cellPosition.y, "blue", blue);
        LineJudge(cellPosition.y, "red", red);
        LineJudge(cellPosition.y, "green", green);

        DeleLine(white.SequenceEqual(delline1), cellPosition.y, delline1);
        DeleLine(white.SequenceEqual(delline2), cellPosition.y, delline2);
        DeleLine(white.SequenceEqual(delline3), cellPosition.y, delline3);
        DeleLine(white.SequenceEqual(delline4), cellPosition.y, delline4);

        DeleLine(blue.SequenceEqual(delline1), cellPosition.y, delline1);
        DeleLine(blue.SequenceEqual(delline2), cellPosition.y, delline2);
        DeleLine(blue.SequenceEqual(delline3), cellPosition.y, delline3);
        DeleLine(blue.SequenceEqual(delline4), cellPosition.y, delline4);

        DeleLine(red.SequenceEqual(delline1), cellPosition.y, delline1);
        DeleLine(red.SequenceEqual(delline2), cellPosition.y, delline2);
        DeleLine(red.SequenceEqual(delline3), cellPosition.y, delline3);
        DeleLine(red.SequenceEqual(delline4), cellPosition.y, delline4);

        DeleLine(green.SequenceEqual(delline1), cellPosition.y, delline1);
        DeleLine(green.SequenceEqual(delline2), cellPosition.y, delline2);
        DeleLine(green.SequenceEqual(delline3), cellPosition.y, delline3);
        DeleLine(green.SequenceEqual(delline4), cellPosition.y, delline4);
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
                    //右下に落ちれるだけ落ちる
                    //yが0以上で、偶数の場合
                    if (cellPosition.y >= 0 && cellPosition.y % 2 == 0)
                    {
                        Debug.Log("中央　右　偶数");
                        for (int i = 0; i <= cellPosition.y; i ++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x + evennumberlist[i];
                            if (grid[xx,yy] == null)
                            {
                                Debug.Log(xx + " , " + yy);
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                    //yが0以上で奇数の場合
                    else if (cellPosition.y >= 0 && cellPosition.y % 2 != 0)
                    {
                        Debug.Log("中央　右　奇数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x + evennumberlist[i + 1];
                            if (grid[xx, yy] == null)
                            {
                                Debug.Log(xx + " , " + yy);
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                    return false;
                }
                //左下にballがない場合
                if (grid[cellPosition.x - 1, cellPosition.y] == null)
                {
                    //yが0以上で、偶数の場合
                    //左下に落ちれるだけ落ちる
                    if (cellPosition.y >= 0 && cellPosition.y % 2 == 0)
                    {
                        Debug.Log("中央　左　偶数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x - evennumberlist[i + 1];
                            Debug.Log(xx + " , " + yy);

                            if (grid[xx, yy] == null)
                            {
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                    //yが0以上で奇数の場合
                    else if (cellPosition.y >= 0 && cellPosition.y % 2 != 0)
                    {
                        Debug.Log("中央　左　奇数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x - evennumberlist[i];
                            Debug.Log(xx + " , " + yy);
                            if (grid[xx, yy] == null)
                            {
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                    return false;
                }
                //右下にも左下にもballがある場合
                if (grid[cellPosition.x + 1, cellPosition.y] != null && grid[cellPosition.x - 1, cellPosition.y] != null)
                {
                    Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                    transform.position += new Vector3(0, 0.5f, 0);
                    transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
                    return false;
                }
                return false;
            }
            //ballのx != tilemapのcell(ball)のセンターのx
            else
            {
                //gridにあるballの　右　にズレて落ちてきた場合
                if (transform.position.x - b.center.x > 0 && grid[cellPosition.x + 1, cellPosition.y] == null)
                {
                    //偶数の場合
                    if (cellPosition.y >= 0 && cellPosition.y % 2 == 0)
                    {
                        Debug.Log("右ずれ　偶数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x + evennumberlist[i];
                            Debug.Log(xx + " , " + yy);
                            if (grid[xx, yy] == null)
                            {
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                    //奇数の場合
                    else if (cellPosition.y >= 0 && cellPosition.y % 2 != 0)
                    {
                        Debug.Log("右ずれ 奇数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x + evennumberlist[i + 1];
                            Debug.Log(xx + " , " + yy);
                            if (grid[xx, yy] == null)
                            {
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                }
                //gridにあるballの　左　にズレて落ちてきた場合
                else if (transform.position.x - b.center.x < 0 && grid[cellPosition.x - 1, cellPosition.y] == null)
                {
                    if (cellPosition.y >= 0 && cellPosition.y % 2 == 0)
                    {
                        Debug.Log("左ずれ　偶数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x - evennumberlist[i + 1];
                            Debug.Log(xx + " , " + yy);
                            if (grid[xx, yy] == null)
                            {
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                    else if (cellPosition.y >= 0 && cellPosition.y % 2 != 0)
                    {
                        Debug.Log("左ずれ 奇数");
                        for (int i = 0; i <= cellPosition.y; i++)
                        {
                            int yy = cellPosition.y - i;
                            int xx = cellPosition.x - evennumberlist[i];
                            Debug.Log(xx + " , " + yy);
                            if (grid[xx, yy] == null)
                            {
                                Vector3Int newcellpos = new Vector3Int(xx, yy, -5);
                                Debug.Log(transform.position + " , " + cellPosition + " , " + b);
                                transform.position = gridLayout.CellToWorld(newcellpos);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("else else" + transform.position + " , " + cellPosition + " , " + b);
                    transform.position += new Vector3(0, 0.5f, 0);
                    transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(transform.position));
                }
                return false;
            }
        }
        else
        {
            return true;
        }
    }
	bool LineJudge(int i,string boolname,List<int> boolnamelist)
    {
    for(int j = 0;j < 10; j++)
		{
            if (grid[j, i] != null )
            {
                if(grid[j,i].ToString().StartsWith(boolname))
                {
                    boolnamelist[j-1] = 1;
                }
            }
            //    return false;
        }
        Debug.Log(string.Join(",", white));
        return true;
    }
    public void RawDown(int i)
    {
        GameObject tilemapgameobj = GameObject.Find("Tilemap");
        GridLayout gridLayout = tilemapgameobj.GetComponent<GridLayout>();

        for (int y = i; y < 10; y++)
        {
            for (int j = 1; j < 10; j++)
            {
                if (grid[j, y] != null)
                {
                    Debug.Log(grid[j, y]);
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;

                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                    grid[j, y - 1].transform.position = gridLayout.CellToWorld(gridLayout.WorldToCell(grid[j, y - 1].transform.position));
                }
            }
        }
    }
    void DeleLine(bool result,int y,int[] dellist)
    {
        if (result == true)
        {
            for (int d = 1; d < 10; d++)
            {
                if (dellist[d - 1] == 1)
                {
                    Destroy(grid[d, y].gameObject);
                    grid[d, y] = null;
                }
            }
            Debug.Log("Destroy");
            RawDown(y);
        }
    }
}