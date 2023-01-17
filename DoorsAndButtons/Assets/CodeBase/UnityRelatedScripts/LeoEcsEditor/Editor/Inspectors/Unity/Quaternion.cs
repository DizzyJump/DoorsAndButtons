// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.LeoEcsEditor.Editor.Inspectors.Unity {
    sealed class QuaternionInspector : EcsComponentInspectorTyped<Quaternion> {
        public override bool OnGuiTyped (string label, ref Quaternion value, EcsEntityDebugView entityView) {
            var eulerAngles = value.eulerAngles;
            var newValue = EditorGUILayout.Vector3Field (label, eulerAngles);
            if (newValue == eulerAngles) { return false; }
            value = Quaternion.Euler (newValue);
            return true;
        }
    }
}