using UnityEditor;

[CustomEditor(typeof(Guid))]

class GuidInspector : Editor {
    void OnEnable() {
        Guid guid = (Guid) target;

        if (guid.guid == System.Guid.Empty) {
            guid.Generate();
            EditorUtility.SetDirty(target);
        }
    }

    public override void OnInspectorGUI() {
        Guid guid = (Guid) target;
        EditorGUILayout.SelectableLabel(guid.guid.ToString());
    }
}
