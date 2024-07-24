using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    private RoomsCanvases roomCanvases;
    public void FirstInitilize(RoomsCanvases canvases)
    {
        roomCanvases = canvases;
    }
}
