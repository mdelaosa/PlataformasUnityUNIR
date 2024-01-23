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
    public string SceneName;

    bool tocando_pared;
    bool deslizar_pared;
    public float velocidadDeslizar;

    bool saltando_pared;
    public float x_fuerza_pared;
    public float y_fuerza_pared;
    public float tiempo_pared;


    public GameObject cortinaInicio;
    public GameObject cortinaMuerte;
    public GameObject cortinaFinal;
    public GameObject timer;

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

    //Para iniciar la m�sica de gameplay
    public static int InicioMusica = 0;
    public GameObject Musica;

    public static bool desaparecer = false;
    public GameObject CanvasVidas;
    public static int cuentaHUD = 0;

    //Contador de niveles
    public static int CuentaNiveles = 0;

    private void Awake()
    {
        desaparecer = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EsperaCortina());

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

        //Salto pared
        if (Input.GetKeyDown("space") && tocando_pared == true && cuenta_saltos < 2)
        {
            saltando_pared = true;
            Invoke("SetSaltandoParedFalse", tiempo_pared);
        }
        /*if (saltando_pared == true)
        {
            //rb2d.AddForce(Vector2.up * impulsoUP, ForceMode2D.Impulse);
            //rb2d.velocity = new Vector2(x_fuerza_pared * -Input, y_fuerza_pared);

        }*/

        //Deslizarse pared
        if (tocando_suelo == false && tocando_pared == true && cuenta_saltos < 2)
        {
            deslizar_pared = true;
        }
        else
        {
            deslizar_pared = false;
        }

        if (deslizar_pared)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -velocidadDeslizar, float.MaxValue));
        }


        //Comprobaci�n HUD
        if (desaparecer == true)
        {
            CanvasVidas.SetActive(false);
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

        //Pared
        if (collision.gameObject.tag == "Wall")
        {
            tocando_pared = true;
        }

        //Sandias
        if (collision.gameObject.tag == "Coin")
        {
            // Destruir Gameobject
            ++Sandias.sandias;
            Sandia.PlayOneShot(Sandia.clip);
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
                heart3.SetActive(false);
                Debug.Log("Queda 1 vida");
                break;

            case 0:
                heart1.SetActive(false);
                Debug.Log("Muerto");
                Musica.SetActive(false);
                cuentaHUD = 0;
                //GameOver.PlayOneShot(GameOver.clip);
                cortinaMuerte.SetActive(true);
                cortinaMuerte.SetActive(true);
                StartCoroutine(EsperaCortina());
                StartCoroutine(EsperaMuerte());
                break;
            default:
            break;
        }
    }

    //Salto pared
    void SetSaltandoParedFalse()
    {
        saltando_pared = false;
    }

    //Final de nivel
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bandera")
        {
            Bandera.PlayOneShot(Bandera.clip);
            Play.NivelAleatorio = Random.Range(1, 2);
            cortinaFinal.SetActive(true);
            timer.SetActive(false);
            Destroy(CanvasVidas);
            StartCoroutine(EsperaCortinaFinal());
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
        yield return new WaitForSeconds(0f);
        ++CuentaNiveles;
        Debug.Log(CuentaNiveles);
        SceneManager.LoadScene(Play.NivelAleatorio);
    }
    IEnumerator EsperaMuerte()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(CanvasVidas);
        SceneManager.LoadScene(SceneName);

    }

    IEnumerator EsperaCortina()
    {
        yield return new WaitForSeconds(0.5f);
        cortinaInicio.SetActive(false);
        timer.SetActive(true);
        CanvasVidas.SetActive(true);
    }

    IEnumerator EsperaCortinaFinal()
    {
        yield return new WaitForSeconds(0.49f);
        StartCoroutine(EsperaFinal());
    }
}

