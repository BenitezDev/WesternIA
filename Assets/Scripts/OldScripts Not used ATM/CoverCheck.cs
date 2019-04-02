using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverCheck : MonoBehaviour
{

    /// <summary>
    /// Resharp
    /// </summary>

    private SphereCollider[] Covers;

    // public List<GameObject> Covered;

    public Transform SphereTransform;

    public float maxDistance = 25;
    

    // transform del enemigo
    private Transform tr;

    public Transform PlayerTransform;

    // Use this for initialization
    private void Start()
    {
        Covers = SphereTransform.GetComponentsInChildren<SphereCollider>();

    }

    // Update is called once per frame
    private void Update()
    {

       // StartCoroutine("CheckCovertures");
    }

    
    //private void createCovers()
    //{
    //    Covered = new List<GameObject>();
    //    foreach ( var c in Covers)
    //    {

    //    }
    //}

    private IEnumerator CheckCovertures()
    {
        foreach (var cover in Covers)
        {

            RaycastHit hit;
            Debug.DrawLine(cover.transform.position, PlayerTransform.position, Color.magenta, 0.1f, true);

            if (Physics.Raycast(cover.transform.position, PlayerTransform.position, out hit))
            {
               
                if ( hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    cover.GetComponent<Renderer>().material.color = Color.green;
                }
                else // if(hit.collider.gameObject.CompareTag("Player"))
                {
                    cover.GetComponent<Renderer>().material.color = Color.red;
                }
                //else
                //{
                //    cover.GetComponent<Renderer>().material.color = Color.red;
                //}

                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }



            yield return new WaitForSeconds(0.1f);

        }

    }

}
/*
 * por algun motivo este metodo no me va:
 
     if (cover.Raycast(ray, out hit, maxDistance) )
   {
   print(hit.collider.gameObject.name);
   if (hit.collider.gameObject.tag == "Player")
   print("choco con player");
   
   if(hit.collider.gameObject.tag == "Player")
   {
   cover.GetComponent<Renderer>().material.color = Color.red;
   //  hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
   }
   
   //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
   }
   else
   cover.GetComponent<Renderer>().material.color = Color.green;
*/
