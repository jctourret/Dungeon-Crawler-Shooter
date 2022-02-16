using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    [SerializeField] BossHand bossHand;
    bool attacking = false;

    protected override void Start() {
        player = FindObjectOfType<PlayerController>();
        bossHand.SetDamage(damage);
        bossHand.gameObject.SetActive(false);
    }

    protected override void Update() {
        if (actualState == EnemyState.Dead)
            return;

        if (attacking)
            return;

        if (actualState == EnemyState.PreparingAttack) {
            timerAttack += Time.deltaTime;
            if (timerAttack >= attackCooldown) {
                timerAttack = 0f;
                AttackPlayer();
            }
        }
    }

    protected override void AttackPlayer() {
        bossHand.gameObject.SetActive(true);
        animator.Play("BossHandAttack");
        attacking = true;
    }

    public void EndAttack() {
        bossHand.gameObject.SetActive(false);
        attacking = false;
    }

}
