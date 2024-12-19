#if UNITY_EDITOR
using UnityEditor;

namespace Editor.Drawers
{
    [CustomEditor(typeof(Framework.Timer))]
    public sealed class TimerEditor : UnityEditor.Editor
    {
        private SerializedProperty _isCountingUp;
        private SerializedProperty _startingTime;
        private SerializedProperty _timerThreshold;
        private SerializedProperty _canCount;
        private SerializedProperty _canCountOnStart;
        private SerializedProperty _onTimerDone;
        private SerializedProperty _onTimerPassedThreshold;
        private SerializedProperty _onStart;
        private SerializedProperty _onReset;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Set up", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_isCountingUp);
            EditorGUILayout.PropertyField(_canCountOnStart);

            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_isCountingUp.boolValue ? _timerThreshold : _startingTime);

            EditorGUILayout.PropertyField(_canCount);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_onStart);
            EditorGUILayout.PropertyField(_onReset);
            EditorGUILayout.PropertyField(_onTimerPassedThreshold);
            EditorGUILayout.PropertyField(_onTimerDone);
            // EditorGUILayout.PropertyField(_isCountingUp.boolValue ? _onTimerPassedThreshold : _onTimerDone);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _isCountingUp = serializedObject.FindProperty("isCountingUp");
            _startingTime = serializedObject.FindProperty("startingTime");
            _timerThreshold = serializedObject.FindProperty("timerThreshold");
            _canCount = serializedObject.FindProperty("canCount");
            _canCountOnStart = serializedObject.FindProperty("canCountOnStart");
            _onStart = serializedObject.FindProperty("onStart");
            _onReset = serializedObject.FindProperty("onReset");
            _onTimerDone = serializedObject.FindProperty("onTimerDone");
            _onTimerPassedThreshold = serializedObject.FindProperty("onTimerPassedThreshold");
        }
    }
}
#endif
