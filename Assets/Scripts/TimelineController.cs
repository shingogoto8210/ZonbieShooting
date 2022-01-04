using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    public void StarGame()
    {
        railMoveController.Move();
    }
}
