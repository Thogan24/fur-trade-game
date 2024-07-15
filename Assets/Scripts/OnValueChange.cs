using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnValueChange : MonoBehaviour
{
    string previousText = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changed()
    {
        /*foreach (var item in this.gameObject.GetComponent<InputField>().text.GetTextInfo.characterInfo)
        {
            if (item.lineNumber > 2)
            {
                this.gameObject.GetComponent<InputField>().text = this.gameObject.GetComponent<InputField>().text.Remove(item.index, this.gameObject.GetComponent<InputField>().text.Length - item.index);
                break;
            }
        }*/
        
/*        foreach (string line in EnumerateLines(this.gameObject.GetComponent<TMP_InputField>().textComponent))
        {
            
        }*/
        TMP_Text text1 = this.gameObject.GetComponent<TMP_InputField>().textComponent;
        
        if (text1.GetTextInfo(text1.text).lineCount >= 3 && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Line above 4");
            Debug.Log(previousText);
            //this.gameObject.GetComponent<TMP_InputField>().textComponent.text = "a";
            this.gameObject.GetComponent<TMP_InputField>().text = previousText;
            //this.gameObject.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "a";
        }
        previousText = text1.text;
    }
/*    IEnumerable<string> EnumerateLines(TMP_Text text)
    {
        // We use GetTextInfo because .textInfo is not always valid
        foreach (TMP_LineInfo line in text.GetTextInfo(text.text).lineInfo)
            yield return text.text.Substring(line.firstCharacterIndex, line.characterCount);
    }*/
 
    
}
