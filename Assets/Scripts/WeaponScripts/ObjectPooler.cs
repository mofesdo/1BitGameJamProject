using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MetroidvaniaTools
{
    public class ObjectPooler : MonoBehaviour
    {
        private static ObjectPooler instance;
        public static ObjectPooler Instance
        {
            get
            {
                if(instance == null)
                {
                    GameObject obj = new GameObject("ObjectPooler");
                    obj.AddComponent<ObjectPooler>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private GameObject currentItem;

         public void CreatePool(WeaponTypes weapon, List<GameObject> currentPool, GameObject projectileParentFolder)
        {
            for(int i = 0; i < weapon.amountToPool;  i++)
            {
                currentItem = Instantiate(weapon.projectile);
                currentItem.SetActive(false);
                currentPool.Add(currentItem);
                currentItem.transform.SetParent(projectileParentFolder.transform);
            }
            projectileParentFolder.name = weapon.name;
        }
        public virtual GameObject GetObject(List<GameObject> currentPool)
        {
            for(int i = 0; i < currentPool.Count; i++)
            {
                if (!currentPool[i].activeInHierarchy)
                {
                    return currentPool[i];
                }
            }
            return null;
        }
    }
}