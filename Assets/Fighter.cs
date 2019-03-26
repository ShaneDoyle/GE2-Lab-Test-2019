using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Public Variables
    public GameObject Base;
    public GameObject Target;
    public GameObject[] EnemyBases = new GameObject[4];
    public GameObject Bullet;
    public float tiberium = 7f;

    //Hold scripts that the fight uses.
    Seek seek;
    Arrive arrive;
    Attack attack;
    Boid boid;

    // Start is called before the first frame update
    void Start()
    {
        //Assign scripts of this fighter.
         seek = GetComponent<Seek>();
         arrive = GetComponent<Arrive>();
         attack = GetComponent<Attack>();
         boid = GetComponent<Boid>();

        //Get material from base.
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            Material m_Material = Base.GetComponent<Renderer>().material;
            r.material = m_Material;
        }

        //Pick a target.
        ChooseTarget();
    }

    //Update is called once per frame. Manages the different behaviours that the fighter can do.
    void Update()
    {
        //If no target, choose a new one.
        if(Target == null)
        {
            ChooseTarget();
        }

        //If fighter has target, arrive to it. Used to arrive to enemy bases.
        if (tiberium > 0)
        {
            arrive.targetPosition = Target.transform.position;
            arrive.enabled = true;
        }

        //Check distance between the fighter and the target. If close enough, change to attack mode.
        if (Vector3.Distance(transform.position, Target.transform.position) < 15 && tiberium > 0)
        {
            arrive.targetPosition = Target.transform.position;
            arrive.slowingDistance = 15f;
            arrive.enabled = false;
            boid.enabled = false;

            //If fighter has ammo and is in range, attack.
            if (tiberium > 0)
            {
                //Attack mode.
                attack.enabled = true;
                attack.target = Target;
                attack.Bullet = Bullet;
                attack.Fighter = this.gameObject;

                //Resets speed, fighter looks smoother returning.
                boid.velocity = new Vector3(0, 0, 0);
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

            //If at base, reload.
            if (Vector3.Distance(transform.position, Base.transform.position) < 1)
            {
                //Reduce "ammo" by 1.
                Base mybase = Base.GetComponent<Base>();
                if (mybase.tiberium >= 7 && tiberium == 0)
                {
                    Reload();
                }
            }

        }
    }

    //Reloads and when done, refreshes a new target.
    void Reload()
    {
        Base mybase = Base.GetComponent<Base>();
        mybase.tiberium -= 7;
        tiberium += 7;
        ChooseTarget();
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
