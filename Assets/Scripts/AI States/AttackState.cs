using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState
{
    enemyAIDemo myEnemy;
    float actualTimeBetweenShots = 0;
    float timeWithoutLookingAtPlayer = 0.0f;

    // When we call the constructor, we save
    // a reference to our enemy's AI
    public AttackState(enemyAIDemo enemy)
    {
        myEnemy = enemy;
    }

    // Here goes all the functionality that we want
    // what the enemy does when he is in this
    // state.
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
        actualTimeBetweenShots += Time.deltaTime;
        timeWithoutLookingAtPlayer += Time.deltaTime;

        if (timeWithoutLookingAtPlayer > myEnemy.reactionDelay){
            timeWithoutLookingAtPlayer = 0.0f;
            GoToAlertState();
        }
    }

    public void Impact(){}

    // Since we are already in the Alert state, 
    // we will never call this function from
    // This state
    public void GoToAlertState() {
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToPatrolState() {}

    public void GoToAttackState() {}


    // In this state the player is already inside, so we will ignore it.
    public void OnTriggerEnter(Collider col) { }

    // We will orient the enemy always looking at the
    // player while we attack him/her
    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            timeWithoutLookingAtPlayer = 0.0f;
            //We always look at the player
            Vector3 lookDirection = col.transform.position - myEnemy.transform.position;

            //We rotate around the Y axis
            myEnemy.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookDirection.x, 0, lookDirection.z));

            //Turn to shoot
            if (actualTimeBetweenShots > myEnemy.timeBetweenShots)
            {
                actualTimeBetweenShots = 0;
                // col.gameObject.GetComponent<FirstPersonController>().Hit(myEnemy.damageForce); // TODOOOOOOOo
            }
        }
    }

    // If they player is outside the enemy radius, the enemy changes to Idle State.
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            {
                timeWithoutLookingAtPlayer = 0.0f;
                GoToAlertState();
            }
    }
}

