using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using System.Threading.Tasks;

[RequireComponent(typeof(Animator))]
public class PlayAnimationSample : MonoBehaviour
{
    public AnimationClip clip;

    PlayableGraph playableGraph;

    void Start()
    {
        playableGraph = PlayableGraph.Create();

        playableGraph.SetTimeUpdateMode( DirectorUpdateMode.GameTime );

        var playableOutput = AnimationPlayableOutput.Create( playableGraph, "Animation", GetComponent<Animator>() );

        // Wrap the clip in a playable
        var clipPlayable = AnimationClipPlayable.Create( playableGraph, clip );

        // Connect the Playable to an output
        playableOutput.SetSourcePlayable( clipPlayable );

        var task = Task.Run( () =>
        {
            //playableGraph.Play();
            Debug.Log( "RUN" );
        } );

        // Plays the Graph.
        //playableGraph.Play();
    }

    void OnDisable()
    {
        // Destroys all Playables and PlayableOutputs created by the graph.
        playableGraph.Destroy();
    }
}
