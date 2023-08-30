using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GhostManager : MonoBehaviour
{
    public bool testGameOver;
    [SerializeField] private PlayerGameOverManager gameOverManager;
    [SerializeField] private float recordInterval;
    public List<GhostData> ghostDatas = new List<GhostData>();
    private WaitForSeconds replayWait;


    private void Start()
    {
        replayWait = new WaitForSeconds(recordInterval);
    }

    private IEnumerator ReplayGhost()
    {
        //��Ȱ��ȭ �Ǿ��ִ� ghost�� ���ֱ�
        foreach(GhostData ghost in ghostDatas)
        {
            ghost.gameObject.SetActive(true);
        }

        int nowCount = 0;
        while (!gameOverManager.isPlayerDaad) // �÷��̾ ���ӿ��� ���� �ʾҴٸ� while�� ����
        {
            
            //Debug.Log(nowCount);
            foreach (GhostData ghost in ghostDatas)
            {
                if(ghost.recordPosition.Count - 1 == nowCount){
                    ghost.gameObject.SetActive(false);
                }

                if (ghost.recordPosition.Count-1 > nowCount)
                {
                    Debug.Log(ghost.recordPosition[nowCount]);
                    ghost.transform.position = ghost.recordPosition[nowCount];
                    Vector3 localScale = transform.localScale;
                    localScale.x = ghost.recordLocalScaleX[nowCount];
                    ghost.transform.localScale = localScale;
                    ghost.ghostAnimator.SetBool("dodge", ghost.dodgeTrigger[nowCount]);
                    ghost.ghostAnimator.SetBool("move", ghost.moveTrigger[nowCount]);
                    ghost.ghostAnimator.SetBool("jump", ghost.jumpTrigger[nowCount]);
                    ghost.ghostAnimator.SetBool("attack", ghost.attackTrigger[nowCount]);
                }
            }
            nowCount++;
            yield return replayWait;
        }
        
    }

    [Button]
    public void StartReplay()
    {
        StartCoroutine(ReplayGhost());
    }

}
