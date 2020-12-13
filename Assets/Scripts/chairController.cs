using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(EventTrigger))]
public class chairController : MonoBehaviour
{
    [Range(0, 10)] public float _speed;
    [Range(0, 360)] public float _rotation;

    private Transform _cam;
    private bool _selected;

    public List<EventTriggerType> _trigerList;
    private EventTrigger _event;

    private void Awake()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").transform;

        _event = GetComponent<EventTrigger>();

        bool value = true;

        for (int i = 0; i < _trigerList.Count; i++)
        {
            CreateTriggerIteration(i, value);
            value = !value;
        }
    }
    private void Update()
    {
        if (_selected)
        {
            Vector3 setRot = transform.localEulerAngles;
            setRot.y = Mathf.Lerp(setRot.y, _rotation, Time.deltaTime * _speed);
            transform.eulerAngles = setRot;
        }
    }
    void CreateTriggerIteration(int trigerList, bool value)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = _trigerList[trigerList];
        entry.callback.AddListener((data) => { SelectionVR(value); });
        _event.triggers.Add(entry);
    }
    public void SelectionVR(bool selectionVale)
    {
        _selected = selectionVale;
    }
}