using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public GameObject Base;
    // Start is called before the first frame update
    void Start()
    {

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {

            Material m_Material = Base.GetComponent<Renderer>().material;
            r.material = m_Material;
           // r.material.color = Base.GetComponent<Color>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
