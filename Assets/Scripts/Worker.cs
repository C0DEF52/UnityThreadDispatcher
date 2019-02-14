using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Fiftytwo
{
    public class Worker : MonoBehaviour
    {
        private void Awake ()
        {

        }

        private void Start ()
        {
            Debug.LogFormat( "[{0}] Run",
                Thread.CurrentThread.ManagedThreadId );

            //Task task = null;
            var task = new Task( DoWork );
            task.Start();
        }

        private void DoWork ()
        {
            for( int i = 0; i < 10; ++i )
            {
                Thread.Sleep( 500 );
                Debug.LogFormat( "[{0}] DoWork {1}",
                    Thread.CurrentThread.ManagedThreadId, i );
            }
        }
    }
}
