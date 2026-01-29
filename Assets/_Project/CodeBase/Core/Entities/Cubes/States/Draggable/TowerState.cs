using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using _Project.CodeBase.Core.Logger;
using _Project.CodeBase.Core.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Draggable
{
    public class TowerState : CubeDraggableStateBase
    {
        public TowerState(Cube owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            if (owner.Prev is not Cube prevCube)
                return;
            
            if (prevCube.IntersectsX(owner))
                return;
                
            DiscardUp(owner);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            PokeTree();
            
            owner.Fsm.SetState<FloatingState>();
        }

        private void PokeTree()
        {
            int i = 0;
            const int max = 50;
            
            INode node = owner;
            INode nextNode = owner.Next;
            
            if (node.Prev != null)
                node.Prev.Next = nextNode;
            if (nextNode != null)
                nextNode.Prev = node.Prev;
            
            while (nextNode != null && i < max)
            {
                if (node is Cube cube && nextNode is Cube nextCube)
                    nextCube.Fsm.SetState<FallingState, FallingStatePayload>(new FallingStatePayload
                    {
                        destinationY = cube.transform.position.y,
                        nextState = typeof(TowerState)
                    });
                
                node = nextNode;
                nextNode = node.Next;
                
                i++;
                if (i > max - 1)
                    Debug.LogError("Overflow");
            }
            owner.Next = null;
            owner.Prev = null;
        }

        private void DiscardUp(INode from)
        {
            actionLogger.Log(LocalizationKeys.TowerFallenKey);
            
            INode node = from;
            
            while (node != null)
            {
                if (node is Cube cube)
                    cube.Fsm.SetState<MissState>();
                
                node.Prev.Next = null;
                node = node.Next;
            }
        }
    }
}
