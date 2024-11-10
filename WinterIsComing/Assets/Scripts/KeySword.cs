using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderOnInteract : MonoBehaviour
{
    // The name of the scene to load when interacting
    

    private void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // Cast a ray from the camera to the point in the scene where the mouse is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object within a certain distance and if the object has the correct tag
        if (Physics.Raycast(ray, out hit, 10f)) // Adjust the distance as needed
        {
            if (hit.collider.CompareTag("Key"))
            {
                // Load the specified scene
                SceneManager.LoadScene("BossLevel");
            }
        }
    }
}
