using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleTrigger : Toggle
{
    // Override the OnKeyDown method to block space bar input
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);  // Only allow mouse clicks
    }

    // Disable toggle interaction when space bar is pressed
    public override void OnSubmit(BaseEventData eventData)
    {
        // Don't do anything on submit (space or Enter)
    }
}
