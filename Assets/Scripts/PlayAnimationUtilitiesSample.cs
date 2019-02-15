using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;


[RequireComponent(typeof(Animator))]
public class PlayAnimationUtilitiesSample : MonoBehaviour
{
    public AnimationClip clip;

    PlayableGraph playableGraph;

    void Start()
    {
        AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out playableGraph);
    }

    void OnDisable()
    {
        // Destroys all Playables and Outputs created by the graph.

        playableGraph.Destroy();
    }
}