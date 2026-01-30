using UnityEngine;
using System.Collections;

public class LungeAttack : MonoBehaviour
{
    public enum Direction
    {
        Top,
        Bottom
    }

    public Direction targetDirection;
    public Vector2 originalPos;
    public Vector2 targetPoint;
    public float attackDuration = 1f;

    public void SetOriginalPos()
    {
        originalPos = transform.localPosition;
    }

    // Generic movement coroutine
    private IEnumerator MoveRoutine(Vector2 startPos, Vector2 endPos)
    {
        float elapsedTime = 0f;

        while (elapsedTime < attackDuration)
        {
            float t = elapsedTime / attackDuration; // progress 0→1
            transform.localPosition = Vector2.Lerp(startPos, endPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPos; // ensure exact final position
        Debug.Log("Ended Movement");
    }

    // Lunge forward
    public IEnumerator AttackRoutine()
    {
        targetDirection = (Direction)Random.Range(0, (int)Direction.Bottom + 1);
        
        Vector2 targetPoint = new(-15, 0);

        if (targetDirection == Direction.Top)
        {
            targetPoint = new(-15, 2);
        }
        else if (targetDirection == Direction.Bottom)
        {
            targetPoint = new(-15, -2);
        }
        
        Debug.Log("Attack started towards " + targetPoint);

        yield return MoveRoutine(transform.localPosition, targetPoint);

        Debug.Log("Started Attack");
    }

    // Return to original position
    public IEnumerator ReturnRoutine()
    {
        if (Vector2.Distance(transform.localPosition, originalPos) < 0.01f)
            yield break;

        yield return MoveRoutine(transform.localPosition, originalPos);

        Debug.Log("Attack complete");
    }
}
