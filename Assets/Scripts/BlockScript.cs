using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlockScript : MonoBehaviour
{
    [SerializeField] KeyCode blockKey;
    [SerializeField] Movement Player;
    [SerializeField] Image bar;
    [SerializeField] bool hasBlockPower;
    [SerializeField] GameObject shootPrefab;
    [SerializeField] Transform[] shootPos;
    [SerializeField] float timeForBlockRechange;
    [SerializeField] float currentTime;
    [SerializeField] SpriteRenderer bullet;
    [SerializeField] Sprite[] bulletSprite;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Player.gameManager.gameStart)
        {
            if (!hasBlockPower)
            {
                currentTime += Time.deltaTime;
                bar.fillAmount = currentTime / timeForBlockRechange;
                if (currentTime >= timeForBlockRechange)
                {

                    hasBlockPower = true;
                    currentTime = 0;
                    bar.fillAmount = 1;
                    bullet.sprite = bulletSprite[1];
                }


            }
            if (hasBlockPower && Input.GetKeyDown(blockKey))
            {

                bullet.sprite = bulletSprite[0];

                Player.anim.anim.SetBool("Shoot", true);
                StartCoroutine(waitTime());
                //  Debug.LogError("Shoot " +hasBlockPower);
            }
        }

    }
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.0f);
        Player.soundManager.VfxPlay(SoundType.Shoot);
        Player.anim.anim.SetBool("Shoot", false);
        int index = GetComponent<Movement>().side == 1 ? 0 : 1;
        GameObject shoot = Instantiate(shootPrefab, shootPos[index].position, Quaternion.identity);
        shoot.GetComponent<ShootScript>().Direction = Player.side * Vector3.right;
        bar.DOFillAmount(0, 0.5f);
        hasBlockPower = false;
    }
}
