using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick_game_manager : MonoBehaviour
{
    enum game_state{game_over,launch,game,pause,restart,win};
    private game_state actual_game_state;
    public int nb_ball;
    private int number_of_brick_remaining;

    public GameObject ball_go;
    public float scale_ball_UI = 2f;

    private List<GameObject> liste_ball_to_draw;

    public bool is_pause = true;
    public Transform position_ball_UI;
    public float spacing_UI = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        liste_ball_to_draw = new List<GameObject>();
        initiate_ball_UI();
        actual_game_state = game_state.launch;

    }

    // Update is called once per frame
    void Update()
    {
        number_of_brick_remaining = GetComponentsInChildren<brick>().Length;

        if(number_of_brick_remaining ==0) actual_game_state = game_state.win;

        switch(actual_game_state){
            case game_state.launch :
                is_pause = true;
                if(Input.GetKeyDown(KeyCode.Space)) {
                    actual_game_state = game_state.game;
                    GetComponentInChildren<ball_movement>().impulse_ball();
                }
                break;

            case game_state.game :
                is_pause = false;
                if(Input.GetKeyDown(KeyCode.Space)){
                    actual_game_state = game_state.pause;
                }
                break;

            case game_state.pause :
                is_pause = true;
                if(Input.GetKeyDown(KeyCode.Space)){
                    actual_game_state = game_state.game;
                    GetComponentInChildren<ball_movement>().impulse_ball();

                }

                break;

            case game_state.game_over :
                is_pause = true;
                break;

            case game_state.restart :
                is_pause = true;
                break;
        }

        
    }

    void launch_game(){
        // Vector2 mousePos = Input.mousePosition;
    }

    public void loose_ball(){
        nb_ball--;

        GameObject go_to_hide = liste_ball_to_draw[nb_ball];
        go_to_hide.GetComponent<SpriteRenderer>().enabled = false;



        if (nb_ball ==0){
            GetComponentInChildren<ball_movement>().kill();
            actual_game_state = game_state.game_over;
            return;
        }
        actual_game_state = game_state.restart;
        StartCoroutine(next_ball()); 
    }

    IEnumerator next_ball(){
        // when you lost a ball
        // begin state
        yield return new WaitForSeconds(2f);
        actual_game_state = game_state.game;
        GetComponentInChildren<ball_movement>().impulse_ball();


    }


    void draw_lives(){
        foreach(GameObject go in liste_ball_to_draw){
            Instantiate(go,go.transform,transform);
        }
    }

    void initiate_ball_UI(){
        for(int i = 0; i<nb_ball; i++){
            GameObject go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            SpriteRenderer spr = go.GetComponent<SpriteRenderer>();
            spr.sprite = ball_go.GetComponent<SpriteRenderer>().sprite;
            spr.sortingOrder = 2;
            go.transform.localScale = ball_go.transform.localScale*scale_ball_UI;
            go.transform.position = position_ball_UI.position;
            go.transform.parent = transform;
            go.transform.Translate(Vector3.right*i*spacing_UI);
            
            liste_ball_to_draw.Add(go);
        }
    }


}
