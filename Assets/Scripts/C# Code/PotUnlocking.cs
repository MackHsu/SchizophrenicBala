using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotUnlocking : MonoBehaviour
{
    public RectTransform progressFilling;
    public Text rateText;
    public GameObject OuterFilling;
    public Transform OuterPointer;
    public GameObject SpaceRandomEvent;

    private float rate = 0;
    public float Rate
    {
        get
        {
            return rate;
        }
        set
        {
            if(rate != value)
            {
                if (value < 0 || value > 1) return;
                progressFilling.sizeDelta = new Vector2(value * 257.66F, progressFilling.rect.height);
                rateText.text = System.Math.Round(value * 100, 0) + "%";
                rate = value;
            }
        }
    }

    private bool started = false;
    private bool ePressed = false;
    private bool spacePressed = false;

    private float incressSpeed; // 长按E键进度条增加速度
    private float decressSpeed; // 松开时进度条减少速度
    private float rotateSpeed; // Space按钮旋转的时间
    private float time; // 下一次激活Space按钮的时间
    private float startAngle = 0F; // Space按钮活跃区起始角度
    private float activeAngle = 0F; // Space按钮活跃区覆盖角度


    private bool randomEventTriggered = false;

    private void Start()
    {
        SetAndStart();
    }

    public void SetAndStart(float incressSpeed = 0.002F, float decressSpeed = 0.0003F, float rotateSpeed = 2F)
    {
        Rate = 0;
        this.incressSpeed = incressSpeed;
        this.decressSpeed = decressSpeed;
        this.rotateSpeed = rotateSpeed;
        this.started = true;
        StartCoroutine("TriggerRandomEvent");
        StartCoroutine("DecressProcessBar");
    }

    IEnumerator IncressProcessBar()
    {
        Debug.Log("Incress speed: " + incressSpeed);
        while (true)
        {
            Rate += incressSpeed;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator DecressProcessBar()
    {
        Debug.Log("decress speed: " + decressSpeed);
        while (true)
        {
            Rate -= decressSpeed;
            yield return new WaitForFixedUpdate();
        }
    }

    public void Update()
    {
        if (started)
        {
            if (!randomEventTriggered)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E down");
                    StartCoroutine("IncressProcessBar");
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    Debug.Log("E up");
                    StopCoroutine("IncressProcessBar");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spacePressed = false;
                    StopCoroutine("Rotate");
                    SpaceRandomEvent.SetActive(false);
                    randomEventTriggered = false;

                    // 失败，增加警戒值
                    float currentAngle = OuterPointer.localEulerAngles.z >= 180F ? OuterPointer.localEulerAngles.z - 360F : OuterPointer.localEulerAngles.z;
                    if ((startAngle + activeAngle < 180F && (currentAngle < startAngle || currentAngle > startAngle + activeAngle))
                        || startAngle + activeAngle >= 180F && (currentAngle < startAngle && currentAngle > startAngle + activeAngle - 360F))
                    {
                        Debug.Log("space event failed");
                    }

                    StartCoroutine("TriggerRandomEvent");
                }
            }
        }
    }

    IEnumerator TriggerRandomEvent()
    {
        time = Random.Range(2F, 5F);
        startAngle = Random.Range(-180F, 180F);
        activeAngle = Random.Range(0.05F, 0.25F) * 360;
        yield return new WaitForSecondsRealtime(time);
        time = 0;
        StopCoroutine("IncressProcessBar");
        randomEventTriggered = true;

        SpaceRandomEvent.SetActive(true);
        StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        OuterFilling.transform.localEulerAngles = new Vector3(0, 0, startAngle);
        OuterFilling.GetComponent<Image>().fillAmount = activeAngle / 360F;
        OuterPointer.transform.localEulerAngles = new Vector3(0, 0, 0);
        while (true)
        {
            OuterPointer.Rotate(new Vector3(0, 0, rotateSpeed)); // 转动指针
            yield return new WaitForSecondsRealtime(0.01F);
        }
    }
}
