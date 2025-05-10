using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PhotoAlbum : MonoBehaviour
{
    public Button btnPrev;
    public Button btnNext;

    public GameObject photoSlotPrefab;  // ���� PhotoSlot �� Prefab
    public Transform leftPageTransform; // ��ҳ��ʾλ��
    public Transform rightPageTransform; // ��ҳ��ʾλ��

    private List<string> allPhotoPaths = new List<string>();
    private int currentPage = 0; // ÿ�α�ʾ��ҳ������0, 2, 4...��

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
        Debug.Log("���ص�������ͼƬ·��:");
        foreach (var path in allPhotoPaths)
        {
            Debug.Log(path);  // ��ӡͼƬ·��
        }
    }

    void UpdatePages()
    {
        // ɾ��֮ǰ��ͼƬ
        ClearPages();

        // ��������ʾ�µ���ҳ����ҳ
        GameObject leftSlotObj = Instantiate(photoSlotPrefab, leftPageTransform);
        GameObject rightSlotObj = Instantiate(photoSlotPrefab, rightPageTransform);

        // ȷ����ʵ�����Ķ�����ȷ��������ҳ����ҳ��λ����
        leftSlotObj.transform.localPosition = Vector3.zero;  // ������Ҫ����ƫ��
        rightSlotObj.transform.localPosition = Vector3.zero;  // ������Ҫ����ƫ��

        // ��ȡ PhotoSlot ���
        PhotoSlot leftSlot = leftSlotObj.GetComponent<PhotoSlot>();
        PhotoSlot rightSlot = rightSlotObj.GetComponent<PhotoSlot>();

        // ���ز���ʾͼƬ
        LoadAndDisplaySlot(currentPage, leftSlot);
        LoadAndDisplaySlot(currentPage + 1, rightSlot);
    }

    void LoadAndDisplaySlot(int index, PhotoSlot slot)
    {
        if (index >= 0 && index < allPhotoPaths.Count)
        {
            string path = allPhotoPaths[index];
            if (File.Exists(path)) // ����ļ��Ƿ����
            {
                Debug.Log("�ļ�����: " + path);  // ��ӡ�ļ�·��
                byte[] data = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(data);
                slot.SetTexture(tex);
            }
            else
            {
                Debug.LogWarning("�ļ�������: " + path);  // ����ļ������ڣ���ӡ����
            }
        }
        else
        {
            slot.Clear(); // ��ʾ�հ�ҳ
        }
    }

    void ClearPages()
    {
        // ɾ����ǰ����ҳ����ҳ
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
        currentPage = 0; // �����Ҫ���ص���һҳ
        UpdatePages();
    }
}
