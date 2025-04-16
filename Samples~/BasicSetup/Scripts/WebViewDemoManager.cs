using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

namespace VRWebView.Samples
{
    /// <summary>
    /// Demo manager for the basic WebView sample scene.
    /// Shows how to set up and control a WebView in VR.
    /// </summary>
    public class WebViewDemoManager : MonoBehaviour
    {
        [Header("WebView References")]
        [SerializeField] private VRWebView webView;
        [SerializeField] private TMP_InputField urlInput;
        [SerializeField] private Transform spawnPoint;
        
        [Header("Demo Content")]
        [SerializeField] private string[] demoUrls = new string[]
        {
            "https://docs.google.com/presentation/d/example1",
            "https://docs.google.com/presentation/d/example2",
            "https://www.youtube.com/watch?v=example"
        };
        
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private GameObject loadingIndicator;
        
        private int currentDemoIndex = 0;
        
        private void Start()
        {
            if (!ValidateReferences())
            {
                enabled = false;
                return;
            }
            
            SetupWebView();
            SetupUI();
        }
        
        private bool ValidateReferences()
        {
            if (webView == null)
            {
                Debug.LogError("WebView reference is missing!");
                return false;
            }
            
            if (urlInput == null)
            {
                Debug.LogError("URL Input reference is missing!");
                return false;
            }
            
            return true;
        }
        
        private void SetupWebView()
        {
            // Position the WebView at the spawn point if specified
            if (spawnPoint != null)
            {
                webView.transform.position = spawnPoint.position;
                webView.transform.rotation = spawnPoint.rotation;
            }
            
            // Load the first demo URL
            LoadDemoUrl(0);
        }
        
        private void SetupUI()
        {
            urlInput.onEndEdit.AddListener(OnUrlSubmitted);
            
            if (statusText != null)
            {
                statusText.text = "WebView Demo Ready";
            }
            
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }
        
        public void LoadNextDemo()
        {
            currentDemoIndex = (currentDemoIndex + 1) % demoUrls.Length;
            LoadDemoUrl(currentDemoIndex);
        }
        
        public void LoadPreviousDemo()
        {
            currentDemoIndex--;
            if (currentDemoIndex < 0) currentDemoIndex = demoUrls.Length - 1;
            LoadDemoUrl(currentDemoIndex);
        }
        
        private void LoadDemoUrl(int index)
        {
            if (index < 0 || index >= demoUrls.Length) return;
            
            string url = demoUrls[index];
            urlInput.text = url;
            LoadUrl(url);
        }
        
        private void OnUrlSubmitted(string url)
        {
            LoadUrl(url);
        }
        
        private void LoadUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(true);
            }
            
            if (statusText != null)
            {
                statusText.text = "Loading...";
            }
            
            // The actual loading will be handled by the VRWebView component
            // This is just for demo purposes
            Invoke(nameof(OnLoadComplete), 2f);
        }
        
        private void OnLoadComplete()
        {
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
            
            if (statusText != null)
            {
                statusText.text = "Content Loaded";
            }
        }
        
        private void OnDestroy()
        {
            if (urlInput != null)
            {
                urlInput.onEndEdit.RemoveListener(OnUrlSubmitted);
            }
        }
    }
} 