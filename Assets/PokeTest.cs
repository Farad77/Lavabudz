using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PokeTest : InteractableColorVisual
{
    [SerializeField, Interface(typeof(IInteractableView))]
    private UnityEngine.Object _a;
    private IInteractableView a { get; set; }
    protected virtual void Awake()
    {
        a = _a as IInteractableView;
    }

    protected virtual void Start()
    {
        this.BeginStart(ref _started);

        this.AssertField(a, nameof(a));
       

        UpdateVisual();
        this.EndStart(ref _started);
    }

    protected virtual void OnEnable()
    {
        if (_started)
        {
            UpdateVisual();
            a.WhenStateChanged += UpdateVisualState;
        }
    }
    private void UpdateVisualState(InteractableStateChangeArgs args)
    {
        Debug.Log("ok "+args);
    }
    
}
