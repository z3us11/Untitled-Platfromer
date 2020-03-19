using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPickUpScript : MonoBehaviour
{
    float m_repairAmount;
    bool isRepairable;
    bool playerGetStoned;
    [SerializeField] float timeToGetUnstoned;
    [SerializeField] Image StonedBar;
    [SerializeField] GameObject Glow;
    [Header("Camera shake")]
    [SerializeField] Camera cam;
    [SerializeField] float duration;
    [SerializeField] SpriteRenderer post;
    public GameObject GlowImge
    {
        get
        {
            return Glow;
        }
        set
        {
            Glow = value;
        }
    }
    public bool IsRepair
    {
        get
        {
            return isRepairable;
        }
        set
        {
            isRepairable = value;
        }
    }
    public float RepairAmount
    {
        get
        {
            return m_repairAmount;
        }
        set
        {
            m_repairAmount = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison : " + collision.gameObject, collision.gameObject);
        if (collision.gameObject.tag == "Repair" && !isRepairable)
        {
            GetComponent<Movement>().soundManager.VfxPlay(SoundType.PickUp);

            m_repairAmount = collision.gameObject.GetComponent<SpawnItem>().RepairAmount;
            isRepairable = true;
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.SetParent(this.transform);
            FindObjectOfType<ItemSpawnerScript>().Spawn();
            post.enabled = true;
            Glow.SetActive(true);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" && isRepairable)
        {
            isRepairable = false;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((-(GetComponent<Movement>().side)) * 4, 8), ForceMode2D.Impulse);
            StartCoroutine(Delay());
            playerGetStoned = true;
            Glow.gameObject.SetActive(false);
            GetComponent<Movement>().canMove = false;
            GetComponent<Movement>().jumpForce = 0;
            StonedBar.fillAmount = 1;
            StonedBar.gameObject.SetActive(true);
            StonedBar.DOFillAmount(0, timeToGetUnstoned).OnComplete(() => GetUnStoned());
            Destroy(collision.gameObject);
            GetComponent<Movement>().soundManager.VfxPlay(SoundType.Hit);
            CameraShakeEffect();
            post.enabled = false;
            StartCoroutine(bliking());

        }
        if (collision.gameObject.tag == "Block")
        {
            playerGetStoned = true;
            GetComponent<Movement>().canMove = false;
            GetComponent<Movement>().jumpForce = 0;
            StonedBar.fillAmount = 1;
            StonedBar.gameObject.SetActive(true);
            StonedBar.DOFillAmount(0, timeToGetUnstoned).OnComplete(() => GetUnStoned());
            GetComponent<Movement>().soundManager.VfxPlay(SoundType.Hit);
            Destroy(collision.gameObject);
            CameraShakeEffect();
            StartCoroutine(bliking());


        }
    }

    void GetUnStoned()
    {
        StonedBar.gameObject.SetActive(true);
        playerGetStoned = false;
        GetComponent<Movement>().canMove = true;
        GetComponent<Movement>().jumpForce = 30f;

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(1).gameObject.transform.SetParent(null);
    }
    void CameraShakeEffect()
    {
        cam.DOShakePosition(duration, 1.5f, 100);
    }
    IEnumerator bliking()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
}
