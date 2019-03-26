using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;
    public GameObject fighterPrefab;


    //Private varaibles.
    private GameObject Fighter;

    private Renderer rend;
    private Color colorStart;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
            colorStart = r.material.color;
            Debug.Log("Renderer complete");
        }


    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + tiberium;

        if(tiberium == 10)
        {

            StartCoroutine("SpawnFighter");
        }

        StartCoroutine("IncreaseTiberium");
    }


    IEnumerator SpawnFighter()
    {
        //Wait 1 second.
        yield return new WaitForSeconds(1);
        Fighter = GameObject.Instantiate<GameObject>(fighterPrefab);
        Fighter.transform.position = transform.position; //Spawn at base.

        Fighter script = Fighter.GetComponent<Fighter>();
        script.Base = this.gameObject;
    }
}


