using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum State
{
    IDLE,
    PATROL,
    CHASE,
    SEARCH,
    ATTACK
}

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] LayerMask lm;
    public Level currentLevel;
    NavMeshAgent agent;
    Transform player;
    Animator anim;

    public float visionRange = 50;
    public float visionAngle = 30f;
    public bool fullAgressive = false;
    public bool noPatrol = false;

    public float chaseTimer = 0;
    public bool isMoving;

    bool lostContact;
    public Vector3 lastKnownPos;
    [SerializeField] State currentState = State.IDLE;
    float speed;
    int looksLeft = 0;

    float timer;
    float timer2;
    bool flag;
    Vector3 pos;
    Quaternion rot;

    EnemyStats stats;
    EnemySounds sounds;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        stats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rot = transform.rotation;
        sounds = GetComponent<EnemySounds>();

        if (!fullAgressive)
            InvokeRepeating("CheckForTarget", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.magnitude > 0f)
        {
            isMoving = true;
        }
        agent.speed = speed * (stats.stunTimer > 0 ? 0.45f : 1) * (currentState == State.CHASE ? 1.75f : 1f);
        if (fullAgressive)
            agent.SetDestination(player.position);
        else
            switch (currentState)
            {
                case State.IDLE:
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                        if (Random.value < 0.5)
                            ChangeState(State.IDLE);
                        else
                            ChangeState(State.PATROL);
                    break;
                case State.SEARCH:

                    if (agent.remainingDistance <= agent.stoppingDistance && looksLeft > 0 && !flag)
                    {
                        timer = Random.Range(5, 10);
                        timer2 = Random.Range(2, 4);
                        pos = GetRandomLocationFromPoint(lastKnownPos, 20);
                        flag = true;
                        looksLeft--;
                    }
                    if (timer <= 0)
                    {
                        flag = false;
                        agent.SetDestination(pos);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * agent.angularSpeed);
                        timer -= Time.deltaTime;
                        timer2 -= Time.deltaTime;
                        if (timer2 <= 0)
                        {
                            timer2 = Random.Range(2, 4);
                            rot = GetNextLookingRotation();
                        }
                    }

                    if (looksLeft == 0)
                    {
                        if (Random.value < 0.5)
                            ChangeState(State.IDLE);
                        else
                            ChangeState(State.PATROL);
                    }
                    break;
                case State.CHASE:
                    agent.SetDestination(player.position);
                    if (Vector3.Distance(transform.position, player.position) <= 1)
                        ChangeState(State.ATTACK);
                    break;
                case State.PATROL:
                    agent.SetDestination(pos);
                    if(agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (timer <= 0)
                        {
                            pos = currentLevel.waypoints[Random.Range(0, currentLevel.waypoints.Length)].position;
                            if (Random.value < 0.5)
                                timer = Random.Range(2, 3);
                        }
                        timer -= Time.deltaTime;
                    }
                    break;
                case State.ATTACK:
                    agent.SetDestination(transform.position);
                    anim.SetBool("attack", true);
                    if (Vector3.Distance(transform.position, player.position) > 2)
                    {
                        Debug.Log("too far");
                        ChangeState(State.CHASE);
                    }
                    break;
            }
        CheckAnimationState();
    }

    public void CheckAnimationState()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            anim.speed = agent.speed;
            anim.SetBool("moving", true);
        }
        else
        {
            anim.speed = 1;
            anim.SetBool("moving", false);
        }
    }
    public State GetState()
    {
        return currentState;
    }
    public void ChangeState(State nextState)
    {
        anim.SetBool("attack", false);
        timer = 0;
        timer2 = 0;
        if (nextState == State.SEARCH)
        {
            if(currentState == State.SEARCH) return;
            looksLeft = Random.Range(4, 8);
            pos = lastKnownPos;
        }
        else if (nextState == State.IDLE)
        {
            timer = Random.Range(10, 300);
        }
        else if (nextState == State.PATROL)
        {
            if (noPatrol)
                return;
            pos = currentLevel.waypoints[Random.Range(0, currentLevel.waypoints.Length)].position;
        }
        else if (nextState == State.CHASE)
        {
            if(currentState != State.ATTACK)
                sounds.AlarmSound();
        }
        currentState = nextState;
    }

    void CheckForTarget()
    {
        if (currentState == State.ATTACK)
            return;
        Vector3 playerDir = (player.position - transform.position).normalized;
        if (Vector3.Angle(playerDir, transform.forward) < visionAngle)
        {
            Debug.DrawRay(transform.position, playerDir * visionRange);
            if (Physics.Raycast(transform.position, playerDir, out RaycastHit hit, visionRange, lm))
            {
                if (hit.transform.tag == "Player")
                {
                    if(currentState != State.CHASE)
                        ChangeState(State.CHASE);
                    chaseTimer = 6;
                    lastKnownPos = player.position;
                }
            }
            else
            {
                if (chaseTimer > 0) chaseTimer--;
                else
                {
                    if (currentState == State.CHASE)
                    {
                        ChangeState(State.SEARCH);
                    }
                }
            }
        }
        else
        {
            if (chaseTimer > 0) chaseTimer--;
            else
            {
                if (currentState == State.CHASE)
                {
                    ChangeState(State.SEARCH);
                }
            }
        }
    }
    
    public Quaternion GetNextLookingRotation()
    {
        Vector2 newRot = Random.insideUnitCircle;
        return Quaternion.LookRotation(new Vector3(newRot.x, 0, newRot.y));
    }

    public Vector3 GetRandomLocationFromPoint(Vector3 point, float radius, int tries = 3)
    {
        Vector3 newPoint = Vector3.zero;
        for (int i = 0; i < tries; i++)
        {
            newPoint = point + getRandomXZPoint() * radius;
            if (Random.value < 0.25 && Vector3.Distance(transform.position, player.position) < radius)
                newPoint = player.position + getRandomXZPoint() * radius;
            if (NavMesh.SamplePosition(newPoint, out NavMeshHit hit, 100, 1))
            {
                newPoint = hit.position;
                break;
            }
        }
        return newPoint;
    }

    Vector3 getRandomXZPoint()
    {
        Vector2 newPoint = Random.insideUnitCircle;
        return new Vector3(newPoint.x, 0, newPoint.y);
    }
}

