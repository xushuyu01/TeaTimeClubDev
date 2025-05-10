using System.Collections.Generic;
using UnityEngine;

public class PhotoPool : MonoBehaviour
{
    public GameObject photoSlotPrefab;  // �洢ÿ�������Ŀ��Ԥ����
    public int poolSize = 10;           // �ش�С��ǰ���10��
    private Queue<PhotoSlot> pool;      // �����

    void Start()
    {
        pool = new Queue<PhotoSlot>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            PhotoSlot slot = Instantiate(photoSlotPrefab).GetComponent<PhotoSlot>();
            slot.gameObject.SetActive(false);  // ��ʼΪ����ʾ
            pool.Enqueue(slot);
        }
    }

    // �ӳ���ȡ��һ��ͼƬ��
    public PhotoSlot GetPhotoSlot()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            Debug.LogWarning("������Ѿ����ˣ�������Ҫ���ӳش�С��");
            return null;
        }
    }

    // ��һ��ͼƬ�۷��س���
    public void ReturnPhotoSlot(PhotoSlot slot)
    {
        slot.gameObject.SetActive(false);
        pool.Enqueue(slot);
    }
}
