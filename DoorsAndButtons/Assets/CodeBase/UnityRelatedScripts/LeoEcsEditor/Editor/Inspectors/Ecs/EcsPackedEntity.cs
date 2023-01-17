// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using CodeBase.GameLogic.LeoEcs;
using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.LeoEcsEditor.Editor.Inspectors.Ecs {
    sealed class EcsPackedEntityInspector : EcsComponentInspectorTyped<EcsPackedEntity> {
        public override bool OnGuiTyped (string label, ref EcsPackedEntity value, EcsEntityDebugView entityView) {
            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.PrefixLabel (label);
            if (value.Unpack (entityView.World, out var unpackedEntity)) {
                if (GUILayout.Button ("Ping entity")) {
                    EditorGUIUtility.PingObject (entityView.DebugSystem.GetEntityView (unpackedEntity));
                }
            } else {
                if (value.EqualsTo (default)) {
                    EditorGUILayout.SelectableLabel ("<Empty entity>", GUILayout.MaxHeight (EditorGUIUtility.singleLineHeight));
                } else {
                    EditorGUILayout.SelectableLabel ("<Invalid entity>", GUILayout.MaxHeight (EditorGUIUtility.singleLineHeight));
                }
            }
            EditorGUILayout.EndHorizontal ();
            return false;
        }
    }
}