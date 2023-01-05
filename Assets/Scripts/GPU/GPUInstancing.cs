using System.Collections.Generic;
using UnityEngine;

public class GPUInstancing : MonoBehaviour
{
    private List<GameObject> objects;
    void Start()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
            objects.Add(transform.GetChild(i).gameObject);

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
