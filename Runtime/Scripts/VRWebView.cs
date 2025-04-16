using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using TMPro;
using System.Text.RegularExpressions;

namespace VRWebView
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class VRWebView : MonoBehaviour
    {
        [Header("WebView Settings")]
        [SerializeField] private string defaultUrl = "https://docs.google.com/presentation/";
        [SerializeField] private float webViewWidth = 1.5f;
        [SerializeField] private float webViewHeight = 0.9f;
        [SerializeField] private float maxDistance = 5f; // Maximum distance for interaction
        
        [Header("UI References")]
        [SerializeField] private TMP_InputField urlInputField;
        [SerializeField] private GameObject webViewObject;
        [SerializeField] private GameObject loadingIndicator;
        
        private XRGrabInteractable grabInteractable;
        private string currentUrl;
        private bool isInitialized = false;
        
        // URL validation patterns
        private const string GoogleSlidesPattern = @"^https:\/\/docs\.google\.com\/presentation\/d\/[^\/]+\/.*$";
        private const string YouTubePattern = @"^https:\/\/(?:www\.)?youtube\.com\/watch\?v=[^&]+.*$";
        
        private void Awake()
        {
            if (!TryGetComponent(out grabInteractable))
            {
                Debug.LogError("XRGrabInteractable component is required on VRWebView object");
                enabled = false;
                return;
            }
            
            InitializeWebView();
        }
        
        private void InitializeWebView()
        {
            if (webViewObject == null)
            {
                Debug.LogError("WebView object reference is missing");
                return;
            }
            
            if (urlInputField != null)
            {
                urlInputField.onEndEdit.AddListener(OnUrlSubmitted);
                urlInputField.text = defaultUrl;
            }
            else
            {
                Debug.LogWarning("URL input field reference is missing");
            }
            
            currentUrl = defaultUrl;
            isInitialized = true;
            
            // Configure grab interactable
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
            
            LoadUrl(currentUrl);
        }
        
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            // Add any special handling when the WebView is grabbed
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(true);
            }
        }
        
        private void OnSelectExited(SelectExitEventArgs args)
        {
            // Add any special handling when the WebView is released
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }
        
        public void OnUrlSubmitted(string url)
        {
            if (!isInitialized) return;
            
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogWarning("Empty URL submitted");
                return;
            }
            
            // Validate and format URL
            url = FormatUrl(url);
            if (!IsValidUrl(url))
            {
                Debug.LogError($"Invalid URL format: {url}");
                return;
            }
            
            currentUrl = url;
            LoadUrl(currentUrl);
        }
        
        private string FormatUrl(string url)
        {
            // Add https:// if not present
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "https://" + url;
            }
            
            // Handle Google Slides URL
            if (url.Contains("docs.google.com/presentation") && !url.Contains("/edit"))
            {
                url = url.Replace("/view", "/edit");
            }
            
            return url;
        }
        
        private bool IsValidUrl(string url)
        {
            // Check if URL matches either Google Slides or YouTube pattern
            return Regex.IsMatch(url, GoogleSlidesPattern) || 
                   Regex.IsMatch(url, YouTubePattern);
        }
        
        private void LoadUrl(string url)
        {
            if (!isInitialized) return;
            
            try
            {
                // Show loading indicator
                if (loadingIndicator != null)
                {
                    loadingIndicator.SetActive(true);
                }
                
                // Implement WebView loading logic here
                Debug.Log($"Loading URL: {url}");
                
                // Example implementation with UniWebView:
                // webViewObject.Load(url);
                
                // Hide loading indicator after a delay
                if (loadingIndicator != null)
                {
                    Invoke(nameof(HideLoadingIndicator), 1f);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading URL: {e.Message}");
                if (loadingIndicator != null)
                {
                    loadingIndicator.SetActive(false);
                }
            }
        }
        
        private void HideLoadingIndicator()
        {
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }
        
        public void ToggleWebView()
        {
            if (!isInitialized) return;
            
            if (webViewObject != null)
            {
                webViewObject.SetActive(!webViewObject.activeSelf);
            }
        }
        
        private void OnDestroy()
        {
            if (urlInputField != null)
            {
                urlInputField.onEndEdit.RemoveListener(OnUrlSubmitted);
            }
            
            if (grabInteractable != null)
            {
                grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
                grabInteractable.selectExited.RemoveListener(OnSelectExited);
            }
        }
    }
} 