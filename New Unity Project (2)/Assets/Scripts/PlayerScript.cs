using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject bulletPrefab;
    public Transform bulletspawn;
    public float bulletSpeed = 10f;
    //public float lifetime = 5f;
    private float nextFireTime = 0.0f;
    private GameObject HUD;
    private TextMesh HUD_bullets;
    private Image deathImage;

    [Header("Bullet properties")]
    [Tooltip("Preset value to tell with how much bullets will our waepon spawn inside rifle.")]
    public float bulletsInTheGun = 5;

    private HealthScript hs;
    private bool corotuineStarted;


    // Start is called before the first frame update
    void Start()
    {
        corotuineStarted = false;
        hs = GetComponent<HealthScript>();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        HUD_bullets = HUD.GetComponent<TextMesh>();
        HUD.SetActive(false);
        deathImage = GameObject.FindGameObjectWithTag("DeathImage").gameObject.GetComponent<Image>();

    }
    

    // Update is called once per frame
    void Update()
    {
        if (HUD == null)
        {
            HUD = GameObject.FindGameObjectWithTag("HUD");
            HUD_bullets = HUD.GetComponent<TextMesh>();
        }

        if (!hs.isDead)
        {
            if (Weapon.gameObject.activeInHierarchy != false)
            {
                HUD.SetActive(true);
                HUD_bullets.text = bulletsInTheGun.ToString();


                if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFireTime)
                {
                    if (PauseMenu.GamePaused != true)
                    {
                        Shooting();
                    }
                }


            }
        }

        else
        {
            if (!corotuineStarted)
            {
                StartCoroutine(Death());
                corotuineStarted = true;
            }
        }
    }

    public void Shooting()
    {

        if (bulletsInTheGun > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = bulletspawn.position;
            Vector3 rotation = bullet.transform.rotation.eulerAngles;
            //bullet.transform.rotation = Quaternion.Euler(rotation.x, bulletspawn.transform.eulerAngles.y, rotation.z);
            bullet.transform.rotation = bulletspawn.transform.rotation;
            bullet.GetComponent<Rigidbody>().AddForce(bulletspawn.forward * bulletSpeed, ForceMode.Impulse);

            Weapon.GetComponent<AudioSource>().Play();
            Weapon.GetComponent<Animation>().Play("GunShot");

            nextFireTime = Time.time + 2f;

            bulletsInTheGun--;
        }
    }



    private IEnumerator DestroyBulletDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

    }

    private IEnumerator Death()
    {
        Debug.Log(deathImage);

        while (deathImage.color.a < 1)
        {
            Color temp = deathImage.color;
            temp.a += Time.unscaledDeltaTime;
            deathImage.color = temp;

            yield return new WaitForEndOfFrame();
        }

        Time.timeScale = 1;
        StaticLevelInfo.NextSceneToLoad = 2;
        SceneManager.LoadScene(1);
    }


}
