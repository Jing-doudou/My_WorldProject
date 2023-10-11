using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Earth : BlockBase
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeGrass());
    }

    private IEnumerator MakeGrass()
    {
        yield return new WaitForSeconds(5);
        if (Text.blockList[(int)Posion.x, (int)Posion.y + 1, (int)Posion.z] == null)
        {
            Text.player.ChangeBlock(Posion, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
