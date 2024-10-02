# RBV.Heatmapping
## Overview
<div align="justify">
	<a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> is a specialized extension package for
	<a href="#RBV"><code>RBV</code></a> designed to visualize raymarched bounding volumes using heatmaps by	mapping
	raymarching iterations to color gradients, allowing for intuitive exploration, debugging, and analysis.
</div>


## Features
- **Settings in Unity Preferences**  
  <div align="justify">
	Configure heatmapping parameters on a per-user basis through the
	<a href="https://docs.unity3d.com/Manual/Preferences.html"><code>Unity Editor Preferences</code></a>, allowing for
	seamless integration and flexibility.
  </div>
- **Customizable Textures**  
  <div align="justify">
	The package comes with three built-in heatmap textures, while also supporting customizable textures to tailor color
	gradients to specific visualization needs.
  </div>


## Installation
You can install the [`RBV.Heatmapping`](#RBVHeatmapping) package using one of the following methods:

### 1. Unity Package Manager

1. Open `Unity`, then go to `Window -> Package Manager`.
2. Click on the dropdown and select `Add package from git URL...`
3. Paste the following `URL` and click `Add`:
```
https://github.com/danliukuri/RaymarchedBoundingVolumes.git?path=RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.Heatmapping
```

### 2. Download From Releases

1. Go to the [Releases Page](https://github.com/danliukuri/RaymarchedBoundingVolumes/releases).
2. Download the desired package: `RBV.Heatmapping@vX.X.X.unitypackage`
3. Import the package into your `Unity` project.


## Usage
### Enabling Heatmapping in Preferences
<div align="justify">
	To see the <a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> package in action, it must first be enabled
	in the settings for all	materials that utilize the
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Shaders/RaymarchedVolume.shader">
		<code>RaymarchedVolume</code></a>
	shader.
</div>
<br>

1. Open the [`Unity Editor Preferences`](https://docs.unity3d.com/Manual/Preferences.html) and navigate to the
`Raymarched Bounding Volumes/Heatmapping` settings.
2. Enable the heatmapping feature using `Enabled` toggle field.

<div align="justify">
	The texture specified in the <code>Texture</code> field will be used for the heatmap visualization. By default, the
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.Heatmapping/Art/Sprites/Heatmaps/HitmapMagma.png">
		<code>HitmapMagma</code></a>
	texture is specified.
</div>
<br>

![RBV.Heatmapping settings in Unity Editor Preferences](https://github.com/user-attachments/assets/3e698fed-5c47-43be-90e0-d513cc5a58b6)
<p align="center"><a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> settings in
	<a href="https://docs.unity3d.com/Manual/Preferences.html"><code>Unity Editor Preferences</code></a></p>

### Example
<div align="justify">
	The colors in the heatmap texture represent the number of iterations used in raymarching,
	arranged from left to right.
	For instance, in a scene that contains numerous spheres, enabling heatmapping with the
 	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.Heatmapping/Art/Sprites/Heatmaps/HitmapCustom.png">
		<code>HitmapCustom</code></a>
	texture selected and setting the maximum number of iterations for ray marching to <code>32</code> will result in the
	following appearance for the scene
	(for clarity, the background is set to the color corresponding to zero iterations):
</div>
<br>

![Scene heatmap example](https://github.com/user-attachments/assets/5f754d66-6a50-4bc8-9903-67af9e4d501c)
<p align="center">Scene heatmap example with texture, showcasing the corresponding color for each iteration</p>

<div align="justify">
	For more details, refer to the
	<a href="https://github.com/danliukuri/RaymarchingRenderingSpacePerformanceTesting">RaymarchingRenderingSpacePerformanceTesting</a>
	repository.
</div>


## Contribution
Feel free to submit issues or pull requests to improve [`RBV.Heatmapping`](#RBVHeatmapping) package.