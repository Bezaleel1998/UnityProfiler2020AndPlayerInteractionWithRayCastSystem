using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    private string email = "bezaleelbagoes@gmail.com";
    private string password = "123456";

    public InputField inputEmail;
    public InputField inputPass;

    public Text error_msg;

    // Update is called once per frame
    public void LoginOnClick()
    {

        if (inputEmail.text == email && inputPass.text == password)
        {

            Debug.Log("You Has Been Login !");
            error_msg.text = "<color=lime>Congratulation You Has Been Login !</color>";

        }
        else
        {

            Debug.LogError("Your Email & Password was not found !");
            error_msg.text = "<color=red>Sorry, Your Email & Password was not found !</color>";

        }

    }

}
