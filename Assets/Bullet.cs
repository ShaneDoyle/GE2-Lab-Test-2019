using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject target;
    public GameObject Fighter; //Fighter who shot the bullet.

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("KillMe", 10);
        //Get material from base.
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            Material m_Material = Fighter.GetComponent<Renderer>().material;
            r.material = m_Material;
        }
    }

    public void KillMe()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target.transform.position,0.06f);

        //When "hit" a base.
        if(Vector3.Distance(transform.position, target.transform.position) < 3f)
        {
            Base targetbase = target.GetComponent<Base>();
            targetbase.tiberium -= 0.5f;
            Destroy(this.gameObject);
        }
    }
}
