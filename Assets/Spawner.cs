using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Block[] Blocks;
    public GameObject trippleBlocks;
    GameObject currentBlocks;

    Block GetRandamBlock()
    {
        int i = Random.Range(0, Blocks.Length);
        if (Blocks[i])
        {
            return Blocks[i];
        }
        
        else
        {
            return null;
        }
    }
    public void SpawnBlock()
    {
        //currentBlocks = Instantiate(trippleBlocks);
        //currentBlocks.transform.position = new Vector3(5, 5, -5);

        Block block1 = Instantiate(GetRandamBlock());
        block1.transform.position = new Vector3(5, 5, -5);
        //block1.transform.SetParent(currentBlocks.transform, true);

        //Block block2 = Instantiate(GetRandamBlock());
        //block2.transform.position = new Vector3(5, 24, -5);
        //block2.transform.SetParent(currentBlocks.transform, true);

        //Block block3 = Instantiate(GetRandamBlock());
        //block3.transform.position = new Vector3(4.5f, 25, -5);
        //block3.transform.SetParent(currentBlocks.transform, true);
        //if (block1)
        //{
        //    return block1;
        //}
        //else if (block2)
        //{
        //    return block2;
        //}
        //else
        //{
        //    return null;
        //}
    }
}
