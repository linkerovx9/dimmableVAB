using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace DimmableVAB
{

	[KSPAddon (KSPAddon.Startup.EditorAny, false)]

	class dimvab : MonoBehaviour
	{
		public Light[] lights;
		public Camera[] cameras;
		public Color dimmed = new Color (0.1f, 0.1f, 0.1f); //new color
		public Color dc = RenderSettings.ambientLight;//default lighting color
		public bool vabcam = true;
		public GameScenes s;
		void Update ()
		{

			if (Input.GetKeyDown (KeyCode.L)) {
				s = HighLogic.LoadedScene; //log gamescene for reset

				//Lights, Camera, Action!
				lights = FindObjectsOfType (typeof(Light)) as Light[];
				cameras = FindObjectsOfType (typeof(Camera)) as Camera[];
				RenderSettings.ambientLight = Color.Lerp (dimmed, dc, 0); //dims ship
				List<Part> pl = EditorLogic.fetch.ship.parts; //get ship parts

				foreach (Part p in pl) {
					p.SendEvent ("LightsOn");//turn on ship lights
				}
					
				foreach (Camera camera in cameras) {
					if (camera.name == "sceneryCam" && vabcam == true) {
						camera.cullingMask = 1 << 0; // removes VAB/SPH
						camera.backgroundColor = Color.black;
						camera.clearFlags = CameraClearFlags.SolidColor;//And get rid of the skybox to make it darker
						vabcam = false;
					} else if (camera.name == "sceneryCam" && vabcam == false) {
						vabcam = true;
						foreach (Part p in pl) {
							p.SendEvent ("LightsOff");//turn off ship lights
						}
						HighLogic.LoadScene (s);//reload scene
					}
				}


				//toggle VAB/SPH lights
					foreach (Light light in lights) {
						if (light.name == "Point light") {
							light.enabled = !light.enabled;
						}
						if (light.name == "Scaledspace Sunlight") {
							light.enabled = !light.enabled;
						}
						if (light.name == "Sunlight") {
							light.enabled = !light.enabled;
						}
						if (light.name == "SpotlightSun") {
							light.enabled = !light.enabled;
						}
						if (light.name == "Spotlight") {
							light.enabled = !light.enabled;
						}
						if (light.name == "Directional light") {
							light.enabled = !light.enabled;
						}
						if (light.name == "ShadowLight") {
							light.enabled = !light.enabled;
						}
						if (light.name == "SpotlightExteriorSun") {
						light.enabled = !light.enabled;//SPH specific
						}
						if (light.name == "SpotlightCraft") {
						light.enabled = !light.enabled;//SPH specific
						}
						if (light.name == "SpotlightWindow") {
						light.enabled = !light.enabled;//SPH specific
						}

					}
			}
		}
	}
}

