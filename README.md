# Noteslider

## Introduction
Noteslider is an application to store notes (especially music sheets), load them and auto-slide.
When you play both hans on guitar you're not able to scroll down the screen by your computer mouse.
Noteslider allows you to compose track from assets - multiple files.
If you create the track you can load it and click play button. It will start to slide slowly.
You can modify speed using slider on the right side.

## Download
TODO

## Extensions
You can register your own file type to be interpreted by Noteslider.
To do so, you need to write class implementing IAssetRendererWorker interface.
You also have to define how to render it: write class derived from AssetRenderer.
At the end you have to register new asset type in Main-CompositionRoot.

```csharp

    // register file type
    AssetConverter.RegisterExtension<TextAsset>(".txt");

    // define conversion from binary asset to your asset
    AssetConverter.RegisterConversionTo<TextAsset>((basset) =>
    {
        var text = Encoding.UTF8.GetString(basset.Bytes);
        return new TextAsset(text);
    });

    // register worker
    AssetRendererFactory.Instance
        .SetRendererProvider<TextAsset>(new TextAssetRendererWorker());


```