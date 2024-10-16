using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_DamageIndicator : MonoBehaviour
{
    [SerializeField] CanvasGroup groupDamageEffect;
    [SerializeField] CanvasGroup[] groupDamageDirection;
    [SerializeField] Transform indicatorArrow;
    [SerializeField] float indicatorDuration;
    [SerializeField] float groupDuration, groupFadeSpeed;

    //public void ShowIndicatorMesh(Quaternion rot)
    //{
    //    ToggleGroup();
    //    indicatorArrow.rotation = rot;
    //    indicatorArrow.GetComponentInChildren<MeshRenderer>().enabled = true;
    //    float timeElapsed = 0;

    //    StartCoroutine(checkDuration());
    //    IEnumerator checkDuration()
    //    {
    //        while (timeElapsed < indicatorDuration)
    //        {
    //            timeElapsed += Time.deltaTime;
    //            yield return null;
    //        }

    //        indicatorArrow.GetComponentInChildren<MeshRenderer>().enabled = false;
    //    }
    //}

    public void ShowIndicator(int pos)
    {
        for(int i = 0; i < groupDamageDirection.Length; i++)
        {
            if(i == pos)
                groupDamageDirection[i].gameObject.SetActive(true);
            else
                groupDamageDirection[i].gameObject.SetActive(false);
        }
        
        ToggleGroup();
    }

    private void ToggleGroup()
    {
        DOTween.Kill("Group");
        groupDamageEffect.DOFade(0.25f, groupFadeSpeed).SetId("Group");

        StartCoroutine(waitFade());
        IEnumerator waitFade()
        {
            yield return new WaitForSeconds(groupDuration);
            groupDamageEffect.DOFade(0, groupFadeSpeed).SetId("Group");
        }
    }

    //private void ToggleGroup()
    //{
    //    DOTween.Kill("Group");
    //    groupDamageEffect.DOFade(0.25f, groupFadeSpeed).SetId("Group");

    //    StartCoroutine(waitFade());
    //    IEnumerator waitFade()
    //    {
    //        yield return new WaitForSeconds(groupDuration);
    //        groupDamageEffect.DOFade(0, groupFadeSpeed).SetId("Group");
    //    }
    //}
}
