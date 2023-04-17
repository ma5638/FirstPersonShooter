using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState
{
    enemyAIDemo myEnemy;
    private int nextWayPoint = 0;


    // When we call the constructor, we save
    // a reference to our enemy's AI
    public PatrolState(enemyAIDemo enemy)
    {
        myEnemy = enemy;
    }

    // Here goes all the functionality that we want
    // what the enemy does when he is in this
    // state.
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.green;

        myEnemy.navMeshAgent.destination = myEnemy.wayPoints[nextWayPoint].position;

        if (myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)

        {
            nextWayPoint = (nextWayPoint + 1) % myEnemy.wayPoints.Length;
        }
    }

    public void GoToAttackState() {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.attackState;
    }

    public void Impact() {

        GoToAttackState();
    
    }

    public void GoToAlertState() {

        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToPatrolState()
    {
        myEnemy.currentState = myEnemy.patrolState;
    }


    // In this state the player is already inside, so we will ignore it.
    public void OnTriggerEnter(Collider col) {    }

    // We will orient the enemy always looking at the
    // player while we attack him/her
    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            GoToAlertState();
            // // We always look at the player.
            // Vector3 lookDirection = col.transform.position -
            //                     myEnemy.transform.position;

            // // Rotating only on the Y axis
            // myEnemy.transform.rotation =
            //     Quaternion.FromToRotation(Vector3.forward,
            //                                 new Vector3(lookDirection.x, 0, lookDirection.z));
        }
    }

    // If they player is outside the enemy radius, the enemy changes to Idle State.
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            GoToPatrolState();
    }
}