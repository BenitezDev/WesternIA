using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBalls : MonoBehaviour {
    
    /*
        Las acciones posibles son:  atacar si estoy cubierto
                                    buscar cobertura si veo al enemigo
                                    moverse por ahi
     */

    public GameObject BolasPrefabCoberturaMedia;
    public GameObject BolasPrefabCoberturaCompleta;

    public List<GameObject> CoberturasCompletas;
    public List<GameObject> CoberturasMedias;

    public Transform Covers;

    void Awake ()
    {
        // Añade las esferas a las coberturas completas
        if (CoberturasCompletas.Count != 0)
            foreach (var cobertura in CoberturasCompletas)
            {
                Instantiate(BolasPrefabCoberturaCompleta, cobertura.transform.position, cobertura.transform.rotation, Covers);

            }

        // Añade las esferas a las coberturas medias
        if (CoberturasMedias.Count != 0)
            foreach (var cobertura in CoberturasMedias)
                Instantiate(BolasPrefabCoberturaMedia, cobertura.transform.position, cobertura.transform.rotation,Covers);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
