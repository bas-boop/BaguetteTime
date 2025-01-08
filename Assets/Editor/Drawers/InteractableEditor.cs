using UnityEditor;

using Framework.Gameplay;

namespace Editor.Drawers
{
    [CustomEditor(typeof(Interactable), true)]
    public class InteractableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            Interactable interactable = (Interactable) target;
            SerializedObject serializedObject = new (interactable);

            serializedObject.Update();

            SerializedProperty property = serializedObject.GetIterator();
            bool enterChildren = true;
            
            while (property.NextVisible(enterChildren))
            {
                if (property.name != "onEnter" 
                    && property.name != "onExit") 
                    EditorGUILayout.PropertyField(property, true);
                
                enterChildren = false;
            }
            
            SerializedProperty onEnterProperty = serializedObject.FindProperty("onEnter");
            SerializedProperty onExitProperty = serializedObject.FindProperty("onExit");

            if (onEnterProperty != null)
                EditorGUILayout.PropertyField(onEnterProperty, true);

            if (onExitProperty != null)
                EditorGUILayout.PropertyField(onExitProperty, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}