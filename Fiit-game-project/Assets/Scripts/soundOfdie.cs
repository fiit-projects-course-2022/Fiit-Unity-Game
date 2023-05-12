using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundOfdie : MonoBehaviour
{
    [SerializeField] public AudioSource soundOfDieBonic;
    [SerializeField] public AudioSource soundOfDieSlug;
    [SerializeField] public AudioSource soundOfDieFireBall;
    [SerializeField] public AudioSource soundOfDiePlayer;

    private GameObject[] bonics = new GameObject[]{};
    private GameObject[] slugs = new GameObject[]{};
    private GameObject[] fireballs = new GameObject[]{};
    // Start is called before the first frame update
    void Start()
    {
        bonics = GameObject.FindGameObjectsWithTag("Bonic");
        slugs = GameObject.FindGameObjectsWithTag("slug");
        fireballs = GameObject.FindGameObjectsWithTag("fireBall");
        Debug.Log(fireballs);

    }

    // Update is called once per frame
    void Update()
    {
        DieFireBall();
        DieBonic();
        
        DieSlug();
        
    }

    public void DieBonic()
    {
        foreach (var bonic in bonics)
        {
            if (bonic != null)
            {
                if (bonic.GetComponent<Enemy>().currentHealth == 0)
                {
                    soundOfDieBonic.Play();
                }
            }
        }
    }
    
    public void DieFireBall()
    {
        foreach (var fireBall in fireballs)
        {
            if (fireBall != null)
            {
                if (fireBall.GetComponent<Enemy>().currentHealth == 0)
                {
                    soundOfDieFireBall.Play();
                }
            }
        }
    }
    
    public void DieSlug()
    {
        foreach (var slug in slugs)
        {
            if (slug != null)
            {
                if (slug.GetComponent<Enemy>().currentHealth == 0)
                {
                    soundOfDieSlug.Play();
                }
            }
        }
    }
}
