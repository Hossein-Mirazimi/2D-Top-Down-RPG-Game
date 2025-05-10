using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // * Parameters
    [SerializeField] float roamChangeDirFloat = 2f;
    // * State
    private enum State {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    IEnumerator RoamingRoutine () {
        while(state == State.Roaming) {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }

    Vector2 GetRoamingPosition () {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
