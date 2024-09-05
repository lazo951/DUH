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
    //[SerializeField] Transform bulletSpawnPosition;

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

        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchGuns();
        }
    }

    private void SwitchGuns()
    {
        activeGun = null;

        previousGun = currentGun;

        currentGun++;
        if (currentGun > availableGuns.Count-1)
            currentGun = 0;

        PutGunDown();
    }

    private void PutGunDown()
    {
        availableGuns[previousGun].DOLocalMove(inactivePosition, 0.5f).OnComplete(DeactivateGun);
    }

    private void PutGunUp()
    {
        availableGuns[currentGun].DOLocalMove(activePosition, 0.5f).OnComplete(SetNewGun);
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
