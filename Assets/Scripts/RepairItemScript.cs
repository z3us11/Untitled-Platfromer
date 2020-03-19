using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RepairItemScript : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] GameManager gameManager;
    [SerializeField] string m_ownPlayerTag;
    [SerializeField] string m_otherPlayerTag;
    [SerializeField] float m_fillAmount;
    [SerializeField] float m_damageAmount;
    [Header("UI")]
    [SerializeField] Image m_repairLevelImg;
    [SerializeField] TMP_Text m_repairLevelTxt;
    [SerializeField] SpriteRenderer RepairBox;
    [SerializeField] Sprite[] RepairdImage;
    [SerializeField] GameObject kamehameha;
    [SerializeField] int index;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Another condition to check whether the player has the repair item or not
        if(other.tag == m_ownPlayerTag)
        {
            if(other.gameObject.GetComponent<PlayerPickUpScript>().IsRepair)
            {
                other.GetComponent<Movement>().soundManager.VfxPlay(SoundType.RepairOnGoing);
                other.GetComponent<PlayerPickUpScript>().GlowImge.gameObject.SetActive(false);
                other.gameObject.GetComponent<PlayerPickUpScript>().IsRepair = false; 
                other.GetComponent<Movement>().canMove = false;
                other.GetComponent<Movement>().jumpForce = 30f;
                other.GetComponent<Movement>().anim.anim.SetBool("Repair", true);
                Destroy(other.gameObject.GetComponent<PlayerPickUpScript>().transform.GetChild(1).gameObject);
                DoRepairing(other.gameObject);
            }
        }

        //Other player carries thing
        if (other.tag == m_otherPlayerTag)
        {
            //if (Input.GetKeyDown(m_repairKey))
            //{
            //    DoDamage(m_damageAmount);
            //}
        }

    }

    void DoRepairing(GameObject other)
    {
        m_fillAmount += 10f * 0.01f;
        m_repairLevelImg.DOFillAmount(m_fillAmount, other.gameObject.GetComponent<PlayerPickUpScript>().RepairAmount / 2f).OnComplete(() => AfterRepair(other));
    }
    void AfterRepair(GameObject other)
    {
        other.GetComponent<Movement>().anim.anim.SetBool("Repair", false);
        other.gameObject.GetComponent<PlayerPickUpScript>().RepairAmount = 0f;
        m_repairLevelTxt.text =Mathf.Round( (m_repairLevelImg.fillAmount * 100)) + "%";
        other.GetComponent<Movement>().canMove = true;
        other.GetComponent<Movement>().jumpForce = 30f;
        other.GetComponent<Movement>().soundManager.VfxPlay(SoundType.RepairFinsh);
        this.GetComponent<SpriteRenderer>().enabled = false;

        if (m_fillAmount >= 1)
        {
            gameManager.GameOver(m_ownPlayerTag);
            kamehameha.SetActive(true);
            gameManager.gameStart = false;
            Invoke("RepairCompleted", 4f);
        }

        index++;
        if (index <= 9)
        {
            RepairBox.GetComponent<SpriteRenderer>().sprite = RepairdImage[index];
        }
    }

    void RepairCompleted()
    {
        Time.timeScale = 0;
    }

    void DoDamage(float damageAmount)
    {
        m_repairLevelImg.fillAmount -= damageAmount;
        m_repairLevelTxt.text = (m_repairLevelImg.fillAmount * 100) + "%";
    }
}
