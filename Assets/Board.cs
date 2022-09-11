using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Transform[,] grid;


    [SerializeField]
    private Transform emptySprite;

    [SerializeField]
    private int height = 30, width = 10, header = 8;
    float z;
    private void Awake()
    {
        grid = new Transform[width, height];
    }

    private void Start()
    {
        CreateBoard();
    }
    void CreateBoard()
    {
        if (emptySprite)
        {
            for (int y = 0; y < height - header; y++)
            {
                if (y % 2 == 1)
                {
                    z = 0.5f;
                }
                else
                {
                    z = 0;
                }
                for (float x = 0 + z; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite, new Vector3(x, y, 0),Quaternion.identity);
                    clone.transform.parent = transform;
                }

            }
        }
    }

    public bool CheckPosition(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);
            if (!BoardOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }
            if (BlockCheck((int)pos.x, (int)pos.y,block))
            {
                return false;
            }
        }
        return true;
    }
    public bool CheckPosition2(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);
            if ((int)pos.y % 2 == 1)
            {
                Debug.Log("false");
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }
    bool BoardOutCheck(int x,int y)
    {
        //x軸は0以上width未満、y軸は0以上
        return (x >= 0 && x < width && y >= 0);
    }

    //移動先にブロックがないか判定する関数
    bool BlockCheck(int x, int y,Block block)
    {
        return (grid[x, y] != null && grid[x, y].parent != block.transform);
    }

    //ブロックが落ちたポジションを記録する関数
    public void SaveBlockInGrid(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            grid[(int)pos.x, (int)pos.y] = item;
        }
    }
}
