using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class ScriptableObjectFactoryWindow : EditorWindow
    {
        private MonoScript _scriptable;

        [MenuItem("Window/ScriptableObjects/Factory")]
        private static void OpenWindow()
        {
            ScriptableObjectFactoryWindow window = GetWindow<ScriptableObjectFactoryWindow>();
            window.titleContent.text = "Factory";
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Drag a ScriptableObject to create an instance of it");

            EditorGUILayout.Space();

            _scriptable = EditorGUILayout.ObjectField(_scriptable, typeof(MonoScript), true) as MonoScript;

            EditorGUILayout.Space();

            if (_scriptable == null || _scriptable.GetClass() == null || !_scriptable.GetClass().BaseType.Equals(typeof(ScriptableObject)))
            {
                _scriptable = null;
                EditorGUILayout.LabelField("You need a subclass of a ScriptableObject");
            }
            else if (GUILayout.Button("Create Instance") && _scriptable != null)
            {
                ScriptableObjectUtility.CreateAsset(_scriptable.GetClass());
            }

            EditorGUILayout.Space();

            EditorGUILayout.EndVertical();
        }
    } 
}