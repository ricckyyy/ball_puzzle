using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Spawner spawner;
    Block activeBlock;
    [SerializeField]
    private float dropInterval = 0.25f;
    float nextdropTimer;

    Board board;

    private void Start()
    {
        GameObject.FindObjectOfType<Spawner>().SpawnBlock();

        //board = GameObject.FindObjectOfType<Board>();

        //if (!activeBlock)
        //{
        //    activeBlock = spawner.SpawnBlock();
        //}
    }
    private void Update()
    {
        if (Time.time > nextdropTimer)
        {
             nextdropTimer = Time.time + dropInterval;
            if (activeBlock)
            {
                activeBlock.MoveDown();

                if (!board.CheckPosition(activeBlock))
                {
					activeBlock.MoveUp();

					board.SaveBlockInGrid(activeBlock);

					//activeBlock = spawner.SpawnBlock();
				
                }
                
            }
        }
        
    }
}
