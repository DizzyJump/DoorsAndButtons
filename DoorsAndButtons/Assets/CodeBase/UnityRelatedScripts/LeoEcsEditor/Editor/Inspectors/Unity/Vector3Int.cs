// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.LeoEcsEditor.Editor.Inspectors.Unity {
    sealed class Vector3IntInspector : EcsComponentInspectorTyped<Vector3Int> {
        public override bool OnGuiTyped (string label, ref Vector3Int value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.Vector3IntField (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}