using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    //[SerializeField] string sceneName;
    public Rigidbody2D rb;
    public float releaseTime = .15f;
    private bool isPressed = false;
    public Rigidbody2D hook;
    public float maxHookDrag = 5f;

    public GameObject nextBall;
    void Update()
    {

        if(isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxHookDrag) {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxHookDrag;
            }
            else
            {
                rb.position = mousePos;
            }
            
        }    
    }

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release ()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(2f);
        if(nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else if(Enemy.numOfEnemies > 0)
        {
            Enemy.numOfEnemies = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
