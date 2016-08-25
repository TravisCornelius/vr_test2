using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public sealed class CameraFocusSwitcher : MonoBehaviour {

    [System.Serializable]
    public class FocusChangedEvent : UnityEvent<Transform> { }

    public float transitionSpeed = 6f;

    public Camera currentCamera {
        get { return _targetCamera == null ? Camera.main : _targetCamera; }
    }

    bool hasFocusPoints {
        get { return focusPoints != null && focusPoints.Length > 0; }
    }

    public FocusChangedEvent onFocusChanged {
        get { return _onFocusChanged; }
    }

    [SerializeField]
    Camera _targetCamera;

    [SerializeField]
    Transform[] focusPoints;

    [SerializeField]
    Color gizmoColor = new Color(0f, 1f, 0f, 0.5f);

    [Space(4f)]
    [SerializeField]
    FocusChangedEvent _onFocusChanged;

    Vector3 targetPos;
    Quaternion targetRot;
    int curPointInd;

    public void SetFocusPoint(Transform point, bool useTransition = true) {

        if (point == null) return;

        targetPos = point.position;
        targetRot = point.rotation;

        if (!useTransition && currentCamera != null) {

            currentCamera.transform.position = point.position;
            currentCamera.transform.rotation = point.rotation;

        }

    }

    public void SetFocusPoint(int index, bool useTransition = true) {

        if (hasFocusPoints) {

            if (index < 0) {

                index = -index;

            }

            index %= focusPoints.Length;
            SetFocusPoint(focusPoints[index], useTransition);

        }

    }

    public void SwitchToNextFocusPoint() {

        SetFocusPoint(++curPointInd);

    }

    public void SwitchToPrevFocusPoint() {

        SetFocusPoint(--curPointInd);

    }

    void Awake() {

        if (currentCamera != null) {

            targetPos = currentCamera.transform.position;
            targetRot = currentCamera.transform.rotation;

        }

        SetFocusPoint(curPointInd, false);

    }

    void LateUpdate() {

        if (currentCamera == null) return;

        float speed = transitionSpeed * Time.smoothDeltaTime;

        currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, targetPos, speed);
        currentCamera.transform.rotation = Quaternion.Lerp(currentCamera.transform.rotation, targetRot, speed);

    }

    void OnDrawGizmos() {

        if (currentCamera != null && hasFocusPoints) {

            Gizmos.color = gizmoColor;

            foreach (Transform p in focusPoints) {

                if (p == null) continue;

                Gizmos.DrawWireSphere(p.position, 0.1f);

                Matrix4x4 stMatrix = Gizmos.matrix;
                Gizmos.matrix = Matrix4x4.TRS(p.position, p.rotation, Vector3.one);
                Gizmos.DrawFrustum(Vector3.zero, currentCamera.fieldOfView, currentCamera.farClipPlane, currentCamera.nearClipPlane, currentCamera.aspect);
                Gizmos.matrix = stMatrix;

            }

        }

    }

}
