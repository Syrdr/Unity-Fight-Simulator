using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public float walkspeed;
    [SerializeField] private string walkAnimationTrigger;
    Rigidbody rigid;
    Animator PlayerAnim;
    //[SerializeField] private bool enemyInRange = false;
    private Vector3 enemyDirection;
    [SerializeField] private string attackAnim;
    [SerializeField] private float attackMoveSpeed;
    [SerializeField] private string enemyGoneAnim;
    public bool isDead = false;
    [SerializeField] private string deathAnim;
    [SerializeField] private string retreatAnim;
    [SerializeField] private string doneWalkingAnim;
    //[SerializeField] private string walkAnimName;
    private EnemyDetection enemyDetection;
    public float attackRange;
    private GameObject detectedEnemy;
    [SerializeField] private bool shouldWalk = false;
    private FollowPosition followPosition;
    private Vector3 direction;
    [SerializeField] private float deviationFromPos;
    [SerializeField] private float engageRange;
    private MoveCharacter enemyMoveCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
        enemyDetection = GetComponent<EnemyDetection>();
        followPosition = GetComponent<FollowPosition>();
        Debug.Log(followPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (shouldWalk)
            {
                Walk();
                if (enemyDetection.enemyClose)
                {
                    enemyMoveCharacter = enemyDetection.closestEnemy.GetComponent<MoveCharacter>();
                    if (!enemyMoveCharacter.isDead)
                    {
                        shouldWalk = false;
                    }
                }

            }
            else
            {
                chaseEnemy();
            }
            if (followPosition != null)
            {
                if (Vector3.Distance(transform.position, followPosition.spotLoc) > deviationFromPos && !enemyDetection.enemyClose)
                {
                    shouldWalk = true;
                }
                else { shouldWalk = false; }
            }
        }// else { Die(); }
    }

    private void chaseEnemy()
    {
        if(enemyDetection != null && enemyDetection.enemyClose)
        {
            detectedEnemy = enemyDetection.closestEnemy;
            Vector3.MoveTowards(transform.position, detectedEnemy.transform.position, attackMoveSpeed * Time.deltaTime);
            PlayerAnim.SetTrigger(attackAnim);
            PlayerAnim.SetTrigger(enemyGoneAnim);
        }
    }

    void Walk()
    {
        if (followPosition != null)
        {
            if (Vector3.Distance(transform.position, followPosition.spotLoc) > deviationFromPos)
            {
                direction = (followPosition.spotLoc - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                PlayerAnim.SetTrigger(walkAnimationTrigger);
                //Vector3.MoveTowards(transform.position, direction * walkspeed * Time.deltaTime, 9999);
                //rigid.AddForce(direction * Time.deltaTime * walkspeed, ForceMode.Force);
                transform.Translate(direction * walkspeed * Time.deltaTime, Space.World);
            }
            else if (transform.rotation != followPosition.spotRot)
            {
                transform.rotation = followPosition.spotRot;
                PlayerAnim.SetTrigger(enemyGoneAnim);
            }
        }
    }

    public void AttackEnemy()
    {
        if (enemyDetection.enemyClose)
        {
            enemyMoveCharacter = detectedEnemy.GetComponent<MoveCharacter>();
            if (!enemyMoveCharacter.isDead)
            {
                while (Vector3.Distance(transform.position, detectedEnemy.transform.position) > attackRange)
                {
                    Vector3.RotateTowards(transform.position, detectedEnemy.transform.position, 360, 500);
                    Vector3.MoveTowards(transform.position, detectedEnemy.transform.position, attackMoveSpeed * Time.deltaTime);
                }
                PlayerAnim.SetTrigger(attackAnim);
                //transform.Translate(enemyDirection * Time.deltaTime * attackMoveSpeed);
            }
        }
    }

    public void Die()
    {
        isDead = true;
        PlayerAnim.SetTrigger(deathAnim);
    }

    void Retreat()
    {

    }

    public void doneWalking()
    {
        PlayerAnim.SetTrigger(doneWalkingAnim);
    }
}