using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_Shooting : MonoBehaviour
{
    [SerializeField] List<Transform> availableGuns = new List<Transform>();
    int previousGun, currentGun;
    [SerializeField] Gun_Base activeGun;
    [SerializeField] Vector3 activePosition, inactivePosition;
    [SerializeField] float switchSpeed;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            activeGun?.Shoot(transform);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            activeGun?.Reload();
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
        {
            SwitchGunIncrement((Input.GetAxisRaw("Mouse ScrollWheel") > 0f) ? 1 : -1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGunDirect(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGunDirect(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchGunDirect(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchGunDirect(3);
        }
    }

    private void SwitchGunDirect(int num)
    {
        if (num > availableGuns.Count - 1 || currentGun == num)
            return;

        DOTween.Kill("GunUp");
        DOTween.Kill("GunDown");

        activeGun = null;
        previousGun = currentGun;
        currentGun = num;

        PutGunDown();
    }

    private void SwitchGunIncrement(int num)
    {
        DOTween.Kill("GunUp");
        DOTween.Kill("GunDown");

        activeGun = null;
        previousGun = currentGun;
        currentGun += num;

        if(currentGun < 0)
            currentGun = availableGuns.Count - 1;
        else if(currentGun > availableGuns.Count - 1)
            currentGun = 0;

        PutGunDown();
    }

    private void PutGunDown()
    {
        availableGuns[previousGun].DOLocalMove(inactivePosition, switchSpeed / 2).SetId("GunDown").OnComplete(DeactivateGun);
    }

    private void PutGunUp()
    {
        availableGuns[currentGun].DOLocalMove(activePosition, switchSpeed / 2).SetId("GunUp").OnComplete(SetNewGun);
    }

    private void ActivateGun()
    {
        availableGuns[currentGun].gameObject.SetActive(true);
        PutGunUp();
    }

    private void DeactivateGun() 
    {
        availableGuns[previousGun].gameObject.SetActive(false);
        ActivateGun();
    }

    private void SetNewGun()
    {
        activeGun = availableGuns[currentGun].GetComponent<Gun_Base>();
    }
}
