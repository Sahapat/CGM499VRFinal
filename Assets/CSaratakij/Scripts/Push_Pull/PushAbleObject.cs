using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace CSaratakij
{
    public class PushAbleObject : MonoBehaviour
    {
        [SerializeField]
        float moveForce;

        [SerializeField]
        VRInteractiveItem interactiveItem;

        [SerializeField]
        VRInput vrInput;

        [SerializeField]
        Vector3 size;

        [SerializeField]
        LayerMask layerMask;


        bool isPushing;
        bool isGazeOver;

        Rigidbody rigid;
        Collider[] hits;

        Vector2 inputVector;

        Vector3 direction;
        Vector3 perpendicularDirection;

#if UNITY_EDITOR
        void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, size);
            Handles.Label(transform.position, "Trigger Area");
        }
#endif

        void OnEnable()
        {
            _SubscribeEvents();
        }

        void OnDisable()
        {
            _UnsubscribeEvents();
        }

        void Awake()
        {
            _Initialize();
        }

        void Update()
        {
            _InputHandler();
        }

        void FixedUpdate()
        {
            hits = Physics.OverlapBox(transform.position, size * 0.5f, Quaternion.identity, layerMask);

            if (hits.Length <= 0) {
                isPushing = false; 
            }

            if (isPushing) {

                var moveForceX = moveForce;
                var moveForceY = moveForce;

                moveForceX *= inputVector.x;
                moveForceY *= inputVector.y;

                direction = hits[0].transform.forward;
                perpendicularDirection = Vector3.Cross(Vector3.up, direction);

                if (inputVector != Vector2.zero) {


                    if (inputVector.magnitude <= 1.0f) {
                        if (inputVector.x != 0.0f) {
                            rigid.velocity = (perpendicularDirection * moveForceX) * Time.fixedDeltaTime;
                        }
                        else if (inputVector.y != 0.0f) {
                            rigid.velocity = (direction * moveForceY) * Time.fixedDeltaTime;
                        }
                    }
                    else {
                        var velocity = Vector3.zero;

                        velocity += (perpendicularDirection * moveForceX) * Time.fixedDeltaTime;
                        velocity += (direction * moveForceY) * Time.fixedDeltaTime;

                        rigid.velocity = velocity;
                    }
                }
            }
        }

        void _Initialize()
        {
            rigid = GetComponent<Rigidbody>();
        }

        void _InputHandler()
        {
            Vector2 gearVRTouchpadAxis2D = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);

            bool moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
                                (OVRInput.Get(OVRInput.Button.One,OVRInput.Controller.RTrackedRemote)&&gearVRTouchpadAxis2D.y > 0.5f);

            bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)||
                                (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTrackedRemote) && gearVRTouchpadAxis2D.x < -0.5f);

            bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
                                (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTrackedRemote) && gearVRTouchpadAxis2D.x > 0.5f);

            bool moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
                                (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTrackedRemote) && gearVRTouchpadAxis2D.y < -0.5f);

            inputVector.x = (moveRight) ? 1.0f : (moveLeft) ? -1.0f : 0.0f;
            inputVector.y = (moveForward) ? 1.0f : (moveBack) ? -1.0f : 0.0f;
        }

        void _SubscribeEvents()
        {
            vrInput.OnDown += _OnDown;

            interactiveItem.OnOver += _OnOver;
            interactiveItem.OnOut += _OnOut;
        }

        void _UnsubscribeEvents()
        {
            vrInput.OnDown -= _OnDown;

            interactiveItem.OnOver -= _OnOver;
            interactiveItem.OnOut -= _OnOut;
        }

        void _OnDown()
        {
            if (hits.Length <= 0) { return; }
            if (isPushing) {
                isPushing = false;
            }
            else {
                if (isGazeOver) {
                    isPushing = true;
                    direction = (transform.position - hits[0].transform.position).normalized;
                    perpendicularDirection = Vector3.Cross(Vector3.up, direction).normalized;
                }
            }
        }

        void _OnOver()
        {
            isGazeOver = true;
        }

        void _OnOut()
        {
            isGazeOver = false;
        }
    }
}
