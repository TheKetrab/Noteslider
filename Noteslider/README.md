```

-- Assets --
  | Converter (Provides conversion from BinaryAsset to concrete Asset)
      | _AssetConverter.cs
      | _IBinAssetConverter.cs
      | ImageAssetConverter.cs
      | PdfAssetconverter.cs
      | TextAssetconverter.cs
  | Model (What kind of data is stored)
      | _Asset.cs
      | _DocAsset.cs
      | ImageAsset.cs
      | JpgAsset.cs
      | PdfAsset.cs
      | PngAsset.cs
      | TextAsset.cs
      | WebAsset.cs
  | Renderer (How to render concrete asset?)
      | _AssetRenderer.cs
      | _AssetRendererFactory.cs
      | ImageAssetRenderer.cs
      | PdfAssetRenderer.cs
      | TextAssetRenderer.cs
      | WebAssetRenderer.cs
  | _BinaryAsset.cs

-- Code --
  | Animator
      | _BaseAnimator.cs
      | MarginAnimator.cs
  | Controls (Custom WPF controls)
      | InfoDialog.cs
      | NewsItem.cs
      | UniformTabPanel.cs
  | Exceptions
      | AssetConverterException.cs
      | ForUserException.cs
      | NewTrackDialogException.cs
      | ServerIsDownException.cs
   | Converter.cs
   | Cryptography.cs
   | Extension.cs (Extension methods)
   | LanguageManager.cs
   | Library.cs
   | Paths.cs
   | Program.cs (Main func)
   | ResourceHelper.cs
   | ServerManager.cs
   | Track.cs
   | TrackRenderer.cs

-- Events --
  | EventAgregator.cs
  | MainWindowSubscriber.cs
  | MenuEvents.cs
  | TrackEvents.cs

-- View --
  | Style
      | StyleBase.xaml
      | StyleOverlayNTD.xaml
      | StyleOverlayOTD.xaml
      | StyleStretchingTab.xaml
      | StyleTitleBar.xaml
  | MainWindow.xaml
  | NewTrackDialog.xaml
  | OpenTrackDialog.xaml


```