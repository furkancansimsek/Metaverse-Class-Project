using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BNG;

public class QuizNextQuestion : MonoBehaviourPunCallbacks
{
    public List<GameObject> screens;
    public GameObject quiz;
    private int activeQuestion = 0;
    PhotonView pv;
    InputBridge input;
    private void Start() {
        pv = gameObject.GetPhotonView();
        input = InputBridge.Instance;
    }
    public void NextQuestion() {
        pv.RPC("RPC_NextQuestion",RpcTarget.AllBuffered);
        input.VibrateController(1,1,1,ControllerHand.Right);
    }
    public void CloseQuiz() {
        pv.RPC("RPC_CloseQuiz",RpcTarget.AllBuffered);
    }
    [PunRPC]
    void RPC_NextQuestion(){
        if(activeQuestion < screens.Count) {
            screens[activeQuestion].SetActive(false);
            activeQuestion++;
            screens[activeQuestion].SetActive(true);
        }
    }
    [PunRPC]
    void RPC_CloseQuiz(){
        quiz.SetActive(false);
    }
}
