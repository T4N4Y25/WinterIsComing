using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMechanic : MonoBehaviour
{
    [SerializeField] int Health = 4;
    public TMP_Text GameOverDisplay;
    private string CurrentText;
    public GameObject pnlHealth;
    private void Start()
    {
        CurrentText = GameOverDisplay.text;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Damage();
            Debug.Log("Damage taken");
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Damage()
    {
        Health--;
        if (Health == 3)
        {
            pnlHealth.GetComponent<Image>().color = Color.yellow;
        }
        if(Health == 2)
        {
            pnlHealth.GetComponent <Image>().color = new Color(1f,05f,0f); //orange
        }
        if (Health == 1)
        {
            pnlHealth.GetComponent<Image>().color = Color.red;
        }
        if (Health <= 0)
        {
            StartCoroutine(DelayedReset());
            GameOverDisplay.text = "You were killed...";
        }
    }

    private IEnumerator DelayedReset()
    {
        GameOverDisplay.text = "You were killed by the creature...";
        yield return new WaitForSeconds(3f);
        ResetGame();
    }
}
