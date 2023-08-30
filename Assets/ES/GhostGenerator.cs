using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GhostGenerator : MonoBehaviour
{
    public bool testGameOver;

    [SerializeField] private Player player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float recordInterval;
    private WaitForSeconds replayWait;
    [SerializeField] private GhostManager ghostManager;


    private void Start()
    {
        replayWait = new WaitForSeconds(recordInterval);
    }

    private IEnumerator RecordGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, Vector3.zero, Quaternion.identity);
        GhostData ghostData = ghost.GetComponent<GhostData>();

        while (!player.isPlayerDaad) //���ӿ����� ���� �ʾҴٸ� while�� ����
        {
            ghostData.recordPosition.Add(transform.position);
            ghostData.recordLocalScaleX.Add(transform.localScale.x);
            ghostData.dodgeTrigger.Add(playerAnimator.GetBool("dodge"));
            ghostData.moveTrigger.Add(playerAnimator.GetBool("move"));
            ghostData.jumpTrigger.Add(playerAnimator.GetBool("jump"));
            ghostData.attackTrigger.Add(playerAnimator.GetBool("attack"));
            yield return replayWait;
        }
		Vector2 ���ڸ� = Vector2.zero;
		ghostData.deadVector = ���ڸ�;
        ghostManager.ghostDatas.Add(ghostData);
    }

    [Button]
    public void StartRecord()
    {
        StartCoroutine(RecordGhost());
    }
}
