﻿using UnityEngine;
using System.Collections;


public class AlertState : IEnemyState
{
    enemyAIDemo myEnemy;
    float currentRotationTime = 0;
    float reactionDelay = 0;


    // When we call the constructor, we save
    // a reference to our enemy's AI
    public AlertState(enemyAIDemo enemy)
    {
        myEnemy = enemy;
    }

    // Here goes all the functionality that we want
    // what the enemy does when he is in this
    // state.
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.yellow;

        //We rotate the enemy in the time specified by rotationtime

        myEnemy.transform.rotation *= Quaternion.Euler (0f, Time.deltaTime * 360 * 1.0f / myEnemy.rotationTime, 0f);
        // myEnemy.transform.rotation *= Quaternion.Euler (0f Time * deltaTime * 360 * 1.0f / myEnemy.rotationTime, 0f);
        Debug.Log(reactionDelay);
        Debug.Log(myEnemy.reactionDelay);
        //If we have rotated
        if(currentRotationTime > myEnemy.rotationTime)
        {
            currentRotationTime = 0;
            GoToPatrolState();
        }
        else if (reactionDelay > myEnemy.reactionDelay)
        {
            // Debug.Log("A");
            //if we are still turning we cast a ray towards the enemy's position
            RaycastHit hit;
            if(Physics.Raycast(new Ray(new Vector3(myEnemy.transform.position.x, myEnemy.transform.position.y, myEnemy.transform.position.z), myEnemy.transform.forward * 100f), out hit))
            {
                // Debug.Log(hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "Player")
                {
                    
                    reactionDelay = 0.0f;
                    GoToAttackState();

                }
            }
            currentRotationTime += Time.deltaTime;
        }
        else 
        {
            reactionDelay += Time.deltaTime;
        }
    }

    //If the Player has hit us
    public void Impact() {
        GoToAttackState();
    
    }

    //We are in this state so we never call it
    public void GoToAlertState() {
    }

    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.attackState;
    }

    public void GoToPatrolState()
    {
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }


    public void OnTriggerEnter(Collider col) {}

    public void OnTriggerStay(Collider col) {}

    public void OnTriggerExit(Collider col) {}

}