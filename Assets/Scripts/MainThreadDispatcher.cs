using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Fiftytwo
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        private Queue<Action> _actions;
        private object _lock;

        private static MainThreadDispatcher _instance;
        public static MainThreadDispatcher Instance
        {
            get
            {
                if( _instance != null )
                    return _instance;
                Touch();
                return _instance;
            }
        }

        public static void Touch ()
        {
            if( _instance != null )
                return;
            var go = new GameObject( typeof( MainThreadDispatcher ).ToString(), typeof( MainThreadDispatcher ) );
        }

        public void Dispatch( Action action )
        {
            lock( _lock )
            {
                _actions.Enqueue( action );
            }
        }

        private void Awake ()
        {
            Assert.IsNull( _instance, typeof( MainThreadDispatcher ) + " is already instantiated" );
            if( _instance != null )
            {
                Destroy( gameObject );
                return;
            }

            _instance = this;
            DontDestroyOnLoad( gameObject );

            _actions = new Queue<Action>();
            _lock = new object();
        }

        private void Update ()
        {
            Action action;

            lock( _lock )
            {
                if( _actions.Count == 0 )
                {
                    Destroy( gameObject );
                    _instance = null;
                    return;
                }
                action = _actions.Dequeue();
            }

            action();
        }
    }
}
