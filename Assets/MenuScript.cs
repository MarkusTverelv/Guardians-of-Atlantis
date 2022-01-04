using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour
{
    [HideInInspector] public List<SelectedScript> selectedScripts = new List<SelectedScript>();
    AudioSource audioSource;
     int _selectedOption = 0;
    public int selectedOption
    {
        get
        {
            return _selectedOption;
        }
        set
        {
            audioSource.Play();
            selectedScripts[_selectedOption].selected = false;
            selectedScripts[value].selected = true;
            _selectedOption = value;
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow)) && selectedOption != selectedScripts.Count - 1)
            selectedOption++;
        else if ((Input.GetKeyDown(KeyCode.UpArrow)) && selectedOption != 0)
            selectedOption--;
        else if (Input.GetKeyDown(KeyCode.Return))
            selectedScripts[selectedOption].OnEnter.Invoke();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (Transform t in transform)
            selectedScripts.Add(t.gameObject.GetComponent<SelectedScript>());
    }
    private void OnEnable()
    {
        selectedOption = 1;
    }
}
