using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public GameObject Base;
    public GameObject Target;
    public GameObject[] EnemyBases = new GameObject[4];



    // Start is called before the first frame update
    void Start()
    {
        //Get material from base.
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            Material m_Material = Base.GetComponent<Renderer>().material;
            r.material = m_Material;
        }


        //Pick a target.
        ChooseTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            Seek seek = GetComponent<Seek>();
            seek.enabled = true;
            seek.target = Target.transform.position;
        }
    }

    void ChooseTarget()
    {
        //Clear Target.
        Target = null;
        EnemyBases = GameObject.FindGameObjectsWithTag("base");

        //Ensure fighter does not choose own base.
        while (Target == null)
        {
            int temp = Random.Range(0, EnemyBases.Length);
            Target = EnemyBases[temp];
            if (Target == Base)
            {
                Target = null;
            }
        }

    }
}
