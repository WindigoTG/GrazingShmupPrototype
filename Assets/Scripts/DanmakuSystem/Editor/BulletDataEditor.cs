#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace GrazingShmup
{
    [CustomEditor(typeof(BulletData))]
    public class BulletDataEditor : Editor
    {
        BulletData _bulletData;
        string _baseBulletName;
        AnimBool _showBulletSettings = new AnimBool();
        AnimBool _showHomingLaserSettings = new AnimBool();
        AnimBool _showWeaponTracking = new AnimBool();
        AnimBool _showLineSettings = new AnimBool();
        AnimBool _showArcSettings = new AnimBool();
        AnimBool _showRowSettings = new AnimBool();
        AnimBool _showCapsuleSettings = new AnimBool();
        AnimBool _showRepeaterCapsuleSettings = new AnimBool();
        AnimBool _showBurstCapsuleSettings = new AnimBool();
        AnimBool _showSpinningCapsuleSettings = new AnimBool();
        bool _showSpeedSettings;
        bool _showLineFoldout;
        bool _showArcFoldout;
        bool _showRowFoldout;
        bool _showCapsuleFoldout;
        bool _showRepeaterCapsuleFoldout;
        bool _showBurstCapsuleFoldout;
        bool _showSpinningCapsuleFoldout;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _bulletData = serializedObject.targetObject as BulletData;

            DrawFireDelaySetting();

            DrawOwnerAndTrackingSettings();

            DrawBulletTypeSelection();

            DrawBaseBulletSettings();

            DrawComponentsArray();

            DrawLineSettings();

            DrawArcSettings();

            DrawRowSettings();

            DrawCapsuleSettings();

            DrawRepeaterCapsuleSettings();

            DrawBurstCapsuleSettings();

            DrawSpinningCapsuleSettings();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawFireDelaySetting()
        {
            _bulletData.BulletConfig.FireDelay = EditorGUILayout.DelayedFloatField("Delay between shots", _bulletData.BulletConfig.FireDelay);

            EditorGUILayout.Space();
        }

        private void DrawOwnerAndTrackingSettings()
        {
            _bulletData.BulletOwner = (BulletOwner)EditorGUILayout.EnumPopup("Weapon owner", _bulletData.BulletOwner);
            _showWeaponTracking.target = _bulletData.BulletOwner == BulletOwner.Enemy;

            if (EditorGUILayout.BeginFadeGroup(_showWeaponTracking.faded))
            {
                EditorGUI.indentLevel = 1;
                _bulletData.IsTracking = EditorGUILayout.Toggle("Track target", _bulletData.IsTracking);
            }

            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel = 0;
            EditorGUILayout.Space();
        }

        private void DrawBulletTypeSelection()
        {
            _bulletData.BaseBullet = (BulletBase)EditorGUILayout.EnumPopup("Base bullet", _bulletData.BaseBullet);

            EditorGUI.indentLevel = 1;

            _showBulletSettings.target = _bulletData.BaseBullet == BulletBase.Bullet;
            _showHomingLaserSettings.target = _bulletData.BaseBullet == BulletBase.HomingLaser;

            DrawRegularBulletSettings();

            DrawHomingLaserSettings();

            EditorGUI.indentLevel = 0;
            EditorGUILayout.Space();
        }

        private void DrawRegularBulletSettings()
        {
            if (EditorGUILayout.BeginFadeGroup(_showBulletSettings.faded))
            {
                _baseBulletName = "Bullet";
                _bulletData.BulletConfig.BulletPrefab =
                    (GameObject)EditorGUILayout.ObjectField($"{_baseBulletName} prefab", _bulletData.BulletConfig.BulletPrefab, typeof(GameObject), false);
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawHomingLaserSettings()
        {
            if (EditorGUILayout.BeginFadeGroup(_showHomingLaserSettings.faded))
            {
                _baseBulletName = "Homing laser";
                _bulletData.BulletConfig.HomingLaserPrefab =
                    (GameObject)EditorGUILayout.ObjectField($"{_baseBulletName} prefab", _bulletData.BulletConfig.HomingLaserPrefab, typeof(GameObject), false);

                _bulletData.BulletConfig.HomingTime =
                    EditorGUILayout.DelayedFloatField("Homing time", _bulletData.BulletConfig.HomingTime);
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawBaseBulletSettings()
        {
            _bulletData.BulletConfig.BulletLifeTime = EditorGUILayout.DelayedFloatField($"{_baseBulletName} life time", _bulletData.BulletConfig.BulletLifeTime);
            _bulletData.BulletConfig.ShouldLiveOffscreen =
                        EditorGUILayout.Toggle("Should live offscreen", _bulletData.BulletConfig.ShouldLiveOffscreen);


            EditorGUILayout.Space();

            _showSpeedSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showSpeedSettings, $"{_baseBulletName} speed settings");

            if (_showSpeedSettings)
            {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.LabelField("Move speed");

                EditorGUI.indentLevel = 2;

                _bulletData.BulletConfig.BulletSpeed = EditorGUILayout.DelayedFloatField("Initial speed", _bulletData.BulletConfig.BulletSpeed);
                _bulletData.BulletConfig.BulletDeltaSpeed = EditorGUILayout.DelayedFloatField("Speed change per second", _bulletData.BulletConfig.BulletDeltaSpeed);
                _bulletData.BulletConfig.BulletDeltaSpeedDelay = EditorGUILayout.DelayedFloatField("Delay before speed change", _bulletData.BulletConfig.BulletDeltaSpeedDelay);

                EditorGUILayout.Space();
                EditorGUI.indentLevel = 1;

                _bulletData.BulletConfig.BulletTurnSpeed = EditorGUILayout.DelayedFloatField("Turn speed", _bulletData.BulletConfig.BulletTurnSpeed);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUI.indentLevel = 0;
            EditorGUILayout.Space();
        }

        private void DrawComponentsArray()
        {
            SerializedProperty componentsArray = serializedObject.FindProperty("BulletComponents");
            EditorGUILayout.PropertyField(componentsArray, new GUIContent("Bullet components"), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
        }

        private void DrawLineSettings()
        {
            _showLineSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.Line);

            if (EditorGUILayout.BeginFadeGroup(_showLineSettings.faded))
            {
                
                _showLineFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showLineFoldout, "Line settings");

                if (_showLineFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.LineSettings.LineBulletCount =
                        EditorGUILayout.DelayedIntField("Number of bullets in line", _bulletData.BulletConfig.LineSettings.LineBulletCount);
                    _bulletData.BulletConfig.LineSettings.DeltaSpeedInLine =
                        EditorGUILayout.DelayedFloatField("Delta speed in line", _bulletData.BulletConfig.LineSettings.DeltaSpeedInLine);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawArcSettings()
        {
            _showArcSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.Arc);

            if (EditorGUILayout.BeginFadeGroup(_showArcSettings.faded))
            {
                _showArcFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showArcFoldout, "Arc settings");

                if (_showArcFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.ArcSettings.ArcBulletCount =
                        EditorGUILayout.DelayedIntField("Number of bullets in arc", _bulletData.BulletConfig.ArcSettings.ArcBulletCount);
                    _bulletData.BulletConfig.ArcSettings.ArcAngle =
                        EditorGUILayout.DelayedFloatField("Arc angle", _bulletData.BulletConfig.ArcSettings.ArcAngle);
                    _bulletData.BulletConfig.ArcSettings.InitialRadius =
                        EditorGUILayout.DelayedFloatField("Initial radius", _bulletData.BulletConfig.ArcSettings.InitialRadius);
                    _bulletData.BulletConfig.ArcSettings.SymmetrizeTurning =
                        EditorGUILayout.Toggle("Symmetrize turning", _bulletData.BulletConfig.ArcSettings.SymmetrizeTurning);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawRowSettings()
        {
            _showRowSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.Row);

            if (EditorGUILayout.BeginFadeGroup(_showRowSettings.faded))
            {
                _showRowFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showRowFoldout, "Row settings");

                if(_showRowFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.RowSettings.RowAdditionalBulletCount =
                        EditorGUILayout.DelayedIntField("Number of additional bullets", _bulletData.BulletConfig.RowSettings.RowAdditionalBulletCount);
                    _bulletData.BulletConfig.RowSettings.RowHorizontalOffset =
                        EditorGUILayout.DelayedFloatField("Horizontal offset", _bulletData.BulletConfig.RowSettings.RowHorizontalOffset);
                    _bulletData.BulletConfig.RowSettings.RowVerticalOffset =
                        EditorGUILayout.DelayedFloatField("Horizontal offset", _bulletData.BulletConfig.RowSettings.RowVerticalOffset);

                    _bulletData.BulletConfig.RowSettings.IsTwoSided = 
                        EditorGUILayout.BeginToggleGroup("Two-sided", _bulletData.BulletConfig.RowSettings.IsTwoSided);

                    EditorGUI.indentLevel = 2;

                    _bulletData.BulletConfig.RowSettings.IsMirrored =
                        EditorGUILayout.BeginToggleGroup("Mirrired", _bulletData.BulletConfig.RowSettings.IsMirrored);

                    EditorGUI.indentLevel = 3;

                    _bulletData.BulletConfig.RowSettings.RowGap =
                        EditorGUILayout.FloatField("Gap between sides", _bulletData.BulletConfig.RowSettings.RowGap);

                    EditorGUILayout.EndToggleGroup();
                    EditorGUILayout.EndToggleGroup();
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawCapsuleSettings()
        {
            _showCapsuleSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.Capsule);

            if (EditorGUILayout.BeginFadeGroup(_showCapsuleSettings.faded))
            {
                _showCapsuleFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showCapsuleFoldout, "Capsule settings");

                if(_showCapsuleFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.CapsuleSettings.CapsuleLifeTime =
                        EditorGUILayout.DelayedFloatField("Life time", _bulletData.BulletConfig.CapsuleSettings.CapsuleLifeTime);
                    EditorGUILayout.Space();

                    _bulletData.BulletConfig.CapsuleSettings.CapsuleSpeed =
                        EditorGUILayout.DelayedFloatField("Initial speed", _bulletData.BulletConfig.CapsuleSettings.CapsuleSpeed);
                    _bulletData.BulletConfig.CapsuleSettings.CapsuleDeltaSpeed = 
                        EditorGUILayout.DelayedFloatField("Speed change per second", _bulletData.BulletConfig.CapsuleSettings.CapsuleDeltaSpeed);
                    _bulletData.BulletConfig.CapsuleSettings.CapsuleDeltaSpeedDelay = 
                        EditorGUILayout.DelayedFloatField("Delay before speed change", _bulletData.BulletConfig.CapsuleSettings.CapsuleDeltaSpeedDelay);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.CapsuleSettings.CapsuleTurnSpeed = 
                        EditorGUILayout.DelayedFloatField("Turn speed", _bulletData.BulletConfig.CapsuleSettings.CapsuleTurnSpeed);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.CapsuleSettings.CapsulePrefab =
                    (GameObject)EditorGUILayout.ObjectField($"Capsule prefab", _bulletData.BulletConfig.CapsuleSettings.CapsulePrefab, typeof(GameObject), false);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawRepeaterCapsuleSettings()
        {
            _showRepeaterCapsuleSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.RepeaterCapsule);

            if (EditorGUILayout.BeginFadeGroup(_showRepeaterCapsuleSettings.faded))
            {
                _showRepeaterCapsuleFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showRepeaterCapsuleFoldout, "Repeater Capsule settings");

                if (_showRepeaterCapsuleFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleLifeTime =
                        EditorGUILayout.DelayedFloatField("Life time", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleLifeTime);
                    EditorGUILayout.Space();

                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleRefireTime =
                        EditorGUILayout.DelayedFloatField("Time between shots", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleRefireTime);
                    _bulletData.BulletConfig.RepeaterCapsuleSettings.SynchronizeDeltaSpeedDelayInShots =
                        EditorGUILayout.Toggle("Synchronize delta speed between shots", _bulletData.BulletConfig.RepeaterCapsuleSettings.SynchronizeDeltaSpeedDelayInShots);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleSpeed =
                        EditorGUILayout.DelayedFloatField("Initial speed", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleSpeed);
                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleDeltaSpeed =
                        EditorGUILayout.DelayedFloatField("Speed change per second", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleDeltaSpeed);
                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleDeltaSpeedDelay =
                        EditorGUILayout.DelayedFloatField("Delay before speed change", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleDeltaSpeedDelay);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleTurnSpeed =
                        EditorGUILayout.DelayedFloatField("Turn speed", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsuleTurnSpeed);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsulePrefab =
                    (GameObject)EditorGUILayout.ObjectField($"Repeater Capsule prefab", _bulletData.BulletConfig.RepeaterCapsuleSettings.RCapsulePrefab, typeof(GameObject), false);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawBurstCapsuleSettings()
        {
            _showBurstCapsuleSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.BurstCapsule);

            if (EditorGUILayout.BeginFadeGroup(_showBurstCapsuleSettings.faded))
            {
                _showBurstCapsuleFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showBurstCapsuleFoldout, "Burst Capsule settings");

                if (_showBurstCapsuleFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleLifeTime =
                        EditorGUILayout.DelayedFloatField("Life time", _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleLifeTime);
                    EditorGUILayout.Space();

                    _bulletData.BulletConfig.BurstCapsuleSettings.BurstRefireTime =
                        EditorGUILayout.DelayedFloatField("Time between shots", _bulletData.BulletConfig.BurstCapsuleSettings.BurstRefireTime);
                    _bulletData.BulletConfig.BurstCapsuleSettings.DeltaSpeedInBurst =
                        EditorGUILayout.DelayedFloatField("Delta speed between shots", _bulletData.BulletConfig.BurstCapsuleSettings.DeltaSpeedInBurst);
                    _bulletData.BulletConfig.BurstCapsuleSettings.IsTracking =
                        EditorGUILayout.Toggle("Track target", _bulletData.BulletConfig.BurstCapsuleSettings.IsTracking);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleSpeed =
                        EditorGUILayout.DelayedFloatField("Initial speed", _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleSpeed);
                    _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleDeltaSpeed =
                        EditorGUILayout.DelayedFloatField("Speed change per second", _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleDeltaSpeed);
                    _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleDeltaSpeedDelay =
                        EditorGUILayout.DelayedFloatField("Delay before speed change", _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleDeltaSpeedDelay);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleTurnSpeed =
                        EditorGUILayout.DelayedFloatField("Turn speed", _bulletData.BulletConfig.BurstCapsuleSettings.BCapsuleTurnSpeed);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.BurstCapsuleSettings.BCapsulePrefab =
                    (GameObject)EditorGUILayout.ObjectField($"Burst Capsule prefab", _bulletData.BulletConfig.BurstCapsuleSettings.BCapsulePrefab, typeof(GameObject), false);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void DrawSpinningCapsuleSettings()
        {
            _showSpinningCapsuleSettings.target = _bulletData.BulletComponents.Contains(BulletComponent.SpinningCapsule);

            if (EditorGUILayout.BeginFadeGroup(_showSpinningCapsuleSettings.faded))
            {
                _showSpinningCapsuleFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_showSpinningCapsuleFoldout, "Burst Capsule settings");

                if (_showSpinningCapsuleFoldout)
                {
                    EditorGUI.indentLevel = 1;

                    _bulletData.BulletConfig.SpinningCapsuleSettings.SpinArc =
                        EditorGUILayout.DelayedFloatField("Spin arc, degrees", _bulletData.BulletConfig.SpinningCapsuleSettings.SpinArc);
                    _bulletData.BulletConfig.SpinningCapsuleSettings.SpinSpeed =
                        EditorGUILayout.DelayedFloatField("Spin speed, degrees/second", _bulletData.BulletConfig.SpinningCapsuleSettings.SpinSpeed);
                    _bulletData.BulletConfig.SpinningCapsuleSettings.DegreesBetweenShots =
                        EditorGUILayout.DelayedFloatField("Degrees between shots", _bulletData.BulletConfig.SpinningCapsuleSettings.DegreesBetweenShots);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleSpeed =
                        EditorGUILayout.DelayedFloatField("Initial speed", _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleSpeed);
                    _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleDeltaSpeed =
                        EditorGUILayout.DelayedFloatField("Speed change per second", _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleDeltaSpeed);
                    _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleDeltaSpeedDelay =
                        EditorGUILayout.DelayedFloatField("Delay before speed change", _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleDeltaSpeedDelay);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleTurnSpeed =
                        EditorGUILayout.DelayedFloatField("Turn speed", _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsuleTurnSpeed);

                    EditorGUILayout.Space();
                    _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsulePrefab =
                    (GameObject)EditorGUILayout.ObjectField($"Spinning Capsule prefab", _bulletData.BulletConfig.SpinningCapsuleSettings.SCapsulePrefab, typeof(GameObject), false);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUI.indentLevel = 0;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void OnEnable()
        {
            //_showBulletSettings.valueChanged.AddListener(Repaint);
            //_showHomingLaserSettings.valueChanged.AddListener(Repaint);
            //_showWeaponTracking.valueChanged.AddListener(Repaint);
            //_showLineSettings.valueChanged.AddListener(Repaint);
            //_showArcSettings.valueChanged.AddListener(Repaint);
            //_showRowSettings.valueChanged.AddListener(Repaint);
            //_showCapsuleSettings.valueChanged.AddListener(Repaint);
            //_showRepeaterCapsuleSettings.valueChanged.AddListener(Repaint);
            //_showBurstCapsuleSettings.valueChanged.AddListener(Repaint);
            //_showSpinningCapsuleSettings.valueChanged.AddListener(Repaint);
        }

        private void OnDisable()
        {
            //_showBulletSettings.valueChanged.RemoveAllListeners();
            //_showHomingLaserSettings.valueChanged.RemoveAllListeners();
            //_showWeaponTracking.valueChanged.RemoveAllListeners();
            //_showLineSettings.valueChanged.RemoveAllListeners();
            //_showArcSettings.valueChanged.RemoveAllListeners();
            //_showRowSettings.valueChanged.RemoveAllListeners();
            //_showCapsuleSettings.valueChanged.RemoveAllListeners();
            //_showRepeaterCapsuleSettings.valueChanged.RemoveAllListeners();
            //_showBurstCapsuleSettings.valueChanged.RemoveAllListeners();
            //_showSpinningCapsuleSettings.valueChanged.RemoveAllListeners();
        }
    }
}
#endif