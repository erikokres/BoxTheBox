using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DS.BoxTheBox.Boss
{
    public class BossMovement : MonoBehaviour
    {

        private float moveSpeed;
        private bool moveRight;

        // [SerializeField] private Transform target;
        private NavMeshAgent agent;
        public float range; //radius of sphere

        public Transform centrePoint;
        public Transform[] waypoints;
        int waypointindex;
        int randomPointer;
        Vector3 target;

        //Randomize Boss Attack
        BossFireBullet bossFireBullet;
        BossFireSpiral bossFireSpiral;
        public float minRandomTimeInSecond = 10;
        public float maxRandomTimeInSecond = 10;
        private float timer;

        // Start is called before the first frame update
        void Start()
        {
            moveSpeed = 2f;
            moveRight = true;
            agent = GetComponent<NavMeshAgent>();
            // UpdateDestination();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            //Randomly boss attack
            
            bossFireBullet = GetComponent<BossFireBullet>();
            bossFireSpiral = GetComponent<BossFireSpiral>();
            StartCoroutine(OnOfRandomly());
            
        }

        // Update is called once per frame
        void Update()
        {
            if(agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
            }
            // if (transform.position.x > 7f) {
            //     moveRight = false;
            // }
            // else if (transform.position.x < -7f) {
            //     moveRight = true;
            // }

            // if (moveRight){
            //     transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            // }
            // else{
            //     transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            // }
        }

        void UpdateDestination()
        {
            // int randomPointer = Random.Range(0, waypoints.Length);
            target = waypoints[waypointindex].position;
            agent.SetDestination(target);
        }

        void IterateWaypointIndex()
        {
            waypointindex++;
            if (waypointindex == waypoints.Length)
            {
                waypointindex = 0;
            }
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
        }

        IEnumerator OnOfRandomly(){
            while (true)
            {
                bossFireBullet.enabled = true;
                yield return new WaitForSeconds(Random.Range(minRandomTimeInSecond, maxRandomTimeInSecond));
                bossFireBullet.enabled = false;
                bossFireSpiral.enabled = false;
                yield return new WaitForSeconds(Random.Range(minRandomTimeInSecond, maxRandomTimeInSecond));
                bossFireSpiral.enabled = true;
                yield return new WaitForSeconds(Random.Range(minRandomTimeInSecond, maxRandomTimeInSecond));
                bossFireBullet.enabled = false;
                bossFireSpiral.enabled = false;
            }
        }
    }
}

