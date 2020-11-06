using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using UnityEngine.Video;
public class SceneController : MonoBehaviour
{
    List<Vector3> ballPList;
    public VideoPlayer videoPlayer;
    public Transform ball;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset text=Resources.Load<TextAsset>("zero_text_2019-10-08_1570529869269-24945");
        Debug.Log(text.text);
        JArray array = JArray.Parse(text.text);
        ballPList = new List<Vector3>();
        /*
        for(int i=0;i<array.Count;i++)
        {
            JObject obj = (JObject)array[i];
            JObject footBallP= (JObject)(((JObject)(((JArray)(obj.GetValue("footballs")))[0])).GetValue("playground_coordinate"));
            ballPList.Add(new Vector3((float)footBallP.GetValue("x"), (float)footBallP.GetValue("y"), (float)footBallP.GetValue("z")));
        }
        */
        for (int i = 0; i < array.Count; i++)
        {
            JObject obj = (JObject)array[i];
            JArray playerArray = (JArray)(obj.GetValue("players"));
            foreach(JObject player in playerArray)
            {
                if(((int)(player.GetValue("virtual_id")))==6)
                {
                    JObject coo = (JObject)(player.GetValue("playground_coordinate"));
                    ballPList.Add(new Vector3((float)coo.GetValue("x"), (float)coo.GetValue("y"), (float)coo.GetValue("z")));
                }
            }
        }
        Debug.Log(ballPList.Count);
        videoPlayer.frameReady += frameReady;
        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void frameReady(VideoPlayer source, long frameIdx)
    {
        Vector3 p= ballPList[(int)frameIdx];
        //ball.position = new Vector3(-p.y,0, p.x);
        Debug.Log(p);
    }
}
