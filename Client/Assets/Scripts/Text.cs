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
    public static int y = 16;//�߶�
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
    /// ��ʼ�������ש��
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

        //������

        //�ж�ש���Ƿ���ʾ
        IsFirstBlcok();
        //���ݵش�����ˮBlock
    }

    private void CreatingTerrain()
    {
        //��������ص㣬�����С��������������
        //���ɸ�ɽ����¼�ص㣬ȷ���߶Ⱥ�����һ�㣬�ٴβ�һ����



        //�����ݵأ���¼�ص�
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
    /// ����һ�������������һ�㿪ʼ����������ʧ�ܻ��ط��ؽ�
    /// </summary>
    /// <returns></returns>
    public bool CreatePicOnFirstBlock()
    {
        //���������
        int _x = UnityEngine.Random.Range(1, x - 1);
        int _z = UnityEngine.Random.Range(1, z - 1);
        //�������һ����
        for (int j = 1; j < 2 * y - 1; j++)
        {
            //����һ��Ϊ��ʱ
            if (blockList[_x, j + 1, _z] == null)
            {
                //��Ϊ�����ٴα�����������˿��Ƿ���ڣ�ʧ�ܴ���ѡ
                if (blockList[_x, j, _z] == null)
                {
                    return false;
                }

                //ɾ��һ�����������ķ�Χ
                int h =0;
                int l = 4;
                while (h<3)
                {
                    for (int rx = 0; rx <= l-h; rx++)
                    {
                        for (int ry = 0; ry <= l-h; ry++)
                        {
                            //����ǵ�ͼ��ʱ
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
    /// ��Ӧλ�ô���block
    /// </summary>
    public void CreateBlock(int i, int j, int k, int index)
    {
        blockList[i, j, k] = Instantiate(prefabBlock[index], BlockParent);
        blockList[i, j, k].Posion = new Vector3(i, j, k);
        blockList[i, j, k].transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 2) * 180, 0);//��������ĽǶȣ�
        blockList[i, j, k].gameObject.SetActive(false);
    }
    /// <summary>
    /// �ı�block
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
        blockList[i, j, k].transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 2) * 180, 0);//��������ĽǶȣ�
        blockList[i, j, k].gameObject.SetActive(true);
    }
    /// <summary>
    /// ��ʼ�� Block�Ƿ���ʾ
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
