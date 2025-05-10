using System.Collections.Generic;
using UnityEngine;

public class PhotoPool : MonoBehaviour
{
    public GameObject photoSlotPrefab;  // 存储每个相册条目的预制体
    public int poolSize = 10;           // 池大小，前后各10张
    private Queue<PhotoSlot> pool;      // 对象池

    void Start()
    {
        pool = new Queue<PhotoSlot>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            PhotoSlot slot = Instantiate(photoSlotPrefab).GetComponent<PhotoSlot>();
            slot.gameObject.SetActive(false);  // 初始为不显示
            pool.Enqueue(slot);
        }
    }

    // 从池中取出一个图片槽
    public PhotoSlot GetPhotoSlot()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            Debug.LogWarning("对象池已经空了，可能需要增加池大小！");
            return null;
        }
    }

    // 把一个图片槽返回池中
    public void ReturnPhotoSlot(PhotoSlot slot)
    {
        slot.gameObject.SetActive(false);
        pool.Enqueue(slot);
    }
}
