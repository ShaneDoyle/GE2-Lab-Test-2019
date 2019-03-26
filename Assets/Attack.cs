using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool AttackCooldown = false;

    public GameObject Fighter; //Fighter who shot the bullet.
    public GameObject Bullet;
    public GameObject target; // = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AttackCooldown == false)
        {
            AttackCooldown = true;
            StartCoroutine("Shoot");
        }

    }

    //Increase Tiberium coroutine. Increments and spawns fighers.
    IEnumerator Shoot()
    {
        //Wait 1 second.
        Bullet = GameObject.Instantiate<GameObject>(Bullet);
        Bullet.transform.position = transform.position; //Spawn at base.
        Bullet.name = "Bullet"; //Give name for niceness.#
        Bullet bullet = Bullet.GetComponent<Bullet>();
        bullet.target = target;



        //Wait 1 second.
        yield return new WaitForSeconds(0.2f);
    }
}
