using UnityEngine;
using UnityEngine.Playables;


public class DestroyThisGameObject : MonoBehaviour
{
    private PlayableGraph playableGraph;

    public void Start()
    {
        if (playableGraph.IsValid())
        {
            playableGraph.Destroy();
        }

        Destroy(gameObject);
    }
}