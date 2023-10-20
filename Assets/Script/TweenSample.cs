using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenSample : MonoBehaviour
{

    public int currentHP;
    public Tween delayTween;
    public Tween floatTween;

    private void Awake()
    {
        //3������
        delayTween = DOVirtual.DelayedCall(3f, () =>
        {
            Debug.Log("�T�����檺�F��");
        });

        //�R���o��Tween�����Ʊ�
        delayTween.Kill();
        //�Ȱ��o��Tween�����Ʊ�
        delayTween.Pause();
        //�~�����o��Tween�����Ʊ�
        delayTween.Play();


        //            (�}�l�Ʀr,�����Ʀr,��O���,(�ܼ�)=>{})
        floatTween = DOVirtual.Float(100f, 0f, 30f, (hp) =>
        {
            // hp �O�@���ܼơA�L�|�C�@�ճ��b�̭��]�@���ظ����{���X�Ahp�N�|�N��{�b�Ʀr�O�h�֤F
            currentHP = (int)hp;
        });


        //�o��Tween������n���檺�F��
        floatTween.OnComplete(() =>
        {
            Debug.Log("�˼Ƶ����F");
        });

        //���a����(�ؼЦ�m,���)
        transform.DOLocalMove(transform.localPosition + new Vector3(0, 5f, 0), 3f);



    }


}
