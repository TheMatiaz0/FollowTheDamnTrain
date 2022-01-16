using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoorController : PlayerTrigger
{
    public const string DIRECTION_PARAMETER = "Direction";
    public const string OPEN_STATE = "OpenTrainDoors";
    private Animator anim = null;

    [SerializeField]
    private TrainMovementController mover = null;

    private bool objectInPhotocell = false;

    [SerializeField]
    private float openStandardSpeed = 4;

    [SerializeField]
    private float closeStandardSpeed = 1;

    [Header("Photocell")]
    [SerializeField]
    private BoxCollider mainCollider = null;
    public BoxCollider MainCollider => mainCollider;

    [SerializeField]
    private float firstDoorOpenSpeedup = 2.5f;

    [SerializeField]
    private float secondDoorOpenSpeedup = 3;

    [Header("SFX")]
    [SerializeField]
    private AudioSource doorsSource = null;

    [SerializeField]
    private AudioClip closeDoorSound = null;

    [SerializeField]
    private AudioClip openDoorSound = null;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator Open(float multiplier = 1)
    {
        PlayOpenDoorSound();
        anim.SetFloat(DIRECTION_PARAMETER, 1.0f * multiplier);
        anim.CrossFade(OPEN_STATE, 1, -1, 0);
        yield return new WaitForSeconds(openStandardSpeed);
    }
    public IEnumerator Close()
    {
        PlayCloseDoorSound();
        anim.SetFloat(DIRECTION_PARAMETER, -1.0f);
        anim.CrossFade(OPEN_STATE, 1, -1, 0);
        yield return new WaitForSeconds(closeStandardSpeed);
    }

    public override void OnPlayerTrigger(FPPMovement fpp, bool isEnter)
    {
        if (isEnter)
        {
            objectInPhotocell = false;
            mover.StopAllCoroutines();
            StartCoroutine(Open(firstDoorOpenSpeedup));
            StartCoroutine(WaitUntilPlayerLeaves());
        }

        else
        {
            objectInPhotocell = true;
        }
    }

    private IEnumerator WaitUntilPlayerLeaves()
    {
        yield return new WaitUntil(() => objectInPhotocell == true);
        mover.stationStopped = mover.StartCoroutine(mover.StationStop(secondDoorOpenSpeedup));
    }

    private void PlayCloseDoorSound()
    {
        doorsSource.clip = closeDoorSound;
        doorsSource.Play();
    }

    private void PlayOpenDoorSound()
    {
        doorsSource.clip = openDoorSound;
        doorsSource.Play();
    }
}
