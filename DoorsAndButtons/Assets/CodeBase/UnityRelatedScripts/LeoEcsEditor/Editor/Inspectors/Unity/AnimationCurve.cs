// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.LeoEcsEditor.Editor.Inspectors.Unity {
    sealed class AnimationCurveInspector : EcsComponentInspectorTyped<AnimationCurve> {
        public override bool OnGuiTyped (string label, ref AnimationCurve value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.CurveField (label, value);
            if (newValue.Equals (value)) { return false; }
            value = newValue;
            return true;
        }
    }
}