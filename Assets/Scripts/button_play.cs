using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_play : MonoBehaviour
{
    public Sprite spr_down;
    public Sprite spr_up;

    public GameObject Start_menu;


    private void Start() {
    }

    private void OnMouseEnter() {
        GetComponent<SpriteRenderer>().color *= Color.cyan;
    }

    private void OnMouseExit() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnMouseOver() {

        if(Input.GetMouseButton(0)){
            GetComponent<SpriteRenderer>().sprite = spr_down;
        }
        else{
            GetComponent<SpriteRenderer>().sprite = spr_up;
        }
        if(Input.GetMouseButtonUp(0)){
            StartCoroutine(startGame());
            transform.parent.parent.GetComponent<brickGameThemeAudio>().gameStarted = true;
        }
    }

    IEnumerator startGame(){
        yield return new WaitForSeconds(1f);
        GetComponentInParent<brick_game_manager>().start_game();
        Start_menu.SetActive(false);
        yield return new WaitForSeconds(1f);
        // GetComponentInParent<brick_game_manager>().is_pause = false;
        
    }
}
