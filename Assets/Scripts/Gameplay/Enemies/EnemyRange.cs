using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{

    [SerializeField] EnemyProjectile projectile;
    [SerializeField] Transform projectileSpawn;
    protected override void Update() {
        Debug.DrawRay(projectileSpawn.position, (player.transform.position - projectileSpawn.position).normalized * 10, Color.red);
        base.Update();
    }
    protected override void AttackPlayer() {
        EnemyProjectile p = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
        p.SetProjectile((player.transform.position - projectileSpawn.position).normalized, damage);       
    }

}
