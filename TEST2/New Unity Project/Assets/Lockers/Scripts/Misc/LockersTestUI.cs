using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class LockersTestUI : MonoBehaviour {

    [SerializeField]
    Text infoText;

    [SerializeField]
    Button nextLockerButton;

    [SerializeField]
    Locker[] targetLockers;

    [TextArea]
    [SerializeField]
    string[] instructions;

    CameraFocusSwitcher camSwitcher;
    int curLockerInd;
    Locker curLocker;

    public void SwitchToLocker(int index) {

        if (targetLockers.Length > 0) {

            index = Mathf.Abs(index);
            index %= targetLockers.Length;
            curLocker = targetLockers[index];

            if (camSwitcher != null) {

                camSwitcher.SetFocusPoint(index);

            }

        }

    }

    public void SwitchToNextLocker() {

        SwitchToLocker(++curLockerInd);

    }

    void Awake() {

        camSwitcher = FindObjectOfType<CameraFocusSwitcher>();

        if (nextLockerButton != null) {

            nextLockerButton.onClick.AddListener(SwitchToNextLocker);

        }

        if (instructions.Length < targetLockers.Length) {

            instructions = new string[targetLockers.Length];

        }

        SwitchToLocker(0);

    }

    void Update() {

        if (curLocker != null) {

            if (infoText != null) {

                infoText.text = curLocker.ToString() + System.Environment.NewLine + instructions[curLockerInd % targetLockers.Length];

            }

        }

    }

}
