using UnityEngine;
using VRWebView;

namespace VRWebView.Samples
{
    public class BasicWebViewSetup : MonoBehaviour
    {
        [SerializeField] private VRWebViewSetup webViewSetup;
        
        void Start()
        {
            if (webViewSetup == null)
            {
                Debug.LogError("Please assign VRWebViewSetup component in inspector");
                return;
            }
            
            // Setup will be handled automatically by VRWebViewSetup
        }
    }
} 