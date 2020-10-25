﻿using System.Collections;
using UnityEngine;

/*
*  Carter Games Utilities Script
*  Copyright (c) Carter Games
*  W: https://carter.games
*  
*  Written by:
*  Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games
*/

namespace CarterGames.Utilities
{
    /// <summary>
    /// CLASS | Camera Shake Script, make a camera shake effect.
    /// </summary>
    public class CameraShakeScript : MonoBehaviour
    {
        /// <summary>
        /// Defines how long the camera should shake for.
        /// </summary>
        [Tooltip("The duration for the camera shake effect.")]
        [SerializeField] private float shakeDuration;

        /// <summary>
        /// Defines whether or not the camera should shake.
        /// </summary>
        [Tooltip("Should the camera be shaken.")]
        [SerializeField] private bool shouldCameraShake;

        /// <summary>
        /// Should the camera shake it 2d or 3d space?
        /// </summary>
        private bool is2D;

        /// <summary>
        /// How much the camera should shake, is set to the force defined by the user in the Shake Camera Method.
        /// </summary>
        private float shakeAmount;

        /// <summary>
        /// Used to check if the camera starting position is saved before it is shaked.
        /// </summary>
        private bool isCameraPositionSaved;

        /// <summary>
        /// Used to check if the coroutine is running or not.
        /// </summary>
        private bool isCoRunning;

        /// <summary>
        /// Used to save the camera position before the shake effect is started, so it can be returned to its starting position.
        /// </summary>
        private Vector3 cameraPosition;

        /// <summary>
        /// The camera to shake.
        /// </summary>
        private GameObject mainCamera;


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity | OnDisable Method
        ///   Stops all coroutines running on this script when disabled, stops any memory waste.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            StopAllCoroutines();
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity | Start Method
        ///   Assigns the camera variable to the object in the scene.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private void Start()
        {
            // Reference to the main camera
            mainCamera = gameObject;
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity | Update Method  
        ///   Checks to see if the camera shake should happen.
        ///   Starts the Coroutine that shakes the camera when called.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (shouldCameraShake && !is2D)
            {
                mainCamera.transform.localPosition += new Vector3(Random.insideUnitSphere.x * shakeAmount, Random.insideUnitSphere.y * shakeAmount, Random.insideUnitSphere.z * shakeAmount);
            }
            else if (shouldCameraShake && is2D)
            {
                mainCamera.transform.localPosition += new Vector3(Random.insideUnitSphere.x * shakeAmount, Random.insideUnitSphere.y * shakeAmount, mainCamera.transform.position.z);
            }

            if ((shouldCameraShake) && (!isCoRunning))
            {
                StartCoroutine(StopCameraShake());
            }
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Coroutine | Stop Camera Shake
        ///   Stops the camera shake effect after the set duration is completed.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------
        private IEnumerator StopCameraShake()
        {
            isCoRunning = true;
            yield return new WaitForSeconds(shakeDuration);
            shouldCameraShake = false;
            isCoRunning = false;
            transform.localPosition = cameraPosition;
            isCameraPositionSaved = false;
        }


        /// ------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Shake Camera
        ///   Running the method will shake the camera with the defined values.
        /// </summary>
        /// <param name="shakeForce">the amount of force to apply. Default = 0.1f</param>
        /// <param name="shakeLenght">the duration for the effect. Default = 0.25f</param>
        /// ------------------------------------------------------------------------------------------------------
        public void ShakeCamera(bool should2D = false, float shakeForce = .1f, float shakeLenght = .25f)
        {
            // Only edited if inputted, otherwise defaulf values are entered
            is2D = should2D;
            shakeAmount = shakeForce;
            shakeDuration = shakeLenght;

            if (!isCameraPositionSaved)
            {
                cameraPosition = transform.localPosition;
                isCameraPositionSaved = true;
            }

            if (!isCoRunning)
            {
                shouldCameraShake = true;
            }
        }
    }
}