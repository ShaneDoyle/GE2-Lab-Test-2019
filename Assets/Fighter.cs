using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public GameObject Base;
    public GameObject Target;
    public GameObject[] EnemyBases = new GameObject[4];
    public GameObject Bullet;

    public float tiberium = 7f;

    //Seek seek;

    // Start is called before the first frame update
    void Start()
    {
        //Get scripts of this fighter.
        //Seek seek = GetComponent<Seek>();


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
        Seek seek = GetComponent<Seek>();
        Arrive arrive = GetComponent<Arrive>();
        Attack attack = GetComponent<Attack>();
        Boid boid = GetComponent<Boid>();

        //If fighter has target, arrive to it. Used to arrive to enemy bases.
        if (Target != null)
        {
            arrive.targetPosition = Target.transform.position;
            //arrive.slowingDistance = 30f;
            arrive.enabled = true;
        }

        //Check distance between the fighter and the target. If close enough, change to attack mode.
        if (Vector3.Distance(transform.position, Target.transform.position) < 15 && tiberium > 0)
        {
            //Arrive.enabled = false;
            arrive.targetPosition = Target.transform.position;
            arrive.enabled = false;
            boid.enabled = false;

            if (tiberium > 0)
            {
                attack.enabled = true;
                attack.target = Target;
                attack.Fighter = this.gameObject;
            }
            else
            {
                attack.enabled = false;
            }
        }

        //When out of ammo, return to base.
        if (tiberium == 0)
        { 

            arrive.targetPosition = Base.transform.position;
            arrive.enabled = true;
            boid.enabled = true;
            attack.enabled = false;

            if(Vector3.Distance(transform.position, Base.transform.position) < 1)
            {

            }

        }




        // boid.force = new Vector3(0, 0, 0);
        // boid



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
