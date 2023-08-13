using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField]private AnimationClip attackAnimationClip;
    [SerializeField]private AnimationClip deathAnimationClip;

    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public override void Attack() {
        base.Attack();
        anim.SetTrigger("Attack");
        StartCoroutine(WaitAttackAnimation());
    }

    IEnumerator WaitAttackAnimation() {
        yield return new WaitForSeconds(attackAnimationClip.length);
        // decrease player health 
        agent.isStopped = false;
        Move();
    }


    public override void StopMovingAgent() {
        base.StopMovingAgent();
        anim.SetFloat("MoveSpeed",0);
    }

    protected override void PlayMoveAnimation() { anim.SetFloat("MoveSpeed",speed); }

    public override void Die() {
        base.Die();
        anim.SetTrigger("Dead");
        Destroy(gameObject,deathAnimationClip.length);
    }
}
