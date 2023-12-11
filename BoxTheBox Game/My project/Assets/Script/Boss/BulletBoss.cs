using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.BoxTheBox.Boss
{

    public class BulletBoss : MonoBehaviour
    {
        private Vector2 moveDirection;
        private float bossBulletSpeed;

        [SerializeField] private GameObject explosionPref;
        private GameObject explosion;

        private void OnEnable() {
            Invoke("Destroy", 3f);
        }
        // Start is called before the first frame update
        void Start()
        {
            bossBulletSpeed = 5f;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(moveDirection * bossBulletSpeed * Time.deltaTime);
        }

        public void SetMoveDirection(Vector2 dir){
            moveDirection = dir;
        }

        private void Destroy(){
            gameObject.SetActive(false);
        }
        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.CompareTag("Wall")){
                gameObject.SetActive(false);
            }

            if(other.gameObject.CompareTag("Player")){
                SpawnExsplosion();
                gameObject.SetActive(false);
            }
        }

        public void SpawnExsplosion(){
            explosion = Instantiate(explosionPref, transform.position, transform.rotation);
            Destroy(explosion, 0.5f);
        }

        private void OnDisable(){
            CancelInvoke();
        }
    }
}
