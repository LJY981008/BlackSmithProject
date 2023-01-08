using System.Collections.Generic;
using UnityEngine;

public class GPUInstancing : MonoBehaviour
{
    private List<GameObject> objects;
    private Transform tmpTr;

    private void Awake()
    {
        objects = new List<GameObject>();
        tmpTr = Utill.FindTransform(transform, "Vilige");
        for (int i = 0; i < tmpTr.childCount; i++)
        {
            objects.Add(tmpTr.GetChild(i).gameObject);
        }
        tmpTr = Utill.FindTransform(transform, "Terrains");
        for (int i = 0; i < tmpTr.childCount; i++)
        {
            objects.Add(tmpTr.GetChild(i).gameObject);
        }
    }
    void Start()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        foreach (GameObject obj in objects)
        {
            renderer = obj.GetComponent<MeshRenderer>();
            float r = 1f;
            float g = 1f;
            float b = 1f;
            props.SetColor("_Color", new Color(r, g, b));
            renderer.SetPropertyBlock(props);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
