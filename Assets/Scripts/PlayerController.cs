using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Par�metros a utilizar
    public Animator anim;
    public Rigidbody2D rb2d;
    public float velocidad;
    float speed;
    public float impulso;
    public float impulso2;
    bool Salto2 = false;
    int cuenta_saltos = 0;
    public AudioSource jump;
    bool tocando_suelo = false;

    public static int contador_vidas = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public AudioSource GameOver;
    public AudioSource hit;
    public float impulsoUP;
    public float impulsoLEFT;

    //Para volver visible el HUD de vidas solo una vez
    public static int CreadordeVidas = 0;
    public GameObject CanvasVidas;

    //Para iniciar la m�sica de gameplay
    public static int InicioMusica = 0;
    public GameObject Musica;


    // Start is called before the first frame update
    void Start()
    {
        ++CreadordeVidas;
        if (CreadordeVidas == 1)
        {
            CanvasVidas.SetActive(true);
        }

        ++InicioMusica;
        if (InicioMusica == 1)
        {
            Musica.SetActive(true);
        }
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

        //Cambio de animaci�n idle --> run
        speed = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(speed));

        //salto
        if (Input.GetKeyDown("space") && tocando_suelo == true)
        {
            ++cuenta_saltos;
            rb2d.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
            tocando_suelo = false;
            anim.SetBool("Grounded", false);
            jump.PlayOneShot(jump.clip);
            StartCoroutine(EsperaSalto());
        }
        //salto 2
        if (Input.GetKeyDown("space") && tocando_suelo == false && cuenta_saltos < 2 && Salto2 == true)
        {
            anim.SetBool("Doble", true);
            ++cuenta_saltos;
            rb2d.AddForce(Vector2.up * impulso2, ForceMode2D.Impulse);
            jump.PlayOneShot(jump.clip);
        }
    }

    //Detecci�n de colisiones
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            tocando_suelo = true;
            anim.SetBool("Grounded", true);
            anim.SetBool("Da�ado", false);
            anim.SetBool("Doble", false);
            cuenta_saltos = 0;
            Salto2 = false;
        }
        //Quitar vidas cuando es da�ado
        if (collision.gameObject.tag == "Enemy")
        {
            --contador_vidas;
            hit.PlayOneShot(hit.clip);
            rb2d.AddForce(Vector2.up * impulsoUP, ForceMode2D.Impulse);
            rb2d.AddForce(Vector2.left * impulsoLEFT, ForceMode2D.Impulse);
            anim.SetBool("Da�ado", true);
        }

        //Cambio de vidas en HUD
        switch (contador_vidas)
        {
            case 2:
                heart3.SetActive(false);
                Debug.Log("Quedan 2 vidas");
                break;

            case 1:
                heart2.SetActive(false);
                Debug.Log("Queda 1 vida");
                break;

            case 0:
                rb2d.bodyType = RigidbodyType2D.Static;
                heart1.SetActive(false);
                Debug.Log("Muerto");
                Musica.SetActive(false);
               GameOver.PlayOneShot(GameOver.clip);
                break;
            default:
            break;
        }
    }
        //Coorutina para activar el segundo salto
        IEnumerator EsperaSalto()
    {
        yield return new WaitForSeconds(0.1f);
        Salto2 = true;
    }
}
