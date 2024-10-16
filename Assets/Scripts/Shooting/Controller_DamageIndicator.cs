using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_DamageIndicator : MonoBehaviour
{
    [SerializeField] CanvasGroup groupDamageEffect;
    [SerializeField] CanvasGroup[] groupDamageDirection;

    [SerializeField] float damageEffectDuration, damageEffectFadeSpeed, damageEffectAlpha;
    [SerializeField] float directionEffectDuration, directionEffectFadeSpeed, directionEffectAlpha;

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
        ShowGroupDirection(pos);
        ShowGroupDamage();
    }

    private void ShowGroupDamage()
    {
        DOTween.Kill("Group");
        groupDamageEffect.DOFade(damageEffectAlpha, damageEffectFadeSpeed).SetId("Group");

        StartCoroutine(waitFade());
        IEnumerator waitFade()
        {
            yield return new WaitForSeconds(damageEffectDuration);
            groupDamageEffect.DOFade(0, damageEffectFadeSpeed).SetId("Group");
        }
    }

    private void ShowGroupDirection(int pos)
    {
        DOTween.Kill("Dir");
        groupDamageDirection[pos].DOFade(directionEffectAlpha, directionEffectFadeSpeed).SetId("Dir");

        StartCoroutine(waitFade());
        IEnumerator waitFade()
        {
            yield return new WaitForSeconds(directionEffectDuration);
            groupDamageDirection[pos].DOFade(0, directionEffectFadeSpeed).SetId("Dir");
        }
    }
}
