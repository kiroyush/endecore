using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InputManager : ARBaseGestureInteractable
{
    //[SerializeField] private GameObject arObj;
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;//helps to identify the object by throwing rays
    [SerializeField] private GameObject crosshair;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();//stores the hit info by the raycast manager

    private Touch touch;
    private Pose pose;// gives the position and rotation of the object being hit by the rays
    
    // Start is called before the first frame update
    void Start()
    {

    }

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)
            return true;
        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;
        if(gesture.targetObject!= null || IsPointerOverUI(gesture))
        {
            return;
        }
        if (GestureTransformationUtility.Raycast(gesture.startPosition, _hits, trackableTypes: UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            GameObject placedObj = Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

            var anchorObject = new GameObject("PlacementAnchor");
            anchorObject.transform.position = pose.position;
            anchorObject.transform.rotation = pose.rotation;
            placedObj.transform.parent = anchorObject.transform;
        }
    }

    // Update is called once per frame
    // this checks wether or not there has been any kind of input from the user
    void FixedUpdate()
    {
        CrosshairCalculations();
    }
    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>(); 
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    void CrosshairCalculations()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));//sets the centre of the screen as the viewport
        //Ray ray = arCam.ScreenPointToRay(origin);
        
        if (GestureTransformationUtility.Raycast(origin, _hits, trackableTypes: UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);//rotation in x as the marker must be aligned with the horizontal plane

        }
       
    }
}
