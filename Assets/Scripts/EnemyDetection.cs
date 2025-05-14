using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyDetection : MonoBehaviour
{
    public string[] enemyTags;
    public GameObject closestEnemy;
    private MoveCharacter moveCharacter;
    public bool enemyClose = false;
    GameObject closest = null;
    private MoveCharacter enemyInfo;

    // Declare the enemies list at the class level
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        string ownTeam = gameObject.tag;

        foreach (string tag in enemyTags)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
    }

    /*public void FindClosestEnemy(float detectionRange)
    {
        float minDistance = detectionRange;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }
        if (closest != null)
        {
            closestEnemy = closest;
            enemyClose = true;
        }
        else
        {
            enemyClose = false;
        }
        //return closest;
    }*/

    void Update()
    {
        FindClosestEnemy(moveCharacter.attackRange);
    }

    public void FindClosestEnemy(float detectionRange)
    {
        enemies.Clear(); // Clear old references
        foreach (string tag in enemyTags)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        float minDistance = detectionRange;
        closest = null; // Reset closest enemy

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // Avoid null references
            enemyInfo = enemy.GetComponent<MoveCharacter>();

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && !enemyInfo.isDead)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        if (closest != null)
        {
            closestEnemy = closest;
            enemyClose = true;
        }
        else
        {
            closestEnemy = null; // Explicitly reset if no enemies are found
            enemyClose = false;
        }
    }

}

