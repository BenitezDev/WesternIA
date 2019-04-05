using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleShoot : MonoBehaviour
{
    public float distanciaOptima;
    [SerializeField] private TextMesh text;


    private void Awake()
    {
        this.text = GetComponentInChildren<TextMesh>();
        this.text.gameObject.SetActive(false);
    }

    public void Shoot()
    {
        this.text.gameObject.SetActive(true);
        Invoke("RemoveText",1);
    }

    private void RemoveText()
    {
        this.text.gameObject.SetActive(false);
    }

    public bool ATiro()
    {
        // Solucion nefasta, pero solucion
        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 
            new Vector3(PCLocomotion.PlayerPosition.x,PCLocomotion.PlayerPosition.y + 1,PCLocomotion.PlayerPosition.z)- new Vector3(transform.position.x, 
                transform.position.y + 1, transform.position.z),Color.red, 4f);
        
        return      Physics.Raycast(
                        new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 
                        new Vector3(PCLocomotion.PlayerPosition.x,PCLocomotion.PlayerPosition.y+1,PCLocomotion.PlayerPosition.z)- new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 
                        out hit) && hit.transform.CompareTag("Player");
        
        
//            Physics.Raycast(
//                new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 
//                transform.forward, 
//                out hit) && hit.transform.CompareTag("Player");
    }
}