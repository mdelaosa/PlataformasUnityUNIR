using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    public AudioSource jump;
    bool tocando_suelo = false;
    public string SceneName;

    //Saltos en pared
    public LayerMask paredLayer;
    public Transform checkPared;
    bool tocar_Pared;
    bool deslizarse_Pared = false;
    public float velocidad_deslizarse = 2f;

    public static int contador_vidas = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public AudioSource GameOver;
    public AudioSource hit;
    public AudioSource Sandia;
    public AudioSource Bandera;
    public float impulsoUP;
    public float impulsoLEFT;

    //Para volver visible el HUD de vidas solo una vez
    public static int CreadordeVidas = 0;
    public GameObject CanvasVidas;

    //Para iniciar la música de gameplay
    public static int InicioMusica = 0;
    public GameObject Musica;

    //Contador de niveles
    public static int CuentaNiveles = 0;


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

        //Pared
        
        if (tocar_Pared == true)
        {
            deslizarse_Pared = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -velocidad_deslizarse, float.MaxValue));
        }
        else
        {
            deslizarse_Pared = false;
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

        //Pared
        if (collision.gameObject.tag == "Wall")
        {
            tocar_Pared = true;
            tocar_Pared = Physics2D.OverlapBox(checkPared.position, new Vector2(.18f, 1.45f), paredLayer);
        }

        //Sandias
        if (collision.gameObject.tag == "Coin")
        {
            // Destruir Gameobject
            ++Sandias.sandias;
            Sandia.PlayOneShot(Sandia.clip);
        }

        //Quitar vidas cuando es dañado
        if (collision.gameObject.tag == "Enemy")
        {
            --contador_vidas;
            hit.PlayOneShot(hit.clip);
            rb2d.AddForce(Vector2.up * impulsoUP, ForceMode2D.Impulse);
            rb2d.AddForce(Vector2.left * impulsoLEFT, ForceMode2D.Impulse);
            anim.SetBool("Dañado", true);
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
                CanvasVidas.SetActive(false);
               GameOver.PlayOneShot(GameOver.clip);
                SceneManager.LoadScene(SceneName);
                break;
            default:
            break;
        }
    }

    //Final de nivel
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bandera")
        {
            Bandera.PlayOneShot(Bandera.clip);
            StartCoroutine(EsperaFinal());
        }
    }

    //Coorutina para activar el segundo salto
    IEnumerator EsperaSalto()
    {
        yield return new WaitForSeconds(0.1f);
        Salto2 = true;
    }

    //Coorutina para activar el siguiente nivel
    IEnumerator EsperaFinal()
    {
        yield return new WaitForSeconds(0.2f);
        ++CuentaNiveles;
        Debug.Log(CuentaNiveles);
        SceneManager.LoadScene(Play.NivelAleatorio);
    }
}
