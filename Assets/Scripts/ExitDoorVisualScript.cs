using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorVisualScript : MonoBehaviour
{
    [SerializeField] ExitDoorScript exitDoorScript;
    private const string DOOR_OPENING_ANIMATION = "DoorOpeningAnimation";
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        exitDoorScript = exitDoorScript.gameObject.GetComponent<ExitDoorScript>();
        exitDoorScript.DoorOpening += exitDoorScript_DoorOpening;
    }

    private void exitDoorScript_DoorOpening(object sender, EventArgs e)
    {
        StartCoroutine(DoorOpeningAnimation());
    }

    private IEnumerator DoorOpeningAnimation()
    {
        Player.Instance.canMove = false;
        animator.Play(DOOR_OPENING_ANIMATION);
        CinemachineLookAtScript.Instance.ChangeTarget(transform);
        yield return new WaitForSeconds(2);
        exitDoorScript.StartEndingLevel();
        
    }

}
