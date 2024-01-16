using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Parámetros a utilizar
    public Animator anim;
    public Rigidbody2D rb2d;
    public float velocidad;
    float speed;
    public float impulso;
    public float impulso2;
    bool Salto2 = false;
    int cuenta_saltos = 0;
    bool tocando_suelo = false;

    //public AudioSource jump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movimiento jugador
        Vector3 movimiento = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position = transform.position + movimiento * velocidad * Time.deltaTime;

        //cambiar sprite de lado
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Cambio de animación idle --> run
        speed = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(speed));

        //salto
        if (Input.GetKeyDown("space") && tocando_suelo == true)
        {
            ++cuenta_saltos;
            rb2d.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
            tocando_suelo = false;
            anim.SetBool("Grounded", false);
            //jump.PlayOneShot(jump.clip);
            StartCoroutine(EsperaSalto());
        }
        //salto 2
        if (Input.GetKeyDown("space") && tocando_suelo == false && cuenta_saltos < 2 && Salto2 == true)
        {
            anim.SetBool("Doble", true);
            ++cuenta_saltos;
            rb2d.AddForce(Vector2.up * impulso2, ForceMode2D.Impulse);
            //jump.PlayOneShot(jump.clip);
        }
    }

    //Detección de colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            tocando_suelo = true;
            anim.SetBool("Grounded", true);
            anim.SetBool("Dañado", false);
            anim.SetBool("Doble", false);
            cuenta_saltos = 0;
            Salto2 = false;
        }
    }

        //Coorutina para activar el segundo salto
        IEnumerator EsperaSalto()
    {
        yield return new WaitForSeconds(0.1f);
        Salto2 = true;
    }
}
