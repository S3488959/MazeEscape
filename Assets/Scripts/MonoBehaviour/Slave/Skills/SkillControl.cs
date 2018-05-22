using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    public string skillInputName = "1";
    public Image darkMask;
    public Text coolDownText;

    [SerializeField]
    private Skill skill;
    [SerializeField]
    private GameObject player;
    private PlayerAPController apControl;
    private Image m_buttonImage;
    private float coolTime;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    private void Start()
    {
        Init(skill, player);
    }

    public void Init(Skill skill, GameObject player)
    {
        this.skill = skill;
        m_buttonImage = GetComponent<Image>();
        m_buttonImage.sprite = skill.skillSprite;
        darkMask.sprite = skill.skillSprite;
        coolTime = skill.skillCoolTime;
        apControl = player.GetComponent<PlayerAPController>();
        skill.Init(player);
        SkillReady();
    }

    void Update()
    {
        
        if (IsCooldownFinished())
        {
            if(isSkillReady())
            {
                SkillReady();
                if (Input.GetButtonDown(skillInputName))
                {
                    ButtonTriggered();
                }
            }
            else
            {
                darkMask.enabled = true;
                darkMask.fillAmount = 1;
            }
        }
        else
        {
            CoolDown();
        }
    }

    private bool isSkillReady()
    {
        if (skill is BreakGameObject)
        {
            return apControl.CanUseSkill(skill.skillCost) && player.GetComponent<BreakGameObjectBehaviour>().CheckIfBreakable();
        }
        else
        {
            return apControl.CanUseSkill(skill.skillCost);
        }
    }

    private bool IsCooldownFinished()
    {
        return (Time.time > nextReadyTime);
    }

    private void SkillReady()
    {
        coolDownText.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownText.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolTime);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolTime + Time.time;
        coolDownTimeLeft = coolTime;
        darkMask.enabled = true;
        coolDownText.enabled = true;
        apControl.UseSkill(skill.skillCost);
        skill.ActivateSkill();
    }
}

