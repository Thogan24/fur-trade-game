using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomsCanvases roomCanvases;
    public void FirstInitilize(RoomsCanvases canvases)
    {
        roomCanvases = canvases;
    }
}
