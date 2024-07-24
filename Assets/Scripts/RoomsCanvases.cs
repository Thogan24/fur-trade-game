using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinRoomCanvas createOrJoinRoomCanvas;

    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas { get { return createOrJoinRoomCanvas; } }

    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }


    private void Awake()
    {
        FirstInitilize();
    }

    private void FirstInitilize()
    {
        createOrJoinRoomCanvas.FirstInitilize(this);
        CurrentRoomCanvas.FirstInitilize(this);
    }

}
