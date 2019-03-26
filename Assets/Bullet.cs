using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public GameObject Fighter; //Fighter who shot the bullet.

    //Start is called before the first frame update.
    void Start()
    {
        //Get material from base.
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            Material m_Material = Fighter.GetComponent<Renderer>().material;
            r.material = m_Material;
        }
    }

    //Update is called once per frame.
    void Update()
    {
        //Move slowly to base. Could use seek behaviour here but lerp was handier in this small script.
        transform.position = Vector3.Lerp(transform.position, target.transform.position,0.06f);

        //When "hit" a base.
        if(Vector3.Distance(transform.position, target.transform.position) < 3f)
        {
            //Apply damage to the target and then destroy self.
            Base targetbase = target.GetComponent<Base>();
            targetbase.tiberium -= 0.5f;
            Destroy(this.gameObject);
        }
    }
}
