using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingProductLine : MonoBehaviour
{
    public EnvManage envManager;
    float scrollSpeed = 5.1f;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (envManager.player.playerData.avatarData1.state == Constant.AvatarState.WORKING ||
            envManager.player.playerData.avatarData2.state == Constant.AvatarState.WORKING ||
            envManager.player.playerData.avatarData3.state == Constant.AvatarState.WORKING)
        {
            float newPos = Mathf.Repeat(Time.time * scrollSpeed, 12.48f);
            transform.position = startPos + Vector2.right * newPos;
        }
    }
}
