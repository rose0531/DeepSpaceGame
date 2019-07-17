using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] objects;
    private GameObject instance;

	// Use this for initialization
	private void Start () {
        int randObj = UnityEngine.Random.Range(0, objects.Length);  // Must specify which Random class we are using, which is the UnityEngine.Random not the System.Random
        instance = Instantiate(objects[randObj], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].tag == "Enemy")
                Gizmos.color = Color.red;
            else if (objects[i].tag == "AcidCeiling" || objects[i].tag == "AcidGround")
                Gizmos.color = Color.magenta;
            else
                Gizmos.color = Color.white;
        }

        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
