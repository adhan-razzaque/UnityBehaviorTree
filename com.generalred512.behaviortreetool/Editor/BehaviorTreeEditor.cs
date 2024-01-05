using GeneralRed512.BehaviorTreeTool;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeEditor : EditorWindow
{
    private BehaviorTreeView _treeView;
    private InspectorView _inspectorView;
    
    [MenuItem("Window/Behavior Tree Editor", false, 50)]
    public static void OpenWindow()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("Behavior Tree Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.generalred512.behaviortreetool/Editor/BehaviorTreeEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.generalred512.behaviortreetool/Editor/BehaviorTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _treeView = root.Q<BehaviorTreeView>();
        _inspectorView = root.Q<InspectorView>();
        _treeView.OnNodeSelected = OnNodeSelectionChanged;
        
        OnSelectionChange();
    }

    public void LoadFromFile(string path)
    {
        var tree = AssetDatabase.LoadAssetAtPath<BehaviorTree>(path);
        
        if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            _treeView.PopulateView(tree);
        }
    }

    private void OnSelectionChange()
    {
        var tree = Selection.activeObject as BehaviorTree;

        if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            _treeView.PopulateView(tree);
        }
    }

    private void OnNodeSelectionChanged(NodeView node)
    {
        _inspectorView.UpdateSelection(node);
    }
}