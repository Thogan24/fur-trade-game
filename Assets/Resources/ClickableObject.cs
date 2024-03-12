using UnityEngine;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour
{
    public GameObject infoScreen;

    private void Start()
    {
        // Make sure the info screen is initially inactive
        if (infoScreen != null)
        {
            infoScreen.SetActive(false);
        }

        // Get the Button component attached to this object
        Button button = GetComponent<Button>();

        // Add listener for the button click event
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("Button component is not attached!");
        }
    }

    private void OnClick()
    {
        // Check if the info screen object is assigned
        if (infoScreen != null)
        {
            if (infoScreen.activeSelf == false)
            {
                // Activate the info screen
                infoScreen.SetActive(true);
            }
            else if (infoScreen.activeSelf == true )
            {
                // Activate the info screen
                infoScreen.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Info Screen object is not assigned!");
        }

        // Make sure the object is visible
        gameObject.SetActive(true);
    }
}