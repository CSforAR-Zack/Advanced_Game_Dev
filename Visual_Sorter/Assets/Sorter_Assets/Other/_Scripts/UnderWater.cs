using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    [SerializeField] float seeInWaterDistance = .1f;
    [SerializeField] Material underWaterSkybox = null;

    private Material skybox = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {            
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Vector4(0.2f, 0.4f, 0.4f);
            RenderSettings.fogDensity = seeInWaterDistance;

            skybox = RenderSettings.skybox;
            RenderSettings.skybox = underWaterSkybox;
        }
        if(other.tag == "Head")
        {
            other.gameObject.GetComponentInParent<PlayerMotor>().HeadAboveWaterCheck(false);
        }
        if (other.tag == "Body")
        {
            other.gameObject.GetComponentInParent<PlayerControl>().ChangeSwimmingMode(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {            
            RenderSettings.fog = false;
            RenderSettings.skybox = skybox;
        }
        if(other.tag == "Head")
        {
            other.gameObject.GetComponentInParent<PlayerMotor>().HeadAboveWaterCheck(true);
        }
        if (other.tag == "Body")
        {
            other.gameObject.GetComponentInParent<PlayerControl>().ChangeSwimmingMode(false);
        }
    }
}
