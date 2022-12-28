// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.LeoEcsEditor.Editor.Inspectors.Unity {
    sealed class BoundsInspector : EcsComponentInspectorTyped<Bounds> {
        public override bool OnGuiTyped (string label, ref Bounds value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.BoundsField (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}