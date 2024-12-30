using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    public float fuseMax;
    private float fuseCount;
    private Explosion explosion;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private ProgressController progressController;
    // Start is called before the first frame update
    void Start()
    {
        fuseCount = 0;
        explosion = GetComponent<Explosion>();
    }

    public void UpdateFuse(){
        fuseCount++;
        if(fuseCount == fuseMax){
            StartCoroutine(FuseTimer());
        }
    }

    IEnumerator FuseTimer(){
        yield return new WaitForSeconds(3f);
        explosion.Explode(gameObject.transform.position, 40f, "Explosion2");
        door.GetComponent<Animator>().SetBool("IsOpen", true);
        progressController.UpdateProgress(1);
    }
}
