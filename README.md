# VR WebView Slides Package

A Unity package that enables displaying Google Slides presentations and YouTube videos in VR using WebView technology. Perfect for VR presentations, training sessions, and interactive learning environments.

## Features

- Display Google Slides presentations in VR
- Play YouTube videos in VR
- Interactive URL input system
- XR Interaction Toolkit integration
- Grabbable and interactive WebView display
- Multiplayer-ready
- Optimized for VR performance

## Requirements

- Unity 2022.3 or later
- XR Interaction Toolkit 2.5.0 or later
- XR Management 4.3.0 or later
- TextMeshPro 3.0.0 or later
- Input System 1.7.0 or later
- A WebView plugin (recommended: UniWebView)

## Installation

### Via Unity Package Manager (Recommended)
1. Open Unity Package Manager (Window > Package Manager)
2. Click the + button in the top-left corner
3. Select "Add package from git URL"
4. Enter: `https://github.com/rohansonawane/unity-webview.git`

### Manual Installation
1. Download this repository
2. Extract it into your project's `Packages` folder
3. Add required dependencies through the Package Manager

## Quick Start

1. Create a new scene or open an existing one
2. Add XR Origin to your scene if not present
3. Right-click in Hierarchy > VRWebView > WebView Display
4. Configure the WebView settings in the inspector:
   - Set default URL (Google Slides or YouTube)
   - Adjust WebView dimensions
   - Configure interaction settings

## Usage Examples

### Basic WebView Display
```csharp
// Attach this script to any GameObject
public class WebViewExample : MonoBehaviour
{
    [SerializeField] private VRWebView webView;
    
    void Start()
    {
        // Load a Google Slides presentation
        webView.LoadUrl("https://docs.google.com/presentation/d/YOUR_PRESENTATION_ID");
    }
}
```

### YouTube Video Player
```csharp
// Example of loading a YouTube video
webView.LoadUrl("https://www.youtube.com/watch?v=VIDEO_ID");
```

## Sample Scenes

### 1. Basic Setup (Samples~/BasicSetup)
- Simple WebView display setup
- URL input demonstration
- Basic interaction example

### 2. Multiplayer Integration (Samples~/Multiplayer)
- Synchronized WebView across multiple users
- Shared control demonstration
- Network state management

## Advanced Features

### Custom URL Validation
```csharp
public class CustomWebViewValidator : MonoBehaviour
{
    [SerializeField] private VRWebView webView;
    
    public bool ValidateUrl(string url)
    {
        // Add your custom validation logic
        return url.Contains("docs.google.com") || 
               url.Contains("youtube.com");
    }
}
```

### Interaction Settings
- Grab to move
- Pointer interaction
- Distance-based interaction
- Scale adjustment

## Best Practices

1. **Performance**
   - Keep WebView resolution reasonable
   - Use proper culling settings
   - Implement lazy loading when possible

2. **Security**
   - Validate URLs before loading
   - Implement proper access controls in multiplayer
   - Use HTTPS URLs only

3. **User Experience**
   - Provide clear loading indicators
   - Implement error handling
   - Add user feedback for interactions

## Troubleshooting

### Common Issues

1. **WebView Not Displaying**
   - Check if WebView plugin is properly installed
   - Verify URL format is correct
   - Check console for error messages

2. **Performance Issues**
   - Reduce WebView resolution
   - Check for memory leaks
   - Verify graphics settings

3. **Interaction Problems**
   - Verify XR Interaction Toolkit setup
   - Check interaction layers
   - Validate input system configuration

## Contributing

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This package is licensed under the MIT License. See the LICENSE file for details.

## Support

For issues and feature requests, please use the GitHub issues page. 