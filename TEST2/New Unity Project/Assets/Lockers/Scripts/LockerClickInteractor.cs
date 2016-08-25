using UnityEngine;
using System.Collections;

public class LockerClickInteractor : LockerInteractor {

    public enum MouseButtonID {

        None = -1,
        Left = 0,
        Right = 1,
        Middle = 2

    }

    [SerializeField]
    MouseButtonID primaryButton = MouseButtonID.Left;

    [SerializeField]
    MouseButtonID secondaryButton = MouseButtonID.None;

    [SerializeField]
    string primaryActAnimParam;

    [SerializeField]
    string secondaryActAnimParam;

    Animator animator;

    void ResetAnimatorTrigger(string name) {

        if (!string.IsNullOrEmpty(name)) {

            animator.ResetTrigger(name);

        }

    }

    void SetAnimatorTrigger(string name, bool value) {

        if (!string.IsNullOrEmpty(name) && animator != null && animator.isActiveAndEnabled) {

            ResetAnimatorTrigger(primaryActAnimParam);
            ResetAnimatorTrigger(secondaryActAnimParam);

            animator.SetTrigger(name);

        }

    }
        
    public bool IsButtonPressed(MouseButtonID button) {

        if (button == MouseButtonID.None) return false;

        return Input.GetMouseButtonDown((int)button);

    }

    protected virtual void OnMouseOver() {

        if (IsButtonPressed(primaryButton)) {

            Use();
            SetAnimatorTrigger(primaryActAnimParam, true);

        }

        if (IsButtonPressed(secondaryButton)) {

            Use(true);
            SetAnimatorTrigger(secondaryActAnimParam, true);

        }

    }

    protected override void Awake() {

        base.Awake();

        animator = gameObject.GetComponent<Animator>();

    }

}
