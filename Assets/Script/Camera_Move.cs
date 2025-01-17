using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Camera_Move : MonoBehaviour
{
    [Header("储存类")]
    [SerializeField] public Camera MainCamera; //主摄像机
    [SerializeField] public Animator animator; //获取相机动画器
    [Header("数值类")]
    [SerializeField] public float MaxCamera_size;
    [SerializeField] public float MinCamera_size;
    [SerializeField] public float moveSpeed = 0.1f;  // 控制背景的移动速度
    [SerializeField] public PostProcessVolume postProcessVolume;
    [SerializeField] public float depthOfField_value;//景深值 
    [Header("计时器")]
    [SerializeField] public float t;
    private Coroutine runningCoroutine;
    private Vector3 lastMousePosition;  // 上一帧鼠标的位置

    private LensDistortion lensDistortion;
    private DepthOfField depthOfField;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化鼠标位置为当前鼠标位置
        lastMousePosition = Input.mousePosition;

        // 尝试获取 Lens Distortion 效果
        if (postProcessVolume.profile.TryGetSettings(out lensDistortion))
        {
            // 如果找到 Lens Distortion，则初始化
            lensDistortion.scale.value = 1f;
        }
        // 尝试获取 Lens Distortion 效果
        if (postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            // 如果找到 Lens Distortion，则初始化
            depthOfField.focusDistance.value = 3.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        depthOfField.focusDistance.value = depthOfField_value;
        if (Input.GetMouseButtonDown(1))
        {
            if(runningCoroutine != null)
            StopCoroutine(runningCoroutine);
            runningCoroutine = StartCoroutine(Lerp_Camera(true));
        }

        lensDistortion.scale.value = Mathf.Lerp(MinCamera_size, MaxCamera_size, t); //缩放至最小
        
        if(Input.GetMouseButtonUp(1))
        {
            if (runningCoroutine != null)
                StopCoroutine(runningCoroutine);
            runningCoroutine = StartCoroutine(Lerp_Camera(false));
        }
        Mouse_move();
    }
    IEnumerator Lerp_Camera(bool IsStart)
    {
        if (IsStart)
        {
            while (t<=1f)
            {
                t += 0.02f;
                yield return new WaitForSeconds(0.02f);
                if (t >= 0.7f)
                {
                    animator.SetBool("Focus", true);
                }
            }
            
        }
        else
        {
            while (t>=0f)
            {
                animator.SetBool("Focus", false);
                t -= 0.02f;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }



    public void Mouse_move()
    {
        // 获取当前鼠标的屏幕坐标
        Vector3 currentMousePosition = Input.mousePosition;

        // 计算鼠标的移动距离（屏幕空间中的差值）
        Vector3 mouseDelta = currentMousePosition - lastMousePosition;

        // 根据鼠标的移动距离来移动背景，乘以一个速度因子来控制背景移动的速度
        MainCamera.transform.position += new Vector3(mouseDelta.x * moveSpeed, mouseDelta.y * moveSpeed, 0);

        // 更新上一帧的鼠标位置
        lastMousePosition = currentMousePosition;
    }
}
