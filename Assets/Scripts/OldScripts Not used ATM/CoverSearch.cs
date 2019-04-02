using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSearch : MonoBehaviour {
    /*
    private SphereCollider[] Covers;

    public Transform PlayerTransform;

    public LayerMask ObstaclesLayerMask;

    

    // Use this for initialization
    void Start () {
        Covers = transform.GetComponentsInChildren<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("CheckCovertures");
    }



    private IEnumerator CheckCovertures()
    {
        foreach (var cover in Covers)
        {

           // RaycastHit hits = Physics.RaycastAll(cover.transform.position, PlayerTransform.position, out hit);
            Debug.DrawLine(cover.transform.position, PlayerTransform.position, Color.magenta, 0.1f, true);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(cover.transform.position, PlayerTransform.position, 100f,ObstaclesLayerMask);

            byte aux = 0;
            for (var i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.CompareTag("Obstacle"))
                {
                    aux++;
                    //cover.GetComponent<Renderer>().material.color = Color.green;
                }
                if (hits[i].transform.gameObject.CompareTag("Player"))
                {
                    aux++;
                   // cover.GetComponent<Renderer>().material.color = Color.red;
                }
                // solo ha tocado con el player
                if(aux<= 1)
                    cover.GetComponent<Renderer>().material.color = Color.red;
                // ha tocado con el jugador y una cobertura minimo
                else
                    cover.GetComponent<Renderer>().material.color = Color.green;
            }

            //if (Physics.Raycast(cover.transform.position, PlayerTransform.position, out hit))
            //{

            //    if (hit.collider.gameObject.CompareTag("Obstacle"))
            //    {
            //        cover.GetComponent<Renderer>().material.color = Color.green;
            //    }
            //    else // if(hit.collider.gameObject.CompareTag("Player"))
            //    {
            //        cover.GetComponent<Renderer>().material.color = Color.red;
            //    }
                
            //}
            yield return new WaitForSeconds(0.1f);

        }
    }

    */
}
