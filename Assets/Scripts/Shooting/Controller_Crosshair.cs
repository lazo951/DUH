using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Controller_Crosshair : MonoBehaviour
{
    [SerializeField] Image up, down, right, left;
    float startUp, startDown, startRight, startLeft;
    float value;
    bool isShooting;
    Tween crossAnim;
    [SerializeField] int maxTravel;
    [SerializeField] float lingerTime, inBetweenTime, returnTime;

    [SerializeField] Image hitMarker;
    Tween hitAnim;
    Color hitMarkerStart;
    [SerializeField] float markerFadeTime;

    private void Start()
    {
        startUp = up.rectTransform.anchoredPosition.y;
        startDown = down.rectTransform.anchoredPosition.y;
        startRight = right.rectTransform.anchoredPosition.x;
        startLeft = left.rectTransform.anchoredPosition.x;

        hitMarkerStart = hitMarker.color;
        Color invCol = hitMarkerStart;
        invCol.a = 0f;
        hitMarker.color = invCol;
    }

    private void Update()
    {
        up.rectTransform.anchoredPosition = new Vector2(0, startUp + value);
        down.rectTransform.anchoredPosition = new Vector2(0, startDown - value);
        right.rectTransform.anchoredPosition = new Vector2(startRight + value, 0);
        left.rectTransform.anchoredPosition = new Vector2(startLeft - value, 0);
    }

    public void ToggleShooting()
    {
        value += maxTravel/10;
        value = Mathf.Clamp(value, 0, maxTravel);

        if (!isShooting)
        {
            isShooting = true;
            crossAnim.Kill();
            StartCoroutine(lingerReturn());
        }
    }

    private IEnumerator lingerReturn()
    {
        yield return new WaitForSeconds(inBetweenTime);
        isShooting = false;
        yield return new WaitForSeconds(inBetweenTime);

        if (!isShooting)
        {
            yield return new WaitForSeconds(lingerTime);
            crossAnim = DOTween.To(() => value, x => value = x, 0, returnTime);
        }
    }

    public void ToggleHitmarker()
    {
        DOTween.Kill("Marker");
        hitMarker.color = hitMarkerStart;
        StartCoroutine(fadeMarker());
    }

    private IEnumerator fadeMarker()
    {
        yield return new WaitForSeconds(lingerTime);
        hitAnim = hitMarker.DOFade(0f, markerFadeTime).SetId("Marker");
    }
}
