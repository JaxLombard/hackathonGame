using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;



public class movement : MonoBehaviour

{
    public Rigidbody2D rb;
    public AudioSource AudioData;
    float lives = 2;
    
    //public ParticleSystem particle_death;

    public GameObject[] heart_UI;
    public bool isTouching;
    public Text DadJoke;

    
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        AudioData = GetComponent<AudioSource>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        isTouching = true;
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        isTouching = false;
    }
    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity = new Vector3(3, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity = new Vector3(-3, rb.velocity.y, 0);
        if (isTouching == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                rb.velocity = new Vector3(0, 9, 0);
            if (Input.GetKey(KeyCode.DownArrow))
                rb.velocity = new Vector3(0, -3, 0);
        }
        for (int i = 0; i <= heart_UI.Length - 1; i++)
            {
                if (lives >= i)
                {
                    heart_UI[i].SetActive(true);
                }
                else
                {
                    heart_UI[i].SetActive(false);
                }
            }

        if (lives > 2)
        {
            lives = 2;
        }
        if (lives < 0)
        {
        
        }    
        if (transform.position.x > 55) {

            DadJoke.text = "You Win!";

        }
    }

        public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "star-1")
        {
            other.gameObject.SetActive(false);
            DadJoke.text = "I'm reading a book about anti-gravity. It's impossible to put down!";
        }
        if (other.tag == "star-2")
        {
            other.gameObject.SetActive(false);
            DadJoke.text = "If a child refuses to sleep during nap time, are they guilty of resisting a rest?";
        }
        if (other.tag == "star-3")
        {
            other.gameObject.SetActive(false);
            DadJoke.text = "A slice of apple pie is $2.50 in Jamaica and $3.00 in the Bahamas. These are the pie rates of the Caribbean.";
        }
        if (other.tag == "enemy-head")
        {
            //particle_death.transform.position = other.transform.parent.position;
            other.transform.parent.gameObject.SetActive(false);
            //particle_death.Play();
        }
        if (other.tag == "heart pickup")
        {
            if (lives < 3)
            lives += 1;

            other.gameObject.SetActive(false);
        }
        if (other.tag == "enemy")
        {
            lives -= 1;
            other.gameObject.SetActive(false);
            AudioData.Play(0);
        }
    }

    // ///////////////////////////////////////////////////
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy-head")
        {

        }
    }


}



