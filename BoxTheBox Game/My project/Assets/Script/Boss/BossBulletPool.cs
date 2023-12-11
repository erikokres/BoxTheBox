using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.BoxTheBox.Boss
{

    public class BossBulletPool : MonoBehaviour
    {
        public static BossBulletPool bulletPoolInstance;
        [SerializeField] private GameObject pooledBullet;
        private bool notEnoughtBulletInPool = true;

        private List<GameObject> bulletsBoss;

        private void Awake() {
            bulletPoolInstance = this;

        }
        // Start is called before the first frame update
        void Start()
        {
            bulletsBoss = new List<GameObject>();
        }

        public GameObject GetBullet(){
            if(bulletsBoss.Count > 0){
                for (int i = 0; i < bulletsBoss.Count; i++)
                {
                    if(!bulletsBoss[i].activeInHierarchy){
                        return bulletsBoss[i];
                    }
                }
            }

            if(notEnoughtBulletInPool){
                GameObject bul = Instantiate(pooledBullet, this.transform);
                bul.SetActive(false);
                bulletsBoss.Add(bul);
                return bul;
            }

            return null;
        }


    }
}
