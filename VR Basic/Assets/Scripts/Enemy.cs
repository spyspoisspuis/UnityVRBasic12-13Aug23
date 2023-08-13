using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,HealthSystem
{
    [SerializeField] protected float maxHealth;
    protected float currentHealth; 
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;
    protected GameObject target;
    protected NavMeshAgent agent;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = speed; 
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move() {
        if (target != null) {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget < attackRange) {
                Attack();
            } else {
                agent.SetDestination(target.transform.position);
                PlayMoveAnimation();
            }
        }
    }

    public virtual void Attack() {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed);
        StopMovingAgent();
    }

    public virtual void StopMovingAgent() {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    protected virtual void PlayMoveAnimation() {

    }

    public virtual void TakeDamage(float amount) {
        currentHealth -= amount;
        if (currentHealth < 0) {
            Die();
        }   
    }

    public virtual void Die () {
        StopMovingAgent();
    }
}
