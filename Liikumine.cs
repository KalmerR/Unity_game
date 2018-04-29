using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Liikumine : MonoBehaviour
{

    public float speed;

    public Text countText;

    public Text winText;

    public string PickUpTag = "Pick Up";

    private Rigidbody rb;

    private int count;
    private int pickupsInScene;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        Scene scene = SceneManager.GetActiveScene();
        foreach (var i in scene.GetRootGameObjects())
        {
            if (i.name == "PickUps")
            {
                foreach (Transform t in i.transform)
                {
                    Debug.Log(t.tag);
                    if (t.tag == PickUpTag)
                        pickupsInScene++;
                }
                return;
            }
        }
        Debug.Log("pickupsInScene " + pickupsInScene);
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Skoor: " + count.ToString();
        if (count >= pickupsInScene)
        {
            winText.text = "Sa kogusid kokku kõik ruudukesed! Õnnitlused!";
        }
    }
}