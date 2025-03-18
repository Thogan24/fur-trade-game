using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu createRoomMenu;
    [SerializeField]
    private RoomsListingMenu roomsListingMenu;
    private RoomsCanvases roomCanvases;
    public void FirstInitilize(RoomsCanvases canvases)
    {
        Debug.Log("Running firstinitialize thing");
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != "Main_Scene")
        {
            roomCanvases = canvases;
            createRoomMenu.FirstInitilize(canvases);
            roomsListingMenu.FirstInitilize(canvases);
        }
        else
        {
            Debug.Log("Rejoining, first initialize");
        }
    }
}
