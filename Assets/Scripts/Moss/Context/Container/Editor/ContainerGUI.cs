using UnityEditor;
using UnityEngine;

namespace Moss
{
    public class ContainerGUI : EditorWindow
    {
        [MenuItem("Moss/Container")]
        static void Open()
        {
            GetWindow<ContainerGUI>("Container").Show();
        }

        private Vector2 _scrollRoot;
        private bool _systemFoldOut;
        private bool _serviceFoldOut;
        private bool _stateFoldOut;
        private bool _coStateFoldOut;
        private static Container _container;

        private void OnGUI()
        {
            if (Application.isPlaying)
                _OnPlayingGUI();
            else
                _OnNoPlayingGUI();
        }

        private void _OnPlayingGUI()
        {
            _container = Game.Instance.Context.Container;

            _scrollRoot = EditorGUILayout.BeginScrollView(_scrollRoot);
            {
                _FoldOutSystemList();
                _FoldOutServiceList();
                _FoldOutStateList();
                _FoldOutCoStateList();
            }
            EditorGUILayout.EndScrollView();
        }

        private void _FoldOutSystemList()
        {
            _systemFoldOut = EditorGUILayout.BeginFoldoutHeaderGroup(_systemFoldOut, "System");
            {
                if (_systemFoldOut)
                {
                    foreach (var (bindIdentifier, system) in _container.SystemBindings)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        {
                            GUILayout.Label(bindIdentifier.ToString(), new GUIStyle("box") { fixedWidth = 500 });
                            GUILayout.Space(10);
                            GUILayout.Label(system.ToString());
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void _FoldOutServiceList()
        {
            _serviceFoldOut = EditorGUILayout.BeginFoldoutHeaderGroup(_serviceFoldOut, "Service");
            {
                if (_serviceFoldOut)
                {
                    foreach (var (bindIdentifier, system) in _container.ServiceBindings)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        {
                            GUILayout.Label(bindIdentifier.ToString(), new GUIStyle("box") { fixedWidth = 500 });
                            GUILayout.Space(10);
                            GUILayout.Label(system.ToString());
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void _FoldOutStateList()
        {
            _stateFoldOut = EditorGUILayout.BeginFoldoutHeaderGroup(_stateFoldOut, "State");
            {
                if (_stateFoldOut)
                {
                    foreach (var (bindIdentifier, system) in _container.StateBindings)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        {
                            GUILayout.Label(bindIdentifier.ToString(), new GUIStyle("box") { fixedWidth = 500 });
                            GUILayout.Space(10);
                            GUILayout.Label(system.ToString());
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void _FoldOutCoStateList()
        {
            _coStateFoldOut = EditorGUILayout.BeginFoldoutHeaderGroup(_coStateFoldOut, "CoState");
            {
                if (_coStateFoldOut)
                {
                    foreach (var (bindIdentifier, type) in _container.CoStateBindings)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        {
                            GUILayout.Label(bindIdentifier.ToString(), new GUIStyle("box") { fixedWidth = 500 });
                            GUILayout.Space(10);
                            GUILayout.Label(type.ToString());
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }


        private void _OnNoPlayingGUI()
        {
            var guiStyle = new GUIStyle("box")
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter
            };
            GUILayout.Box("Only Show When Playing", guiStyle);
        }
    }
}