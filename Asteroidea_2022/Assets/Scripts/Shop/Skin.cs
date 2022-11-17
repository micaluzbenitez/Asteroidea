using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins", menuName = "Skins/Skin")]
public class Skin : ScriptableObject
{
    public int ID = 0;
    public int cost = 0;
    public Sprite starUI = null;
    public Sprite body = null;
    public Sprite feet = null;
    public string animation = "";

    public void SetAnimation(Animator animator)
    {
        animator.SetBool("Skin1", false);
        animator.SetBool("Skin2", false);
        animator.SetBool("Skin3", false);
        animator.SetBool("Skin4", false);
        animator.SetBool("Skin5", false);
        animator.SetBool("Skin6", false);
        animator.SetBool(animation, true);
    }
}
