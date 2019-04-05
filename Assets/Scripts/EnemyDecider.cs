using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDecider : MonoBehaviour
{

    public enum EnemyStates { MoveTo, Fire };
   

    /// <summary>
    /// Current state of this enemy
    /// </summary>
    EnemyStates enemyState;

    /// <summary>
    /// Whether this enemy is safe or not. ATM being in the player's line of sight
    /// </summary>
    [SerializeField]
    private bool imSafe;

    public EnemyCoverManager coverManager;
    public EnemyLocomotion   locomotion;
    public EnemyCosts costs;
    public EnemyHandleShoot handleShoot;

    private BallCover currentCover;

    void Start ()
    {
        coverManager = GetComponent<EnemyCoverManager>();
        locomotion   = GetComponent<EnemyLocomotion>();
        costs = GetComponent<EnemyCosts>();
        handleShoot = GetComponent<EnemyHandleShoot>(); 
        // The enemy start by moving to a save cover
         Invoke("MoveToNearestSaveCover", 1f);
	}

    private void MoverAAStart()
    {
        var rootNode = new Node(null, this.currentCover, this, 0);
        var destinationAstart = AStart(rootNode).getBallCover();
        if(destinationAstart != null)
            this.locomotion.MoveTo(destinationAstart);
    }
    // ATM the enemy only run away from the player
    // TODO: implement other behaviors
    void Update ()
    {

       
	}

    /// <summary>
    /// Move this enemy to the nearest safe cover
    /// </summary>
    private void MoveToNearestSaveCover()
    {
        this.currentCover = coverManager.SearchCloserSafeCover();

        locomotion.MoveTo( currentCover );

        // This make the enemy crouch if need it
        coverManager.StartCoroutine(coverManager.checkDistanceToCover(currentCover.coverType));
        InvokeRepeating("MoverAAStart",5,5);
    }

    private Node AStart(Node root)
    {
        
        if (Mathf.Approximately(this.locomotion.GetComponent<NavMeshAgent>().velocity.sqrMagnitude, 0)&& this.handleShoot.ATiro())
        {
            print("Disparar!");
            this.handleShoot.Shoot();
        }
        
        var abierta = new List<Node>();
        
        abierta.Add(root);

        while (abierta.Count > 0)
        {
            
            if (abierta[0].esMeta()) return abierta[0];

            var sucesores = abierta[0].Expand();

            foreach (var sucesor in sucesores)
            {
//                if ( abierta[0].GetPadre().GetPadre() != null 
//                     && abierta[0].GetPadre()!= null && sucesor != abierta[0].GetPadre().GetPadre())
//                {
                abierta.Add(sucesor);
                
          
            }
            abierta.RemoveAt(0);
        
            //abierta.Sort();
            abierta = OrdenarLista(abierta);
        }

        return null;

    }

    private List<Node> OrdenarLista(List<Node> abierta)
    {
        var auxAbierta = new List<Node>();

        while (abierta.Count > 0)
        {
            var auxG = abierta[0].getF();

            // Encontramos valor más alto de FStar
            for (var i = 0; i < abierta.Count; i++)
            {
                if (auxG < abierta[i].getF())
                    auxG = abierta[i].getF();
            }

            // Añadimos al principio el más alto
            for (var i = 0; i < abierta.Count; i++)
            {
                if (auxG == abierta[i].getF())
                {
                    auxAbierta.Insert(0, abierta[i]);
                    abierta.RemoveAt(i);
                }
            }
        }

        return auxAbierta;
    }
    
    public void EnemyIsInDanger()
    {
        imSafe = false;
    }
    public void EnemyIsSafe()
    {
        imSafe = true;
    }

}
