using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataManager : MonoBehaviour
{
    public static LocalDataManager Instance;

    [SerializeField] List<GameObject> perfectAwards;

    public bool isTutorialCleared = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        TutorialCanSkip();
    }

    void Start()
    {
        

    }

    public void GetTrophy()
    {
        if (PlayerPrefs.GetString("�г��� �Ǵ� ������") == "Achieve")
        {
            perfectAwards[0].SetActive(true);
        }
        if (PlayerPrefs.GetString("�г��� �ƹ�ư ���") == "Achieve")
        {
            perfectAwards[1].SetActive(true);
        }
        if (PlayerPrefs.GetString("�г��� �ƹ�ư ����") == "Achieve")
        {
            perfectAwards[2].SetActive(true);
        }
    }

    public void SetTrophy(string bossname)
    {
        PlayerPrefs.SetString(bossname, "Achieve");
    }

    void TutorialCanSkip()
    {
        if (PlayerPrefs.GetInt("TutorialClear") == 1)
        {
            isTutorialCleared = true;
        }
        else
        {
            isTutorialCleared = false;
        }


    }

    public void ClearTutorial()
    {
        PlayerPrefs.SetInt("TutorialClear", 1);
    }

    [Button]
    void DebugDeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [Button]
    void DebugClearTutorial()
    {
        PlayerPrefs.SetInt("TutorialClear", 1);
    }
}
