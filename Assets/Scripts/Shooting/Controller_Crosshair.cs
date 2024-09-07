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

    [SerializeField] float travelTime, maxTravel;
    [SerializeField] float lingerTime;

    Tween crossAnim;
    bool isShooting;

    private void Start()
    {
        startUp = up.rectTransform.anchoredPosition.y;
        startDown = down.rectTransform.anchoredPosition.y;
        startRight = right.rectTransform.anchoredPosition.x;
        startLeft = left.rectTransform.anchoredPosition.x;
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
        if (!isShooting)
        {
            isShooting = true;
            crossAnim.Kill();
            crossAnim = DOTween.To(() => value, x => value = x, maxTravel, travelTime).OnComplete(ReturnCrosshair);
        }
    }

    private void ReturnCrosshair()
    {
        isShooting = false;
        StartCoroutine(linger());
    }

    private IEnumerator linger()
    {
        yield return new WaitForSeconds(lingerTime);

        if(!isShooting)
            crossAnim = DOTween.To(() => value, x => value = x, 0, travelTime);
    }
}
