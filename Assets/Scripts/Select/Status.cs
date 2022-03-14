using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    int hp = 0;
    private float attack;
    float speed = 0;
    float jump = 0;

    [SerializeField] GameObject chara;
    [SerializeField] GameObject attack2Object;

    /* ŠeƒXƒe[ƒ^ƒX‚Ìslider
     * 0: HP
     * 1: attack
     * 2: speed
     * 3: jump
     * 4: guard
     */
    [SerializeField] Slider[] status;

    public void ChangeStatus()
    {
        // ŠeUŒ‚‚ÌUŒ‚—Í
        int attack1Power = 0;
        int attack2Power = 0;

        switch (chara.name)
        {
            case ("Curry"):
                CurryController curryController = chara.GetComponent<CurryController>();
                // curryController.GetCharaStatus(hp, speed, jump);
                // curryController.GetStatusInfo(hp, speed, jump);
                hp = curryController.GetHp();
                speed = curryController.GetSpeed();
                jump = curryController.GetJump();
                Debug.Log(hp + ", " + speed + ", " + jump);
                status[0].value = hp;
                status[2].value = speed;
                status[3].value = jump;

                // Attack1‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack1Power = this.chara.transform.Find("CurryAttack1").gameObject.GetComponent<PunchController>().GetPower();

                // Attack2‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack2Power = this.attack2Object.GetComponent<CurryAttack2Controller>().GetPower();

                break;
            case ("Melon"):
                MelonController melonController = chara.GetComponent<MelonController>();
                // melonController.GetCharaStatus((int)status[0].value, status[1].value, status[2].value);
                hp = melonController.GetHp();
                speed = melonController.GetSpeed();
                jump = melonController.GetJump();
                Debug.Log(hp + ", " + speed + ", " + jump);
                status[0].value = hp;
                status[2].value = speed;
                status[3].value = jump;

                // Attack1‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack1Power = this.chara.transform.Find("MelonAttack1").gameObject.GetComponent<PunchController>().GetPower();

                // Attack2‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack2Power = this.attack2Object.GetComponent<MelonAttack2Controller>().GetPower();

                break;
            case ("France"):
                FranceController franceController = chara.GetComponent<FranceController>();
                // franceController.GetCharaStatus((int)status[0].value, status[1].value, status[2].value);
                hp = franceController.GetHp();
                speed = franceController.GetSpeed();
                jump = franceController.GetJump();
                Debug.Log(hp + ", " + speed + ", " + jump);
                status[0].value = hp;
                status[2].value = speed;
                status[3].value = jump;

                // Attack1‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack1Power = this.chara.transform.Find("FranceAttack1").gameObject.GetComponent<PunchController>().GetPower();

                // Attack2‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack2Power = this.attack2Object.GetComponent<FranceAttack2Controller>().GetPower();

                break;
            case ("Cornet"):
                CornetController cornetController = chara.GetComponent<CornetController>();
                // cornetController.GetCharaStatus((int)status[0].value, status[1].value, status[2].value);
                hp = cornetController.GetHp();
                speed = cornetController.GetSpeed();
                jump = cornetController.GetJump();
                Debug.Log(hp + ", " + speed + ", " + jump);
                status[0].value = hp;
                status[2].value = speed;
                status[3].value = jump;

                // Attack1‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack1Power = this.chara.transform.Find("CornetAttack1").gameObject.GetComponent<PunchController>().GetPower();

                // Attack2‚ÌUŒ‚—Í‚ğæ“¾‚·‚é
                attack2Power = this.attack2Object.GetComponent<CornetAttack2Controller>().GetPower();

                break;
        }

        // ƒXƒ‰ƒCƒ_[‚ÉŠeUŒ‚—Í‚Ì•½‹Ï’l‚ğİ’è‚·‚é
        this.status[1].value = (float)(attack1Power + attack2Power) / 2.0f;

        // ƒXƒ‰ƒCƒ_[‚ÉƒK[ƒh—¦‚Ì‹t”‚ğİ’è‚·‚é
        this.status[4].value = 1.0f / this.chara.GetComponent<PlayerController>().GetGuardingRatio();
    }
}
