using System.Collections;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Movement")]
    public float detectionRange = 6f;
    public float chaseSpeed = 2.5f;
    public float stopDistance = 0.8f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isChasing = false;
    private bool isAttacking = false;
    private bool canAttack = true;

    [Header("Attack")]
    public float attackCooldown = 1f;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        startPosition = transform.position;
    }

    void Update()
    {
        if (isAttacking) return;

        float distance = Vector2.Distance(rb.position, player.position);

        if (distance <= detectionRange)
        {
            if (!isChasing)
                StartChase();

            ChasePlayer(distance);
        }
        else
        {
            StopChase();
        }
    }

    void StartChase()
    {
        isChasing = true;
        animator.SetBool("isChasing", true);

        AudioManager.instance.PlayZombieRoar();
        AudioManager.instance.StartChase();
    }

    void StopChase()
    {
        isChasing = false;
        animator.SetBool("isChasing", false);

        AudioManager.instance.StopChase();

        rb.linearVelocity = Vector2.zero;
    }

    void ChasePlayer(float distance)
    {
        // 🔥 parar antes de encostar
        if (distance <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );

        rb.MovePosition(newPos);
    }

    // 👹 ATAQUE (TRIGGER)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canAttack || isAttacking) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(AttackRoutine(other.gameObject));
        }
    }

    IEnumerator AttackRoutine(GameObject playerObj)
    {
        isAttacking = true;
        canAttack = false;

        rb.linearVelocity = Vector2.zero;

        animator.SetTrigger("isAttacking");

        PlayerHealth ph = playerObj.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(1);
        }

        yield return new WaitForSeconds(1f); // duração ataque

        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    // 🔥 RESET DO ZOMBIE (GRAVEYARD EXIT)
    public void ResetZombie()
    {
        transform.position = startPosition;

        isChasing = false;
        isAttacking = false;
        canAttack = true;

        animator.SetBool("isChasing", false);

        rb.linearVelocity = Vector2.zero;

        AudioManager.instance.StopChase();
    }
}