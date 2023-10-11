using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum Block_Type
{
    Rock, Earth, Grass, Bronze, Iron, Masonry, Water
}
//岩石，泥土，泥土草，青铜，铁，砖石，水

public class BlockBase : MonoBehaviour
{
    private int hp = 2;
    private Vector3 posion;
    public Block_Type blockType;

    public Vector3 Posion
    {
        get => posion; set
        {
            posion = value;
            gameObject.name = value.ToString("F0");
            gameObject.transform.position = posion;
        }
    }

    public int Hp
    {
        get => hp; set
        {
            if (hp <= 0)
            {
                hp = 0;
                DesEvent();

            }
            hp = value;
        }
    }

    public void DisplayBlock()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
    }
    /// <summary>
    /// Block Destory event 
    /// </summary>
    public void DesEvent()
    {
        int x = (int)Posion.x;
        int y = (int)Posion.y;
        int z = (int)Posion.z;
        if (Text.blockList[x, y + 1, z] != null)
        {
            Text.blockList[x, y + 1, z].DisplayBlock();
        }
        if (Text.blockList[x, y - 1, z] != null)
        {
            Text.blockList[x, y - 1, z].DisplayBlock();
        }

        if (Text.blockList[x + 1, y, z] != null)
        {
            Text.blockList[x + 1, y, z].DisplayBlock();
        }
        if (Text.blockList[x - 1, y, z] != null)
        {
            Text.blockList[x - 1, y, z].DisplayBlock();
        }

        if (Text.blockList[x, y, z + 1] != null)
        {
            Text.blockList[x, y, z + 1].DisplayBlock();
        }
        if (Text.blockList[x, y, z - 1] != null)
        {
            Text.blockList[x, y, z - 1].DisplayBlock();
        }
        Text.blockList[x, y, z] = null;
        Destroy(gameObject);
    }
}
