using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    //Variables
    public float tiberium = 0;
    public int tiberiumCap = 10;
    public TextMeshPro text;
    public GameObject fighterPrefab;


    //Private varaibles.
    private GameObject Fighter;
    private Renderer rend;
    private Color colorStart;

    private bool addTiberium = true;

    //Start is called before the first frame update.
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
            colorStart = r.material.color;
        }
    }

    //Update is called once per frame.
    void Update()
    {
        //Display tiberium amount.
        text.text = "" + tiberium;

        //Increase tiberium every second.
        if (addTiberium == true)
        {
            addTiberium = false;
            StartCoroutine("IncreaseTiberium");
        }
    }


    //Spawn fighter function.
    void SpawnFighter()
    {
        //Spawn fighter at base.
        Fighter = GameObject.Instantiate<GameObject>(fighterPrefab);
        Fighter.transform.position = transform.position;
        Fighter.name = "Fighter"; //Give name for niceness.

        //Assign the base it spawned to as its home base.
        Fighter script = Fighter.GetComponent<Fighter>();
        script.Base = this.gameObject;
    }

    //Increase Tiberium coroutine. Also handles the spawning of new fighters.
    IEnumerator IncreaseTiberium()
    {
        //Wait 1 second to increase.
        yield return new WaitForSeconds(1);
        if (tiberium >= tiberiumCap - 1)
        {
            tiberium -= 9;
            SpawnFighter();
        }
        else
        {
            tiberium++;
        }
        addTiberium = true;
    }
}


