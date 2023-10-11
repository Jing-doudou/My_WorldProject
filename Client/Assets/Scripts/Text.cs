using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Text : MonoBehaviour
{
    public static Text player;

    public List<BlockBase> prefabBlock = new List<BlockBase>();
    public Transform BlockParent;
    public static BlockBase[,,] blockList;
    public static int x = 16;
    public static int z = 16;
    public static int y = 16;//高度
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player/Main Camera").GetComponent<Text>();

        InitWorldBlock();

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 初始化世界的砖块
    /// </summary>
    private void InitWorldBlock()
    {

        blockList = new BlockBase[x, 2 * y, z];
        for (int i = 1; i < x - 1; i++)
        {
            for (int j = 1; j < y - 1; j++)
            {
                for (int k = 1; k < z - 1; k++)
                {
                    CreateBlock(i, j, k, 0);
                }
            }
        }
        CreatingTerrain();

        //生成土

        //判断砖块是否显示
        IsFirstBlcok();
        //在洼地处生成水Block
    }

    private void CreatingTerrain()
    {
        //生成随机地点，随机大小，发送至服务器
        //生成高山，纪录地点，确定高度后生成一层，再次层一个点



        //生成洼地，记录地点
        int picNum = UnityEngine.Random.Range(4, Math.Max(4, x / 2));
        bool succ;
        for (int i = 0; i < picNum; i++)
        {
            succ = CreatePicOnFirstBlock();
            if (!succ)
            {
                picNum--;
            }

        }
    }
    /// <summary>
    /// 创建一个坑在最上面的一层开始创建，创建失败换地方重建
    /// </summary>
    /// <returns></returns>
    public bool CreatePicOnFirstBlock()
    {
        //随机的坐标
        int _x = UnityEngine.Random.Range(1, x - 1);
        int _z = UnityEngine.Random.Range(1, z - 1);
        //此坐标的一列内
        for (int j = 1; j < 2 * y - 1; j++)
        {
            //上面一块为空时
            if (blockList[_x, j + 1, _z] == null)
            {
                //因为可能再次被随机到，检查此块是否存在，失败从新选
                if (blockList[_x, j, _z] == null)
                {
                    return false;
                }

                //删除一个倒金字塔的范围
                int h =0;
                int l = 4;
                while (h<3)
                {
                    for (int rx = 0; rx <= l-h; rx++)
                    {
                        for (int ry = 0; ry <= l-h; ry++)
                        {
                            //如果是地图外时
                            if (_x+rx>x-1||_z+ry>z-1)
                            {
                                continue;
                            }
                            if (blockList[_x + rx, j-h, _z + ry] != null)
                            {
                                ChangeBlock(new Vector3(_x + rx, j-h, _z + ry), -1);
                            }
                        }
                    }
                    h++;
                }
                return true;
            }
        }
        return true;
    }
    /// <summary>
    /// 对应位置创建block
    /// </summary>
    public void CreateBlock(int i, int j, int k, int index)
    {
        blockList[i, j, k] = Instantiate(prefabBlock[index], BlockParent);
        blockList[i, j, k].Posion = new Vector3(i, j, k);
        blockList[i, j, k].transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 2) * 180, 0);//自身坐标的角度，
        blockList[i, j, k].gameObject.SetActive(false);
    }
    /// <summary>
    /// 改变block
    /// </summary>
    public void ChangeBlock(Vector3 v, int index)
    {
        int i = (int)v.x;
        int j = (int)v.y;
        int k = (int)v.z;

        if (index == -1)
        {
            blockList[i, j, k].DesEvent();
            return;
        }
        Destroy(blockList[i, j, k].gameObject);
        blockList[i, j, k] = Instantiate(prefabBlock[index], BlockParent);
        blockList[i, j, k].Posion = new Vector3(i, j, k);
        blockList[i, j, k].transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 2) * 180, 0);//自身坐标的角度，
        blockList[i, j, k].gameObject.SetActive(true);
    }
    /// <summary>
    /// 初始化 Block是否显示
    /// </summary>
    /// <returns></returns>
    public void IsFirstBlcok()
    {
        for (int i = 1; i < x - 1; i++)
        {
            for (int j = 1; j < z - 1; j++)
            {
                for (int k = 1; k < y - 1; k++)
                {
                    if (blockList[i, k + 1, j] == null && blockList[i, k, j] != null)
                    {
                        blockList[i, k, j].gameObject.SetActive(true);
                    }
                }
            }
        }
    }

}
