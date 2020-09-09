using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button openButton;

    public delegate void Chest();
    public Chest Open;

    public static Buttons instance;
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    private void Start()
    {
        SetDisabled();
    }
    private void SetDisabled()
    {
        openButton.interactable = false;
    }
    public void OpenChest()
    {
        Open();
    }
}
