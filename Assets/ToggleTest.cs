using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class ToggleTest : MonoBehaviour
{
    ToggleGroup toggleGroupInstance;
    public Toggle currentSelection
    {
        get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }

    void start ()
    {
        toggleGroupInstance = GetComponent<ToggleGroup>();
        Debug.Log("First Selected" + currentSelection.name);
        SelectToggle(2);
    }

    public void SelectToggle(int id)
    {
        var toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();
        toggles[id].isOn = true;
    }
}
