using System;
using GeneralRed512.BehaviorTreeTool;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    
    public GeneralRed512.BehaviorTreeTool.Node Node;
    public Port Input;
    public Port Output;

    public NodeView(GeneralRed512.BehaviorTreeTool.Node node)
    {
        Node = node;
        title = node.name;
        viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;
        style.position = Position.Absolute;
        
        CreateInputPorts();
        CreateOutputPorts();
    }

    private void CreateInputPorts()
    {
        switch (Node)
        {
            case ActionNode:
            case CompositeNode:
            case DecoratorNode:
                Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                break;
        }

        if (Input == null) return;
        
        Input.portName = "";
        inputContainer.Add(Input);
    }

    private void CreateOutputPorts()
    {
        switch (Node)
        {
            case ActionNode:
                break;
            case CompositeNode:
                Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode:
            case RootNode:
                Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (Output == null) return;
        
        Output.portName = "";
        outputContainer.Add(Output);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        Node.position.x = newPos.xMin;
        Node.position.y = newPos.yMin;
    }

    public override void OnSelected()
    {
        base.OnSelected();

        OnNodeSelected?.Invoke(this);
    }

    public sealed override string title
    {
        get { return base.title; }
        set { base.title = value; }
    }
}