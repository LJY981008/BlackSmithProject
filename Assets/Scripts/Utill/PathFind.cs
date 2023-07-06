using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ã�� Ŭ����
/// </summary>
public class PathFind : MonoBehaviour
{
    const int MAX = 10;
    public GameObject line;
    public List<Transform> pathObj;
    List<Transform> path = new List<Transform>();
    Transform startPath;
    Transform endPath;
    float[,] graph = new float[MAX, MAX];
    float[] distance = new float[MAX];
    bool[] visit = new bool[MAX];
    int[] parent = new int[MAX];
    void SetGraph()
    {
        startPath = pathObj[0];
        endPath = pathObj[8];
        graph[0, 1] = Vector3.Distance(startPath.position, pathObj[1].position); 
        graph[1, 2] = Vector3.Distance(pathObj[1].position, pathObj[2].position); 
        graph[2, 3] = Vector3.Distance(pathObj[2].position, pathObj[3].position); 
        graph[3, 4] = Vector3.Distance(pathObj[3].position, pathObj[4].position); 
        graph[3, 5] = Vector3.Distance(pathObj[3].position, pathObj[5].position); 
        graph[5, 6] = Vector3.Distance(pathObj[4].position, pathObj[6].position); 
        graph[6, 7] = Vector3.Distance(pathObj[6].position, pathObj[7].position); 
        graph[7, 8] = Vector3.Distance(pathObj[7].position, endPath.position); 
    }
    /// <summary>
    /// ���ͽ�Ʈ�� �˰���
    /// </summary>
    void Dijikstra()
    {
        path.Add(startPath);
        distance[0] = 0;
        parent[0] = 0;
        while (true)
        {
            float candidate = Int32.MaxValue;
            int index = -1;
            for (int i = 0; i < MAX; i++)
            {
                // �̹� ����� ��� ��ŵ
                if (visit[i])
                    continue;
                // �ĺ����� ũ�ų� �߰ߵ��� ���� ��� ��ŵ
                if (distance[i] == Int32.MaxValue || distance[i] >= candidate)
                    continue;
                // ���� ������ ���
                candidate = distance[i];
                index = i;
            }
            // ���� ��尡 ���� ��
            if (index == -1)
                break;
            // ������ �ĺ� üũ
            visit[index] = true;
            // �ش� �ĺ��� ������ ��� Ȯ���ϰ� �ִܰŸ� ����
            for (int nextIndex = 0; nextIndex < MAX; nextIndex++)
            {
                // ������� ���� ���
                if (graph[index, nextIndex] == 0)
                    continue;
                // �̹� Ȯ���� ���
                if (visit[nextIndex])
                    continue;
                // ���� Ȯ���� ����� �ִܰŸ� ���.
                float nextDis = distance[index] + graph[index, nextIndex];
                // �� ª���Ÿ��� ����
                if (nextDis < distance[nextIndex])
                {
                    if(path.Count == index + 1)
                        path.RemoveAt(index);
                    path.Add(pathObj[index]);
                    distance[nextIndex] = nextDis;
                    parent[nextIndex] = index;
                }
            }
        }
        path.Add(endPath);
    }
    /// <summary>
    /// ���� �׷��ִ� �Լ�
    /// </summary>
    public void DrawLine()
    {
        Vector3[] linePos = new Vector3[path.Count];
        Instantiate<GameObject>(line, Vector3.zero, Quaternion.identity);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        for (int i = 0; i < path.Count; i++) {
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(i,path[i].position);
            
        }
    }
    private void Start()
    {
        Array.Fill(visit, false);
        Array.Fill(distance, Int32.MaxValue);
        SetGraph();
        Dijikstra();
        DrawLine();
        foreach (var o in path)
        {
            Debug.Log(o.name);
        }
    }
}
