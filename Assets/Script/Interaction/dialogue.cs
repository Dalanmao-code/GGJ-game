using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class dialogue : MonoBehaviour
{
    [Header("对话类")]
    [SerializeField] public string s1;//对话1
    [SerializeField] public string s2;//对话2
    [SerializeField] public string s3;//对话3
    [SerializeField] public string s4;//对话4
    [SerializeField] public string s5;//对话5
    [SerializeField] public float Timer_s1;//对话时间输入1
    [SerializeField] public float Timer_s2;//对话时间输入2
    [SerializeField] public float Timer_s3;//对话时间输入3
    [SerializeField] public float Timer_s4;//对话输入时间4
    [SerializeField] public float Timer_s5;//对话输入时间5
    [Header("数据类")]
    [SerializeField] public int dialogue_size;//对话数量
    [SerializeField] public int Next_dialogue_Number;//下一段对话的编号
    [SerializeField] public bool IsStart;//是否开启
    private int local_dialogue_Number;
    private float Timer=0;
    private float Timer_l1;//私有计时器总和
    private float Timer_l2;//私有计时器总和
    private float Timer_l3;//私有计时器总和
    private float Timer_l4;//私有计时器总和
    private float Timer_l5;//私有计时器总和

    private GameObject localDia;
    private Coroutine coroutine;

    [Header("储存类")]
    [SerializeField] public TextMeshProUGUI text_pro;
    

    [Header("Ai类")]
    [SerializeField] public float typingSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        local_dialogue_Number = 1;
        text_pro.gameObject.transform.parent.gameObject.SetActive(true);
        //将用户输入的时间累加变为私有计时器
        Timer_l1 = Timer_s1;
        Timer_l2 = Timer_s2+Timer_s1;
        Timer_l3 = Timer_s3+ Timer_s2 +Timer_s1;
        Timer_l4 = Timer_s4+ Timer_s3 + Timer_s2 +Timer_s1;
        Timer_l5 = Timer_s5+Timer_s4+ Timer_s3 + Timer_s2 +Timer_s1;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsStart &&local_dialogue_Number<=dialogue_size)
        {       
            //对话1
            if (Timer == 0&& local_dialogue_Number==1) {

                text_pro.text = "";
                coroutine = StartCoroutine(TypeText(text_pro, s1));
                Timer += Time.deltaTime;
                
            }
            if(Timer>0&&Timer< Timer_l1)
            {
                Timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)||Timer >= Timer_l1-0.1)
                {
                    local_dialogue_Number++;
                    Timer = Timer_l1;
                    return;
                } 
                
            }
            //对话2
            if (Timer == Timer_l1 && local_dialogue_Number == 2)
            {
                text_pro.text = "";
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(TypeText(text_pro, s2));
                Timer += Time.deltaTime;
            }
            if (Timer > Timer_l1 && Timer < Timer_l2)
            {
                Timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Timer >= Timer_l2 - 0.1)
                {
                    local_dialogue_Number++;
                    Timer = Timer_l2;
                    return;
                }
            }

            //对话3
            if (Timer == Timer_l2 && local_dialogue_Number == 3)
            {
                text_pro.text = "";
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(TypeText(text_pro, s3));
                Timer += Time.deltaTime;
            }
            if (Timer > Timer_l2 && Timer < Timer_l3)
            {
                Timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Timer >= Timer_l3 - 0.1)
                {
                    local_dialogue_Number++;    
                    Timer = Timer_l3;
                    return;
                }
            }

            //对话4
            if (Timer == Timer_l3 && local_dialogue_Number == 4)
            {
                text_pro.text = "";
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(TypeText(text_pro, s4));
                Timer += Time.deltaTime;
            }
            if (Timer > Timer_l3 && Timer < Timer_l4)
            {
                Timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Timer >= Timer_l4 - 0.1)
                {
                    local_dialogue_Number++;
                    Timer = Timer_l4;
                    return;
                }
            }

            //对话5
            if (Timer == Timer_l4 && local_dialogue_Number == 5)
            {
                text_pro.text = "";
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(TypeText(text_pro, s5));
                Timer += Time.deltaTime;
            }
            if (Timer > Timer_l4 && Timer < Timer_l5)
            {
                Timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Timer >= Timer_l5 - 0.1)
                {
                    local_dialogue_Number++;
                    Timer = Timer_l5;
                    StopCoroutine(coroutine);
                    return;
                }
            }

            
        }
        if(local_dialogue_Number == dialogue_size + 1)
        {
            if (Next_dialogue_Number == -1)
            {
                text_pro.gameObject.transform.parent.gameObject.SetActive(false);
                Destroy(this);
            }
            else
            {

                this.gameObject.transform.parent.transform.GetChild(Next_dialogue_Number - 1).GetComponent<dialogue>().enabled = true;
                Destroy(this);
            }
        }
            
    }

    IEnumerator TypeText(TextMeshProUGUI TextUi,string s1)
    {
        yield return new WaitForSeconds(1f);
        // 遍历完整的文本字符串
        foreach (char letter in s1)
        {
            // 将每个字符逐个添加到显示的文本中
            TextUi.text += letter;

            // 等待一定时间后继续显示下一个字符
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
