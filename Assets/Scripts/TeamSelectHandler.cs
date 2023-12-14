using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TeamSelectHandler : MonoBehaviour
{

    [SerializeField] GameObject IroquoisButton;
    [SerializeField] GameObject IroquoisMenu;
    [SerializeField] Button IroquoisClose;

    [SerializeField] GameObject MunseeButton;
    [SerializeField] GameObject MunseeMenu;
    [SerializeField] Button MunseeClose;

    [SerializeField] GameObject DutchButton;
    [SerializeField] GameObject DutchMenu;
    [SerializeField] Button DutchClose;

    [SerializeField] GameObject PhilipsesButton;
    [SerializeField] GameObject PhilipsesMenu;
    [SerializeField] Button PhilipsesClose;

    bool menuOpen = false;

    void Start()
    {
        IroquoisButton.SetActive(true);
        IroquoisMenu.SetActive(false);

        Button IroquoisButtonReal = IroquoisButton.GetComponent<Button>();
        IroquoisButtonReal.onClick.AddListener(IroquoisButtonClicked);
        IroquoisClose.onClick.AddListener(IroquoisClosedClicked);

        MunseeButton.SetActive(true);
        MunseeMenu.SetActive(false);

        Button MunseeButtonReal = MunseeButton.GetComponent<Button>();
        MunseeButtonReal.onClick.AddListener(MunseeButtonClicked);
        MunseeClose.onClick.AddListener(MunseeClosedClicked);

        DutchButton.SetActive(true);
        DutchMenu.SetActive(false);

        Button DutchButtonReal = DutchButton.GetComponent<Button>();
        DutchButtonReal.onClick.AddListener(DutchButtonClicked);
        DutchClose.onClick.AddListener(DutchClosedClicked);

        PhilipsesButton.SetActive(true);
        PhilipsesMenu.SetActive(false);

        Button PhilipsesButtonReal = PhilipsesButton.GetComponent<Button>();
        PhilipsesButtonReal.onClick.AddListener(PhilipsesButtonClicked);
        PhilipsesClose.onClick.AddListener(PhilipsesClosedClicked);
    }

    public void IroquoisButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            IroquoisMenu.SetActive(true);
        }
    }

    public void IroquoisClosedClicked()
    {
         menuOpen = false;
         IroquoisMenu.SetActive(false);
        
    }

    public void MunseeButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            MunseeMenu.SetActive(true);
        }
    }

    public void MunseeClosedClicked()
    {
        menuOpen = false;
        MunseeMenu.SetActive(false);

    }

    public void DutchButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            DutchMenu.SetActive(true);
        }
    }

    public void DutchClosedClicked()
    {
        menuOpen = false;
        DutchMenu.SetActive(false);

    }

    public void PhilipsesButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            PhilipsesMenu.SetActive(true);
        }
    }

    public void PhilipsesClosedClicked()
    {
        menuOpen = false;
        PhilipsesMenu.SetActive(false);

    }

}
