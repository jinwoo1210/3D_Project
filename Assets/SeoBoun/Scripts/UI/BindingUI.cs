using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BindingUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms;
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;
    protected Dictionary<string, Image> images;
        
    protected virtual void Awake()
    {
        Bind();
    }

    private void Bind()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();
        images = new Dictionary<string, Image>();

        RectTransform[] children = GetComponentsInChildren<RectTransform>();

        foreach(RectTransform child in children)
        {
            string name = child.gameObject.name;

            if(transforms.ContainsKey(name))
            {
                continue;
            }
            transforms.Add(name, child);

            Button button = child.GetComponent<Button>();
            if(button != null)
            {
                buttons.Add(name, button);
            }

            TMP_Text text = child.GetComponent<TMP_Text>();
            if(text != null)
            {
                texts.Add(name, text);
            }

            Image image = child.GetComponent<Image>();
            if(images != null)
            {
                images.Add(name, image);
                Debug.Log(name);
            }
            else if(image.enabled == false)
            {
                if (images.ContainsKey(name))
                    return;

                images.Add(name, image);
                Debug.Log($"Ãß°¡ : {name}");
            }
        }
    }
}

