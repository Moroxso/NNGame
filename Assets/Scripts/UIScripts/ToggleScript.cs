using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Colors")]
    public Color backgroundOnColor = new Color(0.188f, 0.858f, 0.356f, 1f); // �������
    public Color backgroundOffColor = new Color(0.784f, 0.784f, 0.784f, 1f); // �����

    [Header("Checkmark Position")]
    public float checkmarkOnPosition = 18f; // ������� ��� ���������
    public float checkmarkOffPosition = -18f; // ������� ��� ����������

    [Header("Animation")]
    public float animationDuration = 0.2f;

    private Toggle toggle;
    private Image background;
    private RectTransform checkmarkRect;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        background = toggle.targetGraphic as Image;
        checkmarkRect = toggle.graphic.GetComponent<RectTransform>();

        // ������������� ���������� ���������
        UpdateVisuals(toggle.isOn);

        // �������� �� ������� ������������
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(AnimateToggle(isOn));
    }

    private System.Collections.IEnumerator AnimateToggle(bool targetState)
    {
        float elapsedTime = 0f;
        Color startColor = background.color;
        Color targetColor = targetState ? backgroundOnColor : backgroundOffColor;

        float startPos = checkmarkRect.anchoredPosition.x;
        float targetPos = targetState ? checkmarkOnPosition : checkmarkOffPosition;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            // �������� �����
            background.color = Color.Lerp(startColor, targetColor, t);

            // �������� �������
            float newX = Mathf.Lerp(startPos, targetPos, t);
            checkmarkRect.anchoredPosition = new Vector2(newX, checkmarkRect.anchoredPosition.y);

            yield return null;
        }

        // ��������, ��� �������� ��������� ���������
        UpdateVisuals(targetState);
    }

    private void UpdateVisuals(bool isOn)
    {
        background.color = isOn ? backgroundOnColor : backgroundOffColor;
        float xPos = isOn ? checkmarkOnPosition : checkmarkOffPosition;
        checkmarkRect.anchoredPosition = new Vector2(xPos, checkmarkRect.anchoredPosition.y);
    }

    private void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}

