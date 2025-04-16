using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

namespace VRWebView.Samples
{
    /// <summary>
    /// Helper script to create and configure the basic WebView prefab.
    /// Add this script to an empty GameObject and run the Setup method in the editor.
    /// </summary>
    public class WebViewPrefabSetup : MonoBehaviour
    {
        [Header("Prefab Settings")]
        [SerializeField] private string prefabName = "WebViewDisplay";
        [SerializeField] private Vector3 defaultPosition = new Vector3(0, 1.5f, 2f);
        [SerializeField] private Vector3 defaultScale = new Vector3(1.5f, 0.9f, 0.1f);
        
        [Header("UI Settings")]
        [SerializeField] private Vector3 urlInputPosition = new Vector3(0, -0.6f, 0);
        [SerializeField] private Vector2 urlInputSize = new Vector2(1.2f, 0.1f);
        
        [Header("References")]
        [SerializeField] private GameObject webViewPrefab;
        [SerializeField] private GameObject urlInputPrefab;
        
        /// <summary>
        /// Creates and configures the WebView prefab with all necessary components.
        /// </summary>
        public void SetupPrefab()
        {
            // Create the main WebView object
            GameObject webViewObject = new GameObject(prefabName);
            webViewObject.transform.SetParent(transform);
            webViewObject.transform.localPosition = defaultPosition;
            webViewObject.transform.localScale = defaultScale;
            
            // Add required components
            VRWebView webView = webViewObject.AddComponent<VRWebView>();
            XRGrabInteractable grabInteractable = webViewObject.AddComponent<XRGrabInteractable>();
            
            // Configure grab interactable
            grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
            grabInteractable.throwOnDetach = false;
            
            // Create URL input
            GameObject urlInputObject = new GameObject("URLInput");
            urlInputObject.transform.SetParent(webViewObject.transform);
            urlInputObject.transform.localPosition = urlInputPosition;
            
            // Add URL input components
            Canvas canvas = urlInputObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = Camera.main;
            
            RectTransform rectTransform = urlInputObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = urlInputSize;
            
            TMP_InputField inputField = urlInputObject.AddComponent<TMP_InputField>();
            
            // Create input field background
            GameObject background = new GameObject("Background");
            background.transform.SetParent(urlInputObject.transform);
            background.transform.localPosition = Vector3.zero;
            
            Image backgroundImage = background.AddComponent<Image>();
            backgroundImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            
            RectTransform bgRectTransform = background.GetComponent<RectTransform>();
            bgRectTransform.anchorMin = Vector2.zero;
            bgRectTransform.anchorMax = Vector2.one;
            bgRectTransform.sizeDelta = Vector2.zero;
            
            // Create input field text
            GameObject textObject = new GameObject("Text");
            textObject.transform.SetParent(urlInputObject.transform);
            textObject.transform.localPosition = Vector3.zero;
            
            TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
            textComponent.color = Color.white;
            textComponent.fontSize = 0.1f;
            textComponent.alignment = TextAlignmentOptions.Left;
            
            RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.sizeDelta = new Vector2(-20, -10);
            
            // Setup input field references
            inputField.targetGraphic = backgroundImage;
            inputField.textComponent = textComponent;
            
            // Create loading indicator
            GameObject loadingIndicator = new GameObject("LoadingIndicator");
            loadingIndicator.transform.SetParent(webViewObject.transform);
            loadingIndicator.transform.localPosition = Vector3.zero;
            
            Image loadingImage = loadingIndicator.AddComponent<Image>();
            loadingImage.color = new Color(1, 1, 1, 0.5f);
            
            // Setup WebView references
            webView.urlInputField = inputField;
            webView.webViewObject = webViewObject;
            
            // Save as prefab
            #if UNITY_EDITOR
            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(webViewObject))
            {
                string prefabPath = $"Assets/{prefabName}.prefab";
                UnityEditor.PrefabUtility.SaveAsPrefabAsset(webViewObject, prefabPath);
                Debug.Log($"Prefab created at: {prefabPath}");
            }
            #endif
        }
    }
} 