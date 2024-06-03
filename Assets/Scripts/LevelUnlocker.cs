using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] UnlockerType unlockerType;
    [SerializeField] UnlockedLevels unlockedLevels;

    [Header("Only needed if is on start menu")]
    [SerializeField] Button[] selectLevelButtons;
    [SerializeField] GameObject playLevelToUnlockInfoText;

    [Header("Only needed if is in a fighting room")]
    [SerializeField] int room;

    private void Start()
    {
        if (unlockerType == UnlockerType.StartMenu)
        {
            for (int i = 0; i < selectLevelButtons.Length; i++)
            {
                selectLevelButtons[i].interactable = unlockedLevels.unlockedLevels[i];

                if (!unlockedLevels.unlockedLevels[i])
                {
                    playLevelToUnlockInfoText.SetActive(true);
                }
            }
        }
        else
        {
            UnlockLevel(room);
        }
    }

    public void UnlockLevel(int level)
    {
        unlockedLevels.unlockedLevels[level - 1] = true;
    }
}

public enum UnlockerType
{
    StartMenu,
    NextLevel
}
