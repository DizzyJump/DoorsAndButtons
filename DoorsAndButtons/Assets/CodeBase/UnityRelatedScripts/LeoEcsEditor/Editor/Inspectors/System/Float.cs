// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using UnityEditor;

namespace CodeBase.UnityRelatedScripts.LeoEcsEditor.Editor.Inspectors.System {
    sealed class FloatInspector : EcsComponentInspectorTyped<float> {
        public override bool OnGuiTyped (string label, ref float value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.FloatField (label, value);
            if (global::System.Math.Abs (newValue - value) < float.Epsilon) { return false; }
            value = newValue;
            return true;
        }
    }
}