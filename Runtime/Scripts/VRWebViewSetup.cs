using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System;

namespace VRWebView
{
    public class VRWebViewSetup : MonoBehaviour
    {
        [Header("Prefab References")]
        [SerializeField] private GameObject webViewPrefab;
        [SerializeField] private GameObject urlInputPrefab;
        
        [Header("Setup Settings")]
        [SerializeField] private Vector3 webViewPosition = new Vector3(0, 1.5f, 2f);
        [SerializeField] private Vector3 urlInputPosition = new Vector3(0, 1.2f, 2f);
        [SerializeField] private float maxDistance = 5f;
        
        private GameObject webViewInstance;
        private GameObject urlInputInstance;
        private bool isSetup = false;
        
        private void Start()
        {
            if (!ValidateSetup())
            {
                Debug.LogError("VRWebViewSetup validation failed");
                return;
            }
            
            SetupWebView();
        }
        
        private bool ValidateSetup()
        {
            if (webViewPrefab == null)
            {
                Debug.LogError("WebView prefab reference is missing");
                return false;
            }
            
            if (urlInputPrefab == null)
            {
                Debug.LogError("URL input prefab reference is missing");
                return false;
            }
            
            // Check if prefabs have required components
            if (webViewPrefab.GetComponent<VRWebView>() == null)
            {
                Debug.LogError("WebView prefab is missing VRWebView component");
                return false;
            }
            
            if (urlInputPrefab.GetComponent<TMP_InputField>() == null)
            {
                Debug.LogError("URL input prefab is missing TMP_InputField component");
                return false;
            }
            
            return true;
        }
        
        public void SetupWebView()
        {
            if (isSetup)
            {
                Debug.LogWarning("WebView is already set up");
                return;
            }
            
            try
            {
                // Create WebView object
                webViewInstance = Instantiate(webViewPrefab, transform);
                webViewInstance.transform.localPosition = webViewPosition;
                
                // Create URL input
                urlInputInstance = Instantiate(urlInputPrefab, transform);
                urlInputInstance.transform.localPosition = urlInputPosition;
                
                // Get references
                VRWebView webView = webViewInstance.GetComponent<VRWebView>();
                TMP_InputField urlInput = urlInputInstance.GetComponent<TMP_InputField>();
                
                // Setup references
                if (webView != null && urlInput != null)
                {
                    // Set up the URL input field reference
                    // Note: You'll need to implement this in your VRWebView component
                    // webView.SetUrlInputField(urlInput);
                }
                else
                {
                    Debug.LogError("Failed to get required components from instantiated objects");
                    return;
                }
                
                // Configure XR components
                XRGrabInteractable grabInteractable = webViewInstance.GetComponent<XRGrabInteractable>();
                if (grabInteractable != null)
                {
                    // Configure grab settings
                    grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
                    grabInteractable.throwOnDetach = false;
                    
                    // Set up interaction distance
                    var interactor = grabInteractable.interactionLayers;
                    var distanceInteractor = webViewInstance.GetComponent<XRDistanceInteractor>();
                    if (distanceInteractor != null)
                    {
                        distanceInteractor.maxDistance = maxDistance;
                    }
                }
                
                isSetup = true;
                Debug.Log("WebView setup completed successfully");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error during WebView setup: {e.Message}");
                Cleanup();
            }
        }
        
        private void Cleanup()
        {
            if (webViewInstance != null)
            {
                Destroy(webViewInstance);
            }
            
            if (urlInputInstance != null)
            {
                Destroy(urlInputInstance);
            }
            
            isSetup = false;
        }
        
        private void OnDestroy()
        {
            Cleanup();
        }
    }
} 