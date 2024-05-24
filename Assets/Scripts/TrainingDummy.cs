using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitText;
    [SerializeField] float timeToShowText;

    float timeLeftToShowText;
    bool isShowingText;

    public void GotHit()
    {
        timeLeftToShowText = timeToShowText;
        isShowingText = true;
        hitText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isShowingText) { return; }

        timeLeftToShowText -= Time.deltaTime;

        if (timeLeftToShowText <= 0 )
        {
            hitText.gameObject.SetActive(false);
            isShowingText = false;
        }
    }
}
