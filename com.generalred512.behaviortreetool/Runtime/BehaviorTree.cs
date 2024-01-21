using System.Collections.Generic;
using UnityEngine;

#if (UNITY_EDITOR)
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace GeneralRed512.BehaviorTreeTool
{
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        public Node root;
        public Node.State state = Node.State.Running;
        [HideInInspector] public List<Node> nodes = new List<Node>();

        public Node.State TreeUpdate(Tick tick)
        {
            if (root.state == Node.State.Running)
            {
                state = root.NodeUpdate(tick);
            }

            return state;
        }
        
#if (UNITY_EDITOR)

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            if (node == null)
            {
                Debug.LogError($"Error Creating Node of type {type.Name}");
                return null;
            }
            
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                rootNode.child = child;
            }
            
            var decorator = parent as DecoratorNode;
            if (decorator != null)
            {
                decorator.child = child;
            }
            
            var composite = parent as CompositeNode;
            if (composite != null)
            {
                composite.children.Add(child);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                rootNode.child = null;
            }
            
            var decorator = parent as DecoratorNode;
            if (decorator != null)
            {
                decorator.child = null;
            }
            
            var composite = parent as CompositeNode;
            if (composite != null)
            {
                composite.children.Remove(child);
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            var children = new List<Node>();
            
            var rootNode = parent as RootNode;
            if (rootNode != null && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }
            
            var decorator = parent as DecoratorNode;
            if (decorator != null && decorator.child != null)
            {
                children.Add(decorator.child);
            }
            
            var composite = parent as CompositeNode;
            if (composite != null)
            {
                return composite.children;
            }

            return children;
        }
        
#endif

        public BehaviorTree Clone()
        {
            BehaviorTree tree = Instantiate(this);
            tree.root = tree.root.Clone();
            return tree;
        }
    }
}