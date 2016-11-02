﻿//using System.Collections.Generic;
//using System;

//namespace BehaviorEngine
//{
//    public class Sequence : ANodeComposite
//    {
//        private int index = 0;


//        public override void Update()
//        {
//            for (; index < Children.Count; index++)
//            {
//                if (Children[index].Status != NodeState.Active) Children[index].Start();

//                Children[index].Update();
//                Status = Children[index].Status;

//                if (Status == NodeState.Successful) Children[index].End();
//                else
//                {
//                    if (Status == NodeState.Failure || Status == NodeState.Error)
//                    {
//                        EndNode();
//                    }
//                    break;
//                }
//            }
//        }

//        public override void StartRoutine()
//        {
//            base.Start();
//            index = 0;
//        }

//        public override void EndNode()
//        {
//            base.EndNode();

//            foreach (var c in Children)
//            {
//                if(c.Status == NodeState.Active) c.End();
//            }

//            index = 0;
//        }
//    }
//}
