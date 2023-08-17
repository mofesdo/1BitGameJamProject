using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Weapon : Abilities
    {
        [SerializeField] protected List<WeaponTypes> weaponTypes;
        [SerializeField] protected Transform hand;
        [SerializeField] protected Transform handRotation;
        public List<GameObject> currentPool = new List<GameObject>();
        private GameObject projectileParentFolder;
        public GameObject currentProjectile;

        protected override void Initialization()
        {
            base.Initialization();
            foreach(WeaponTypes weapon in weaponTypes)
            {
                GameObject newPool = new GameObject();
                projectileParentFolder = newPool;
                objectPooler.CreatePool(weapon, currentPool, projectileParentFolder);
            }
        }
        protected virtual void Update()
        {
            if (input.WeaponFired())
            {
                FireWeapon();
            }
        }
        protected virtual void FireWeapon()
        {
            currentProjectile = objectPooler.GetObject(currentPool);
            if (currentProjectile != null)
            {
                Invoke("PlaceProjectile", .1f);
            }
        }
        protected virtual void PlaceProjectile()
        {
            currentProjectile.transform.position = hand.position;
            currentProjectile.transform.rotation = hand.rotation;
            currentProjectile.SetActive(true);
        }
    }
}