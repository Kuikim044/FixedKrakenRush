using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputNameData : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TMP_InputField txtInput;
    public GameObject dialog;
    public PlayerData player;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("username"))
        {
            dialog.SetActive(false);
            txtName.text = PlayerPrefs.GetString("username");
        }
        else if (player != null && !string.IsNullOrEmpty(player.playerName)) // เพิ่มเงื่อนไขใหม่
        {
            dialog.SetActive(false);
            txtName.text = player.playerName;
        }
    }

    public void OnClickSave()
    {
        if(txtInput.text != "")
        {
            PlayerPrefs.SetString("username", txtInput.text);
            PlayerPrefs.Save();
            if (player != null)
            {
                player.playerName = txtInput.text; // บันทึกชื่อลงใน PlayerData
            }

            dialog.SetActive(false);
            txtName.text =  txtInput.text;
            
        }
    }
   
}
