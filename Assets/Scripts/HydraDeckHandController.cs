/*  HydraDeck camera code by Teddy0@gmail.com
  
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using UnityEngine;
using System.Collections;

public class HydraDeckHandController : SixenseHandController
{
    protected Quaternion m_modelRotation = new Quaternion(0.0f, 0.0f, 1.0f, -1.0f);

    protected override void UpdateObject(SixenseInput.Controller controller)
    {
        if (m_animator == null)
            return;

//        GameObject CameraBase = GameObject.Find("CameraBase");
//        if ( CameraBase == null)
//            return;
//
//        HydraDeckCamera DeckCamera = CameraBase.GetComponentInChildren<HydraDeckCamera>();
		GameObject CameraBase = GameObject.Find("OVRPlayerController");
		if(CameraBase == null)
			return;
		
		HydraDeckCamera DeckCamera = CameraBase.GetComponent<HydraDeckCamera>();
        if (DeckCamera == null)
            return;

        if (controller.Enabled)
        {
            // Animation update
            UpdateAnimationInput(controller);
        }

        if (!m_enabled && DeckCamera.State == HydraDeckCamera.CameraState.Enabled)
        {
            // enable position and orientation control
            m_enabled = !m_enabled;
        }

        if (m_enabled)
        {
            UpdatePosition(controller, DeckCamera);
            UpdateRotation(controller);
        }
    }

    // Updates the animated object from controller input.
    new protected void UpdateAnimationInput(SixenseInput.Controller controller)
    {
        // Point
        if (controller.GetButton(SixenseButtons.BUMPER))
        {
            m_animator.SetBool("Point", true);
        }
        else
        {
            m_animator.SetBool("Point", false);
        }

        // HoldBook
        if (controller.GetButton(SixenseButtons.START))
        {
            m_animator.SetBool("HoldBook", true);
        }
        else
        {
            m_animator.SetBool("HoldBook", false);
        }

        // Fist
        float fTriggerVal = controller.Trigger;
        fTriggerVal = Mathf.Lerp(m_fLastTriggerVal, fTriggerVal, 0.1f);
        m_fLastTriggerVal = fTriggerVal;

        if (fTriggerVal > 0.01f)
        {
            m_animator.SetBool("Fist", true);
        }
        else
        {
            m_animator.SetBool("Fist", false);
        }

        m_animator.SetFloat("FistAmount", fTriggerVal);

        // Idle
        if (m_animator.GetBool("Fist") == false &&
             m_animator.GetBool("HoldBook") == false &&
             m_animator.GetBool("GripBall") == false &&
             m_animator.GetBool("Point") == false)
        {
            m_animator.SetBool("Idle", true);
        }
        else
        {
            m_animator.SetBool("Idle", false);
        }
    }

    protected void UpdatePosition(SixenseInput.Controller controller, HydraDeckCamera DeckCamera)
    {
        Vector3 BodyPosition = DeckCamera.BodyPosition;

        Vector3 controllerPosition = new Vector3(controller.Position.x,
                                                  controller.Position.y - DeckCamera.HydraFloorY, 
                                                  controller.Position.z);
		

        //Orientate it to our scene
        controllerPosition = DeckCamera.StartCameraOffset * controllerPosition;
		
        controllerPosition.Scale(DeckCamera.HydraToWorldScale);
		
        // update the localposition of the object
        this.gameObject.transform.position = BodyPosition + controllerPosition;
    }

    new protected void UpdateRotation(SixenseInput.Controller controller)
    {
        this.gameObject.transform.localRotation = m_initialRotation * controller.Rotation * m_modelRotation;
    }

    void OnGUI()
    {
    }

    //This is a good location for doing interactions, should be near the fingers
    public Vector3 GetInteractionPosition()
    {
        return transform.position + transform.forward * 0.1f;
    }

    public bool IsHandEnabled()
    {
        return m_enabled;
    }
}

