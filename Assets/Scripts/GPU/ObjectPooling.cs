using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Queue<Transform> itemQueue;
    private Dictionary<int, Transform> itemSpawn;
    private Transform thisItem;
    private int maxCount = 3;
    private int spawnCount = 0;
    private Vector3 defaultSpawnPos = new Vector3(-5.5f, 0f, 100.5f);
    private void Awake()
    {
        itemSpawn = new Dictionary<int, Transform>();
        itemQueue = new Queue<Transform>();
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            itemQueue.Enqueue(transform.GetChild(i));
        }
    }
    private void Start()
    {
        StartCoroutine(SetSpawn());
    }
    /// <summary>
    /// 아이템 생성
    /// </summary>
    public void CreateItem()
    {
        thisItem = itemQueue.Dequeue();
        thisItem.name = spawnCount.ToString();
        Vector3 spawnPos = Utill.RandomPos(defaultSpawnPos, 13.5f, 12.5f);
        spawnPos.y = -5f;
        thisItem.position = spawnPos;
        thisItem.gameObject.SetActive(true);
        itemSpawn.Add(spawnCount, thisItem);
        spawnCount++;
    }
    /// <summary>
    /// 아이템 반환
    /// </summary>
    /// <param name="_id">반환할 아이템의 id</param>
    public void ReturnItem(int _id)
    {
        itemSpawn[_id].gameObject.SetActive(false);
        itemQueue.Enqueue(itemSpawn[_id]);
        itemSpawn.Remove(_id);
    }
    /// <summary>
    /// 일정 시간마다 아이템 스폰
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetSpawn()
    {
        while (true)
        {
            if(itemSpawn.Count < maxCount)
            {
                CreateItem();
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
