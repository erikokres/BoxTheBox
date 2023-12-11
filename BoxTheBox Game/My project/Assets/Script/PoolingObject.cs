using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.BoxTheBox
{
    
public class PoolingObject : MonoBehaviour
{
    public static PoolingObject instance;

    private List<GameObject> pooledObject = new List<GameObject>();

    private int amountToPool = 20;

    [SerializeField] private GameObject bulletPrefabs;

    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefabs, this.transform);
            obj.SetActive(false);
            pooledObject.Add(obj);
        }
    }

    public GameObject GetPooledObject(){
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if(!pooledObject[i].activeInHierarchy){
                return pooledObject[i];
            }
        }
        return null;
    }
}
}
