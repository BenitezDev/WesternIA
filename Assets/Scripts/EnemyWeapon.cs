using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    #region Weapon attributes
    /// <summary>
    /// Firing rate per second
    /// </summary>
    [SerializeField]
    private float firingRate = 1f;

    /// <summary>
    /// Maximun capacity of the magazine
    /// </summary>
    [SerializeField]
    private float magazineCapacity = 6f;

    /// <summary>
    /// Actual bullets in magazine
    /// </summary>
    private float bulletsInMagazine;

    /// <summary>
    /// How many seconds it takes to recharge the weapon
    /// </summary>
    [SerializeField]
    private float reloadingTime;

    /// <summary>
    /// Damage per bullet
    /// </summary>
    [SerializeField]
    private float damagePerBullet = 1;

    /// <summary>
    /// Weapon range
    /// </summary>
    [SerializeField]
    private float weaponRange;

    #endregion

    public enum EnemyType { Pistol = 0, DoublePistol = 1, Sniper = 2 };

    public EnemyType enemyType;

    /// <summary>
    /// Movement Speed depending of the weapon
    /// [0] Pistol
    /// [1] Double Pistol
    /// [2] Snipper
    /// </summary>
    static float[] movementSpeed = { 5f , 3.5f, 2f };

    // TODO: Make that only by changing the EnemyType,  change all the corresponding attributes


    EnemyLocomotion locomotion;


    void Start ()
    {
        locomotion = GetComponentInParent<EnemyLocomotion>();

        PickWeapon(enemyType);

	}


	public void PickWeapon(EnemyType enemyType)
    {
        locomotion.agent.speed = movementSpeed[(int)enemyType];
    }
}
