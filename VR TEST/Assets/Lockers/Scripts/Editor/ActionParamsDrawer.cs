using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using ActID = LockerInteractor.ActionID;

[CustomPropertyDrawer(typeof(LockerInteractor.ActionParams))]
public sealed class ActionParamsDrawer : PropertyDrawer {

    const float propHeight = 16f;

    static readonly Dictionary<string, HashSet<ActID>> propMap = new Dictionary<string, HashSet<ActID>>() {

        {"symbol", new HashSet<ActID>{ ActID.SetSymbol, ActID.AddSymbol }},
        {"symbolIndex", new HashSet<ActID>{ ActID.SetSymbol }},
        {"useEnteredSequence", new HashSet<ActID>{ ActID.ReassignControlSequence }},
        {"newSequence", new HashSet<ActID>{ ActID.ReassignControlSequence }}

    };

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

        float h = propHeight;
        ActID id = (ActID)property.FindPropertyRelative("id").enumValueIndex;

        if (property.isExpanded) {

            h += propHeight;

            if (id != ActID.None) {

                foreach (KeyValuePair<string, HashSet<ActID>> p in propMap) {

                    if (p.Value.Contains(id)) {

                        h += EditorGUI.GetPropertyHeight(property.FindPropertyRelative(p.Key));

                    }

                }

                h += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("sound"));

            }

            h += 4f;

        }

        return h;

    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        EditorGUI.BeginProperty(position, label, property);

        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, propHeight), property.isExpanded, label);

        if (property.isExpanded) {

            Rect curPos = position;
            curPos.y += propHeight;

            SerializedProperty idProp = property.FindPropertyRelative("id");
            ActID curID = (ActID)idProp.enumValueIndex;
            EditorGUI.PropertyField(new Rect(curPos.x, curPos.y, curPos.width, propHeight), idProp);

            if (curID != ActID.None) {

                curPos.y += propHeight;

                foreach (KeyValuePair<string, HashSet<ActID>> p in propMap) {

                    if (p.Value.Contains(curID)) {

                        SerializedProperty prop = property.FindPropertyRelative(p.Key);

                        if (prop != null) {

                            EditorGUI.PropertyField(new Rect(curPos.x, curPos.y, curPos.width, propHeight), prop);
                            curPos.y += propHeight;

                        }

                    }

                }

                EditorGUI.PropertyField(new Rect(curPos.x, curPos.y, curPos.width, propHeight), property.FindPropertyRelative("sound"));

            }

        }

        EditorGUI.EndProperty();

    }

}
