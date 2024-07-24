using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu createRoomMenu;
    [SerializeField]
    private RoomsListingMenu roomsListingMenu;
    private RoomsCanvases roomCanvases;
    public void FirstInitilize(RoomsCanvases canvases)
    {
        roomCanvases = canvases;
        createRoomMenu.FirstInitilize(canvases);
        roomsListingMenu.FirstInitilize(canvases);
    }
}
