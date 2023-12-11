using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS.BoxTheBox
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speedBullet;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private GameObject explosionPref;
        private GameObject explosion;
        // public GameObject Explosion => explosion;

        // Start is called before the first frame update
        void FixedUpdate()
        {
            rb.velocity = transform.right * speedBullet;
        }

        // Update is called once per frame
        // void Update()
        // {
        //     transform.Translate(Vector3.up * speedBullet * Time.deltaTime);
        // }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.CompareTag("Wall")){
                gameObject.SetActive(false);
                
            }

            if(other.gameObject.CompareTag("Enemy")){
                SpawnExsplosion();
                gameObject.SetActive(false);
                // explosion.Play();
            }
        }

        public void SpawnExsplosion(){
            explosion = Instantiate(explosionPref, transform.position, transform.rotation);
            Destroy(explosion, 0.5f);
        }
    }
}
