using UnityEngine;
using System.Collections;

public class drawLineScipt : MonoBehaviour {
	struct Line{
		public Vector3 start { get; set;}
		public Vector3 end { get; set;}
	}
	public Material mat;
	Line[] data;
	AudioClip clip;
	AudioSource audioSource;
	float[] audioData;
	public GameObject bullet;

	void OnPostRender() {
		if (!mat)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things. In this case, we just want to use
			// a blend mode that inverts destination colors.			
			var shader = Shader.Find ("Hidden/Internal-Colored");
			mat = new Material (shader);
			mat.hideFlags = HideFlags.HideAndDontSave;
			// Set blend mode to invert destination colors.
			mat.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusDstColor);
			mat.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			// Turn off backface culling, depth writes, depth test.
			mat.SetInt ("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			mat.SetInt ("_ZWrite", 0);
			mat.SetInt ("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
		}

		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(GL.LINES);
		GL.Color(Color.green);
		for (int i = 0; i < data.Length; i++) {
			GL.Vertex(data [i].start);
			GL.Vertex (data [i].end);
		}
		GL.End();
		GL.PopMatrix();
	}
		
	void Start () {
		data = new Line[128];
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = Microphone.Start(null, true, 10, 44100);
		audioSource.loop = true; 
		while (!(Microphone.GetPosition("Built-in Microphone") > 0)){}
		audioSource.Play(); 

		audioData = new float[2048];

	}
	
	// Update is called once per frame
	void Update () {
		if (Microphone.GetPosition ("Built-in Microphone") > 0) {
			audioSource.GetOutputData (audioData, 0);
			int unit = audioData.Length / data.Length;
			float[] values = new float[unit];
			float value = 0;
			int j = 0;
			for (int i = 0; i < data.Length; i++) {
				if (i + 1 % unit == 0) {
					values [j] = value;
					j++;
					value = 0;
				}
				value += Mathf.Abs(audioData [i]);
			}

			for (int i = 0; i < values.Length; i++) {
			//	GameObject gj = Instantiate (bullet, new Vector3(10, 0, 0), transform.rotation) as GameObject;
			//	gj.GetComponent<Transform> ().localScale = new Vector3 (1, values [i] + 1.0f, 1);
				/*if (i == 0) {
					data [i].start = new Vector3 ((float)(i) / values.Length, values [i] + 0.2f, 0);
				} else {
					data [i].start = data [i - 1].end;
				}
				data[i].end = new Vector3 ((float)(i + 1) / values.Length, values [i] + 0.2f, 0);*/
			}
		}
	}
}
