using System;
using System.Collections.Generic;
using System.Linq;
using GeneralRed512.BehaviorTreeTool;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;

    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits>
    {
    }

    private BehaviorTree _behaviorTree;

    public BehaviorTreeView()
    {
        Insert(0, new GridBackground());
    
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.generalred512.behaviortreetool/Editor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }

    private NodeView FindNodeView(GeneralRed512.BehaviorTreeTool.Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    internal void PopulateView(BehaviorTree tree)
    {
        _behaviorTree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (_behaviorTree.root == null)
        {
            _behaviorTree.root = _behaviorTree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(_behaviorTree);
            AssetDatabase.SaveAssets();
        }
        
        // Create node views
        _behaviorTree.nodes.ForEach(CreateNodeView);
        
        // Create edges
        _behaviorTree.nodes.ForEach(n =>
        {
            var children = _behaviorTree.GetChildren(n);
            children.ForEach(c =>
            {
                var parentView = FindNodeView(n);
                var childView = FindNodeView(c);

                var edge = parentView.Output.ConnectTo(childView.Input);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                if (elem is NodeView nodeView)
                {
                    _behaviorTree.DeleteNode(nodeView.Node);
                }
                
                if (elem is Edge edge)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _behaviorTree.RemoveChild(parentView?.Node, childView?.Node);
                }
            });
            EditorUtility.SetDirty(_behaviorTree);
            AssetDatabase.SaveAssets();
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _behaviorTree.AddChild(parentView?.Node, childView?.Node);
            });
            EditorUtility.SetDirty(_behaviorTree);
            AssetDatabase.SaveAssets();
        }

        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // base.BuildContextualMenu(evt);
        // Calculate position of cursor in view coordinates
        var position = (evt.currentTarget as VisualElement).ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", _ => CreateNode(type, position));
            }
        }
        
        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", _ => CreateNode(type, position));
            }
        }
        
        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", _ => CreateNode(type, position));
            }
        }
    }

    private void CreateNodeView(GeneralRed512.BehaviorTreeTool.Node node)
    {
        var nodeView = new NodeView(node)
        {
            OnNodeSelected = OnNodeSelected
        };

        AddElement(nodeView);
    }

    private void CreateNode(Type type, Vector2 position)
    {
        var node = _behaviorTree.CreateNode(type);
        node.position = position;
        CreateNodeView(node);
        EditorUtility.SetDirty(_behaviorTree);
        AssetDatabase.SaveAssets();
    }
}