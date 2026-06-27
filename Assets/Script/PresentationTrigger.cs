using UnityEngine;

public class PresentationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject presentationMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            presentationMenu.SetActive(true);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
