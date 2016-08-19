using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Locker : MonoBehaviour {

    [System.Serializable]
    public sealed class String2ChangedEvent : UnityEvent<string, string> { }

    [System.Serializable]
    public sealed class String1ChangedEvent : UnityEvent<string> { }

    [System.Serializable]
    public sealed class BooleanChangedEvent : UnityEvent<bool> { }

    const char whiteSpace = ' ';

    public static bool IsSequenceEmpty(string sequence) {

        return string.IsNullOrEmpty(sequence) || sequence.Trim().Length == 0;

    }

    public string controlSequence {

        get {

            return _controlSequence;

        }

    }

    public string currentSequence {

        get {

            return _currentSequence;

        }
        protected set {

            if (_currentSequence != value) {

                _onCurrentSequenceChanged.Invoke(_currentSequence, value);
                _currentSequence = value;

                if (tryToUnlockOnSeqChanged) {

                    TryToUnlock();

                }

                if (IsCurrentSequenceFilled()) {

                    _onCurrentSequenceFilled.Invoke(_currentSequence);

                    if (autoClearOnSeqFilled) {

                        ClearCurrentSequence();

                    }

                }

            }

        }

    }

    public bool isLocked {

        get {

            return _isLocked;

        }
        protected set {

            if (_isLocked != value) {

                SetLocked(value);

            }

        }

    }

    public bool isOpened {

        get {

            return _isOpened;

        }
        protected set {

            if (_isOpened != value) {

                SetOpened(value);

            }

        }

    }

    public AudioSource audioSource {
        get { return _audioSource; }
    }

    public String2ChangedEvent onCorrectSequenceChanged {
        get { return _onControlSequenceChanged; }
    }

    public String2ChangedEvent onCurrentSequenceChanged {
        get { return _onCurrentSequenceChanged; }
    }

    public BooleanChangedEvent onLockStateChanged {
        get { return _onLockStateChanged; }
    }

    public BooleanChangedEvent onOpenStateChanged {
        get { return _onOpenStateChanged; }
    }

    [SerializeField]
    string _controlSequence = "12345";

    [Space(4f)]
    [SerializeField]
    bool tryToUnlockOnSeqChanged = true;

    [SerializeField]
    bool autoClearOnSeqFilled;

    [SerializeField]
    bool autoClearOnLockChanged;

    [SerializeField]
    bool autoClearOnUnlockAttempt;

    [SerializeField]
    bool _isOpened;

    [Space(4f)]
    [SerializeField]
    Animator animator;

    [SerializeField]
    bool useAnimator = true;

    [SerializeField]
    string animatorOpenedTrigger = "isOpened";

    [Space(4f)]
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField, Range(0f, 1f)]
    float sfxVolumeScale = 1f;

    [SerializeField]
    AudioClip lockSound;

    [SerializeField]
    AudioClip unlockSound;

    [SerializeField]
    AudioClip tryToOpenSound;

    [SerializeField]
    AudioClip openSound;

    [SerializeField]
    AudioClip closeSound;

    [SerializeField]
    AudioClip sequenceClearSound;

    [SerializeField]
    AudioClip wrongSequenceSound;

    [Space(4f)]
    [SerializeField]
    String2ChangedEvent _onControlSequenceChanged;

    [SerializeField]
    String2ChangedEvent _onCurrentSequenceChanged;

    [SerializeField]
    String1ChangedEvent _onCurrentSequenceFilled;

    [SerializeField]
    BooleanChangedEvent _onLockStateChanged;

    [SerializeField]
    BooleanChangedEvent _onOpenStateChanged;

    int sequenceLength;
    string _currentSequence;
    bool _isLocked;
    bool isInitialized;

    void SetLocked(bool state) {

        _isLocked = state;

        if (autoClearOnLockChanged) {

            ClearCurrentSequence();

        }

        if (_isLocked) {

            PlaySound(lockSound);

        }
        else {

            PlaySound(unlockSound);

        }

        _onLockStateChanged.Invoke(_isLocked);

    }

    void SetOpened(bool state) {

        _isOpened = state;

        if (_isOpened) {

            isLocked = false;
            PlaySound(openSound);

        }
        else {

            PlaySound(closeSound);

        }

        if (animator != null && !string.IsNullOrEmpty(animatorOpenedTrigger) && animator.isActiveAndEnabled) {

            animator.SetBool(animatorOpenedTrigger, _isOpened);

        }

        _onOpenStateChanged.Invoke(_isOpened);


    }

    protected void SetControlSequence(string newSequence, bool ingoreEquality = false) {

        if (string.IsNullOrEmpty(newSequence)) {

            newSequence = " ";

        }

        newSequence = newSequence.Length <= sequenceLength ? newSequence.PadRight(sequenceLength) : newSequence.Substring(0, sequenceLength);

        if (_controlSequence != newSequence || ingoreEquality) {

            _onControlSequenceChanged.Invoke(_controlSequence, newSequence);
            _controlSequence = newSequence;
            ClearCurrentSequence();
            isLocked = _currentSequence != _controlSequence;

        }

    }

    protected bool IsCurrentSequenceFilled() {

        foreach (char c in _currentSequence) {

            if (char.IsWhiteSpace(c)) {

                return false;

            }

        }

        return true;

    }

    protected void TryToUnlock() {

        if (!_isOpened) {

            if (_currentSequence == _controlSequence) {

                isLocked = false;

            }

        }

        if (isLocked) {

            PlaySound(wrongSequenceSound);

        }

    }

    public void ClearCurrentSequence() {

        currentSequence = new string(whiteSpace, sequenceLength);
        PlaySound(sequenceClearSound);

    }

    public void Lock(string newSequence = null) {

        if (!_isOpened) {

            if (newSequence != null && !_isLocked) {

                SetControlSequence(newSequence);

            }
            else if (!IsSequenceEmpty(_controlSequence)) {

                isLocked = true;

            }

        }

    }

    public void LockWithEnteredSequence() {

        Lock(_currentSequence);

    }

    public void Unlock() {

        TryToUnlock();

        if (autoClearOnUnlockAttempt) {

            ClearCurrentSequence();

        }

    }

    public void SwitchOpened() {

        if (_isOpened) {

            Close();

        }
        else {

            Open();

        }

    }

    public void SetSymbol(char symbol, int position) {

        if (position >= 0 && position < sequenceLength) {

            char[] curSymbols = _currentSequence.ToCharArray();
            curSymbols[position] = symbol;
            currentSequence = new string(curSymbols);

        }

    }

    public void RemoveSymbol(int position) {

        SetSymbol(whiteSpace, position);

    }

    public void AddSymbol(char symbol) {

        if (!IsCurrentSequenceFilled()) {

            SetSymbol(symbol, _currentSequence.IndexOf(whiteSpace));

        }

    }

    public void RemoveLastSymbol() {

        RemoveSymbol(sequenceLength - 1);

    }

    public void Close() {

        isOpened = false;

    }

    public void Open() {

        if (!_isLocked) {

            isOpened = true;

        }
        else {

            PlaySound(tryToOpenSound);

        }

    }

    public void PlaySound(AudioClip sound) {

        if (isInitialized && _audioSource != null && sound != null) {

            _audioSource.PlayOneShot(sound, sfxVolumeScale);

        }

    }

    public override string ToString() {

        string res = string.Empty;
        string nl = System.Environment.NewLine;

        res += gameObject.name + nl;
        res += "Control Sequence: " + _controlSequence.ToString() + nl;
        res += "Current Sequence: " + _currentSequence.ToString() + nl;
        res += "Is Locked: " + _isLocked.ToString() + nl;
        res += "Is Opened: " + _isOpened.ToString();

        return res;

    }

    protected virtual void Reset() {

        if (_audioSource == null) {

            _audioSource = gameObject.AddComponent<AudioSource>();

        }

    }

    protected virtual void Awake() {

        if (useAnimator) {

            if (animator == null) {

                animator = GetComponent<Animator>();

            }

        }
        else {

            animator = null;

        }

        if (_audioSource == null) {

            _audioSource = GetComponent<AudioSource>();

        }

    }

    protected virtual void Start() {

        if (IsSequenceEmpty(_controlSequence)) {

            Debug.LogWarning(gameObject.name + " has empty control sequence!");

        }

        sequenceLength = _controlSequence.Length;
        SetControlSequence(_controlSequence, true);
        SetOpened(_isOpened);

        isInitialized = true;

    }
 
}
