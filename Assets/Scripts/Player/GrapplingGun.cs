using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    //Function uses springs and the already implemented gun to use as a grapple gun.
    
    [SerializeField] LayerMask grappleLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform cam;
    [SerializeField] Transform player;
    [SerializeField] float distance = 25f;
    [SerializeField] float maxMultiplier = 0.8f;
    [SerializeField] float minMultiplier = 0.25f;
    [SerializeField] float springValue = 4.5f;
    [SerializeField] float dampingValue = 7f;
    [SerializeField] float massScaleValue = 4.5f;
    [SerializeField] int linePosition = 2;

    LineRenderer lineRender;
    Vector3 grapplePoint;
    SpringJoint grappleJoint;
    int resetPositionCount = 0;
   
    //Line renderer used to draw the rope onto the screen
    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    //Checks if the user is clicking the right button to grapple.
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Grapple();
        }else if (Input.GetMouseButtonUp(1))
        {
            EndGrapple();
        }
    }

    
    private void LateUpdate()
    {
        RenderRope();
    }

    //Renders the rope onto the screen.
    void RenderRope()
    {
        if (!grappleJoint)
        {
            return;
        }
        lineRender.SetPosition(0, attackPoint.position);
        lineRender.SetPosition(1, grapplePoint);
    }

    //Sets up the grapple as a joint added to the end of the players gun
    //Uses springs and damping in order to make the effect more realistic.
    void SetGrappleJoints(Vector3 point)
    {
        grappleJoint = player.gameObject.AddComponent<SpringJoint>();
        grappleJoint.autoConfigureConnectedAnchor = false;
        grappleJoint.connectedAnchor = point;

        float pointDistance = Vector3.Distance(player.position, grapplePoint);
        grappleJoint.maxDistance = pointDistance * maxMultiplier;
        grappleJoint.minDistance = pointDistance * minMultiplier;

        grappleJoint.spring = springValue;
        grappleJoint.damper = dampingValue;
        grappleJoint.massScale = massScaleValue;
    }
    
    //Checks if the user is grappling onto the right surface.
    void Grapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, distance, grappleLayer))
        {
            grapplePoint = hit.point;
            SetGrappleJoints(grapplePoint);
            lineRender.positionCount = linePosition;
        }
    }

    //Stops the grapple once the mouse button has been released.
    void EndGrapple()
    {
        lineRender.positionCount = resetPositionCount;
        Destroy(grappleJoint);
    }
}
