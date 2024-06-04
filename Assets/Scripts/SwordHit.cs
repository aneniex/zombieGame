using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public float damageToEnemy = 20f;
    private bool hasHitEnemyOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!CustomCharacterController.Instance.isAttacking) return;
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (hasHitEnemyOnce) return;
            StartCoroutine(ToggleHasHitEnemyOnce());
            var zombie = other.gameObject.GetComponent<ZombieAI>();
            //subtract zombie health

            if (SFXManager.Instance)
            {
                SFXManager.Instance.PlaySwordHitSound();
                SFXManager.Instance.OnVibrateEvent();
            }

            zombie.totalHealth -= damageToEnemy;
            zombie.InstantiateZombieHitEffect();
            if (zombie.xpDamage != null)
            {
                var damageNumber = zombie.xpDamage.Spawn(transform.position, damageToEnemy);
            }

            if (zombie.totalHealth <= 0)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<Animator>().Play("Death");

                StartCoroutine(DelayInZombieDisappear(other.gameObject));

                //Subtract total enemies in level number
                Level.Instance.numberOfEnemiesInThisLevel--;
                Level.Instance.enemiesKilled++;

                if (EnemySpawner.Instance != null)
                    UIManager.Instance.enemyCounterText.text = $"{Level.Instance.enemiesKilled} / {EnemySpawner.Instance.totalEnemiesCount}";

                if (Level.Instance.numberOfEnemiesInThisLevel <= 0)
                {
                    Level.Instance.OnLevelCompletedEvent();
                }

                //Play Zombie Death Sound
                if (SFXManager.Instance)
                {
                    SFXManager.Instance.PlayZombieDeathSound();
                }
            }
        }
    }

    private IEnumerator ToggleHasHitEnemyOnce()
    {
        hasHitEnemyOnce = true;
        yield return new WaitForSeconds(0.10f);
        hasHitEnemyOnce = false;
    }

    private IEnumerator DelayInZombieDisappear(GameObject zombie)
    {
        yield return new WaitForSeconds(2f);
        var zombieAI = zombie.GetComponent<ZombieAI>();
        zombieAI.zombieDisappearEffect.SetActive(true);
        zombieAI.zombieDisappearEffect.transform.SetParent(null);
        Destroy(zombieAI.zombieDisappearEffect, 2.5f);
        zombie.SetActive(false);

        //play explosion flash sound
        if (SFXManager.Instance)
            SFXManager.Instance.PlayExplosionFlashSound();
    }

}
