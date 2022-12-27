// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class GradientInspector : EcsComponentInspectorTyped<Gradient> {
        public override bool OnGuiTyped (string label, ref Gradient value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.GradientField (label, value);
            if (newValue.Equals (value)) { return false; }
            value = newValue;
            return true;
        }
    }
}