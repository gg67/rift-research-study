using UnityEngine;
using System.Collections;

public class CrossHairControls : MonoBehaviour {
	public float 	FadeInTime    		= 2.0f;
	public Texture 	FadeInTexture 		= null;

	// Handle to OVRCameraController
	private OVRCameraController CameraController = null;
	
	// Handle to OVRPlayerController
	private OVRPlayerController PlayerController = null;	
	
	private float  AlphaFadeValue	= 1.0f;
	
	// Replace the GUI with our own texture and 3D plane that
	// is attached to the rendder camera for true 3D placement
	private OVRGUI  		GuiHelper 		 = new OVRGUI();
	private GameObject      GUIRenderObject  = null;
	private RenderTexture	GUIRenderTexture = null;	
	// Crosshair system, rendered onto 3D plane
	public Texture  CrosshairImage 			= null;
	private OVRCrosshair Crosshair        	= new OVRCrosshair();
	
	// Create a delegate for update functions
	private delegate void updateFunctions();	
	private updateFunctions UpdateFunctions;
	
	// Awake
	void Awake()
	{
		// Find camera controller
		OVRCameraController[] CameraControllers;
		CameraControllers = gameObject.GetComponentsInChildren<OVRCameraController>();
		
		if(CameraControllers.Length == 0)
			Debug.LogWarning("OVRMainMenu: No OVRCameraController attached.");
		else if (CameraControllers.Length > 1)
			Debug.LogWarning("OVRMainMenu: More then 1 OVRCameraController attached.");
		else
			CameraController = CameraControllers[0];
	
		// Find player controller
		OVRPlayerController[] PlayerControllers;
		PlayerControllers = gameObject.GetComponentsInChildren<OVRPlayerController>();
		
		if(PlayerControllers.Length == 0)
			Debug.LogWarning("OVRMainMenu: No OVRPlayerController attached.");
		else if (PlayerControllers.Length > 1)
			Debug.LogWarning("OVRMainMenu: More then 1 OVRPlayerController attached.");
		else
			PlayerController = PlayerControllers[0];
	
	}
	
	// Use this for initialization
	void Start () {
		// Crosshair functionality
		Crosshair.Init();
		Crosshair.SetCrosshairTexture(ref CrosshairImage);
		Crosshair.SetOVRCameraController (ref CameraController);
		Crosshair.SetOVRPlayerController(ref PlayerController);
		UpdateFunctions += Crosshair.UpdateCrosshair;
	}
	
	void Update() {
		UpdateFunctions();
	}
	
	void OnGUI() {
		// Important to keep from skipping render events
		if (Event.current.type != EventType.Repaint)
			return;

		// Fade in screen
		if(AlphaFadeValue > 0.0f)
		{
  			AlphaFadeValue -= Mathf.Clamp01(Time.deltaTime / FadeInTime);
			if(AlphaFadeValue < 0.0f)
			{
				AlphaFadeValue = 0.0f;	
			}
			else
			{
				GUI.color = new Color(0, 0, 0, AlphaFadeValue);
  				GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), FadeInTexture ); 
				return;
			}
		}
		
		// We can turn on the render object so we can render the on-screen menu
		if(GUIRenderObject != null)
		{
			if (Crosshair.IsCrosshairVisible())
				GUIRenderObject.SetActive(true);
			else
				GUIRenderObject.SetActive(false);
		}
		
		//***
		// Set the GUI matrix to deal with portrait mode
		Vector3 scale = Vector3.one;
		if(CameraController.PortraitMode == true)
		{
			float h = OVRDevice.HResolution;
			float v = OVRDevice.VResolution;
			scale.x = v / h; 					// calculate hor scale
    		scale.y = h / v; 					// calculate vert scale
		}
		Matrix4x4 svMat = GUI.matrix; // save current matrix
    	// substitute matrix - only scale is altered from standard
    	GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		// Cache current active render texture
		RenderTexture previousActive = RenderTexture.active;
		
		// if set, we will render to this texture
		if(GUIRenderTexture != null)
		{
			RenderTexture.active = GUIRenderTexture;
			GL.Clear (false, true, new Color (0.0f, 0.0f, 0.0f, 0.0f));
		}
		
		Crosshair.OnGUICrosshair();
		
		// Restore active render texture
		RenderTexture.active = previousActive;
		
		// ***
		// Restore previous GUI matrix
		GUI.matrix = svMat;
	}
}
