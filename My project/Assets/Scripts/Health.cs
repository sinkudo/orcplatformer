using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    private Animator animator;
    [SerializeField] private LayerMask ground;
    
    public float curHealth { get; private set; }
    void Start()
    {
        animator = GetComponent<Animator>();
        if(!PlayerPrefs.HasKey("hp"))
            curHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        curHealth = Mathf.Clamp(curHealth - _damage, 0, startingHealth);
        if (curHealth <= 0)
        {
            animator.SetBool("Dead", true);
            if (GetComponent<Player>() != null)
            {
                GetComponent<Player>().enabled = false;
            }
            if (GetComponent<Enemy>() != null)
            {
                Enemy enemy = GetComponent<Enemy>();
                DropCoins drop = GetComponent<DropCoins>();
                drop.spawnCoins();
                enemy.isDead = true;
                RaycastHit2D[] rays = Physics2D.RaycastAll(enemy.groundPoint.position, new Vector2(0, -1f), ground);
                tileDestroy destr = GameObject.Find("Grid").GetComponentInChildren<tileDestroy>();
                enemy.enabled = false;
                destr.DestroyTile(rays[0].transform.position, transform.localScale.x);
                enemy.stopMovement();
                enemy.enabled = false;
                StartCoroutine(DeathAnim(enemy));
            }
        }
    }
    private IEnumerator DeathAnim(Enemy enemy)
    {
        yield return new WaitForSeconds(0.5f);
        enemy.isDead = true;
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, new Vector2(0, -1.5f));
    }
    public void UpgradeHealth()
    {
        startingHealth++;
        curHealth = startingHealth;
    }
    public void loadHealth(float _maxHealth, float _curHealth)
    {
        startingHealth = _maxHealth;
        curHealth = _curHealth;
    }
}
