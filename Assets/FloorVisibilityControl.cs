using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorVisibilityControl : MonoBehaviour
{

    public GameObject player;
    FloorVisibilityControl playerFloor;
    public enum Floor {GROUND, FIRST};
    public Floor floor;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerFloor = player.gameObject.GetComponent<FloorVisibilityControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(floor == Floor.GROUND)
        {
            if (skinnedMeshRenderer != null)
            {
                skinnedMeshRenderer.enabled = true;
            }
        }

        if(player != null)
        {
            if (floor != playerFloor.floor)
            {
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.enabled = false;
                }
                else if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
            else
            {
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.enabled = true;
                }
                else if (meshRenderer != null)
                {
                    meshRenderer.enabled = true;
                }
            }
        }

    }
}
