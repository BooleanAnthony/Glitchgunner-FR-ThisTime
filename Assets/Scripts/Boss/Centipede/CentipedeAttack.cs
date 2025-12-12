using UnityEngine;
using System.Collections;

public class CentipedeAttack : MonoBehaviour
{
    [SerializeField] GameObject head;
    [SerializeField] Animator anim;
    [SerializeField] Vector2 minAttackPos; // top-left
    [SerializeField] Vector2 maxAttackPos; // bottom-right
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float cooldown = 10f;

    private float timer = 0f;
    private bool isAttacking = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (!isAttacking && timer >= cooldown)
        {
            StartAttack();
        }

        anim.SetBool("isAttacking", isAttacking);
    }

    public void StartAttack()
    {
        isAttacking = true;
        anim.SetTrigger("attack");
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        Debug.Log("Attack started");
        yield return new WaitForSeconds(2f);

        Vector3 originalPos = head.transform.position;
        float randomX = Random.Range(minAttackPos.x, maxAttackPos.x);
        Vector3 targetXPos = new Vector3(randomX, originalPos.y, originalPos.z);
        Debug.Log($"Moving to X: {randomX}");
        yield return MoveToPosition(head.transform, targetXPos);

        float targetY = Mathf.Min(minAttackPos.y, maxAttackPos.y);
        Vector3 targetDownPos = new Vector3(randomX, targetY, originalPos.z);
        Debug.Log($"Moving down to Y: {targetY}");
        yield return MoveToPosition(head.transform, targetDownPos);

        moveSpeed *= 2;

        Debug.Log("Returning to original position");
        yield return MoveToPosition(head.transform, originalPos);

        isAttacking = false;
        timer = 0;
        anim.enabled = true;
        moveSpeed /= 2;
        Debug.Log("Attack complete");
    }


    private IEnumerator MoveToPosition(Transform target, Vector3 destination)
    {
        while (Vector3.Distance(target.position, destination) > 0.05f)
        {
            target.position = Vector3.MoveTowards(target.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
        target.position = destination;
    }
}