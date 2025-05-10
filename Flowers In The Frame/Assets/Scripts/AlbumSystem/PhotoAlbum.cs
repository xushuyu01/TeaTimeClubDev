using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PhotoAlbum : MonoBehaviour
{
    public Button btnPrev;
    public Button btnNext;

    public GameObject photoSlotPrefab;  // 拖入 PhotoSlot 的 Prefab
    public Transform leftPageTransform; // 左页显示位置
    public Transform rightPageTransform; // 右页显示位置

    private List<string> allPhotoPaths = new List<string>();
    private int currentPage = 0; // 每次表示左页索引（0, 2, 4...）

    void Start()
    {
        Debug.Log("persistentDataPath: " + Application.persistentDataPath);
        LoadAllPhotoPaths();
        UpdatePages();

        btnPrev.onClick.AddListener(PrevPage);
        btnNext.onClick.AddListener(NextPage);
    }

    void LoadAllPhotoPaths()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "photo_level_*.png");
        allPhotoPaths = files.OrderByDescending(f => f).ToList();
        Debug.Log("加载到的所有图片路径:");
        foreach (var path in allPhotoPaths)
        {
            Debug.Log(path);  // 打印图片路径
        }
    }

    void UpdatePages()
    {
        // 删除之前的图片
        ClearPages();

        // 创建并显示新的左页和右页
        GameObject leftSlotObj = Instantiate(photoSlotPrefab, leftPageTransform);
        GameObject rightSlotObj = Instantiate(photoSlotPrefab, rightPageTransform);

        // 确保新实例化的对象被正确设置在左页和右页的位置上
        leftSlotObj.transform.localPosition = Vector3.zero;  // 根据需要设置偏移
        rightSlotObj.transform.localPosition = Vector3.zero;  // 根据需要设置偏移

        // 获取 PhotoSlot 组件
        PhotoSlot leftSlot = leftSlotObj.GetComponent<PhotoSlot>();
        PhotoSlot rightSlot = rightSlotObj.GetComponent<PhotoSlot>();

        // 加载并显示图片
        LoadAndDisplaySlot(currentPage, leftSlot);
        LoadAndDisplaySlot(currentPage + 1, rightSlot);
    }

    void LoadAndDisplaySlot(int index, PhotoSlot slot)
    {
        if (index >= 0 && index < allPhotoPaths.Count)
        {
            string path = allPhotoPaths[index];
            if (File.Exists(path)) // 检查文件是否存在
            {
                Debug.Log("文件存在: " + path);  // 打印文件路径
                byte[] data = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(data);
                slot.SetTexture(tex);
            }
            else
            {
                Debug.LogWarning("文件不存在: " + path);  // 如果文件不存在，打印警告
            }
        }
        else
        {
            slot.Clear(); // 显示空白页
        }
    }

    void ClearPages()
    {
        // 删除当前的左页和右页
        foreach (Transform child in leftPageTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in rightPageTransform)
        {
            Destroy(child.gameObject);
        }
    }

    void PrevPage()
    {
        if (currentPage - 2 >= 0)
        {
            currentPage -= 2;
            UpdatePages();
        }
    }

    void NextPage()
    {
        if (currentPage + 2 < allPhotoPaths.Count || (currentPage + 1 < allPhotoPaths.Count && allPhotoPaths.Count % 2 != 0))
        {
            currentPage += 2;
            UpdatePages();
        }
    }

    public void RefreshAlbum()
    {
        LoadAllPhotoPaths();
        currentPage = 0; // 如果需要，回到第一页
        UpdatePages();
    }
}
