# RBV
<div align="justify">
	<a href="#RBV"><code>RBV</code></a> (short for 
	<a href="#Raymarched-Bounding-Volumes"><code>Raymarched-Bounding-Volumes</code></a>) is a core package for raymarching
	in Unity. It is designed for the effective visualization and manipulation of 3D objects and their combinations.
	<p><p>
	The name <a href="#Raymarched-Bounding-Volumes"><code>Raymarched-Bounding-Volumes</code></a> comes from the idea of 
	using bounding volumes to define the	rendering space for raymarching, which has been proven to be more effective than
	rendering in the entire screen space
	(refer to <a href="https://github.com/danliukuri/RaymarchingRenderingSpacePerformanceTesting">
		<code>RaymarchingRenderingSpacePerformanceTesting</code></a>).
</div>


## Features
- **Ready-to-Use Configurable 3D Objects**  
	<div align="justify">
	A wide variety of 3D objects that can be easily customized to suit your needs.
	</div>
- **Ready-to-Use Configurable Combining Operations**  
	<div align="justify">
	Utilize various operations to combine 3D objects in creative ways, enabling complex scene creation with ease.
	</div>
- **Operation Nesting Support**  
	<div align="justify">
	Supports nesting of operations up to <code>10</code> levels deep
	(currently a hardcoded value, but could be easily made dynamic, wait for future updates).
	</div>
- **`Editor` and `Runtime` Object Creation/Deletion Support**  
	<div align="justify">
	Create and delete objects seamlessly during both the editing phase and at runtime.
	</div>
- **High Reactivity**  
	<div align="justify">
	All object and operation properties are observable, ensuring immediate feedback and interaction.
	</div>
- **Render Settings for Each Object** 
	<div align="justify"> 
	Customize render settings for each object, currently supporting color adjustments.
	</div>
- **Advanced Shading Options** 
	<div align="justify"> 
	Configurable shading per material, with support for four types of shadows, ambient occlusion, and <code>Unity</code>
	directional and ambient light.
	</div>
- **Raymarching Configuration** 
	<div align="justify"> 
	Fine-tune raymarching settings on a per-material basis, including iterations, accuracy, and far rendering plane.
	</div>
- **GPU Instancing** 
	<div align="justify"> 
	Leverage GPU instancing for the main
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Shaders/RaymarchedVolume.shader">
		<code>RaymarchedVolume</code></a> shader, improving performance.
	</div>
- **Bounding Volumes** 
	<div align="justify">
	Limit the rendering space of raymarching to improve performance by confining calculations to specific areas.
	</div>
- **Raymarched Volume Mesh Configuration** 
	<div align="justify"> 
	Change the mesh scale for object bounding volumes to suit various visualization needs.
	</div>
- **Custom Inspectors** 
	<div align="justify"> 
	Features custom property drawers, material property drawers, custom attributes, and editors for a tailored user
	experience.
	</div>


## Installation
<div align="justify">
	You can install the <a href="#RBV"><code>RBV</code></a> package using one of the following methods:
</div>
<p>
<ul>
	<li><div align="justify"><b>Unity Package Manager</b><ol>
		<li>Open <code>Unity</code>, then go to <code>Window -> Package Manager</code>.</li>
		<li>Click on the drop-down and select <code>Add package from git URL...</code>.</li>
		<li>Paste the following <code>URL</code> and click <code>Add</code>:
			<pre><code>https://github.com/danliukuri/RaymarchedBoundingVolumes.git?path=RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV</code></pre>
		</li>
	</ol></div></li>
	<li><div align="justify"><b>Download From Releases</b><ol>
		<li>Go to the <a href="https://github.com/danliukuri/RaymarchedBoundingVolumes/releases"><code>Releases Page</code></a>.</li>
		<li>Download the desired package: <code>RBV@vX.X.X.unitypackage</code>.</li>
		<li>Import the package into your <code>Unity</code> project.</li>
	</ol></div></li>
</ul>


## Usage
### Scene Construction
<div align="justify">
	To start using the <a href="#RBV"><code>RBV</code></a> package, follow these steps to set up your scene:
</div>
<p>
<ol>
	<li><div align="justify"><b>Open your Desired Scene</b><br>
		Begin by opening the <code>Unity</code> scene where you want to use raymarching.
	</div></li>
	<li><div align="justify"><b>Navigate to the Prefabs Folder</b><br>
		Go to the package's prefabs folder located at <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs">
			<code>RBV/Prefabs</code></a>.
	</div></li>
	<li><div align="justify"><b>Add the Raymarching Engine</b><br>
		Drag and drop the <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchingEngine.prefab">
			<code>RaymarchingEngine</code></a> prefab into your scene hierarchy. This object contains two crucial components:
		<ul>
			<li><div align="justify">
				<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Infrastructure/RaymarchingServicesRegister.cs">
					<code>RaymarchingServicesRegister</code></a>:
				Serves as the entry point for the package, handling the	creation of services and their dependencies.
			</div></li>
			<li><div align="justify">
				<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Features/RaymarchingSceneBuilding/RaymarchingSceneUpdater.cs">
					<code>RaymarchingSceneUpdater</code></a>:
				The main loop of the package lifecycle, ensuring proper shader data updates and managing the construction of
				the raymarching scene.
			</div></li>
		</ul>
	</div></li>
	<li><div align="justify"><b>Add Raymarched Objects and Operations</b><br>
		Now that your scene is set up, you can start dragging and dropping
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedObject.prefab">
			<code>RaymarchedObject</code></a> and 
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchingOperation.prefab">
			<code>RaymarchingOperation</code></a> prefabs into the scene, tailoring it to suit your specific needs.
	</div></li>
	<li><div align="justify"><b>Configure the Visualization Space</b><br>
		Last step is to configure the visualization space for the raymarching volume. For more details on how to do this,
		refer to the <a href="#Volume-Mesh-Configuration"><code>Volume Mesh Configuration</code></a> section.
	</div></li>
</ol>

### Volume Mesh Configuration
<div align="justify">
	Since <a href="#RBV"><code>RBV</code></a> utilizes the concept of bounding volumes, scaling the 
	<code>Transform</code> adjusts the raymarching space scale. To address this, the mesh of the volume itself can be
	scaled. For simplifying this process, the 
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Features/RaymarchedVolumeMeshConfigurator.cs">
		<code>RaymarchedVolumeMeshConfigurator</code></a> 
	has been designed, enabling straightforward adjustments to the mesh scale.
</div>
<p>
<div align="justify">
	To utilize the <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Features/RaymarchedVolumeMeshConfigurator.cs">
		<code>RaymarchedVolumeMeshConfigurator</code></a>, follow these steps:
</div>
<p>
<ol>
	<li><div align="justify"><b>Drag and Drop the Configurator Prefab</b><br>
		Drag and drop the <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedVolumeMeshConfigurator.prefab">
			<code>RaymarchedVolumeMeshConfigurator</code></a> prefab located at
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs"><code>RBV/Prefabs</code></a>
		into the desired scene.
	</div></li>
	<li><div align="justify"><b>Parent Volume GameObjects</b><br>
		Parent the desired volume-defining <code>GameObjects</code> to the
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedVolumeMeshConfigurator.prefab">
			<code>RaymarchedVolumeMeshConfigurator</code></a>. This can include not only
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedObject.prefab">
			<code>RaymarchedObjects</code></a>, as the volume is not limited to them; volume mesh renderers can exist as
		separate <code>GameObjects</code>.
	</div></li>
	<li><div align="justify"><b>Set the Mesh via Context Menu</b><br>
		In the context menu of the
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Features/RaymarchedVolumeMeshConfigurator.cs">
			<code>RaymarchedVolumeMeshConfigurator</code></a> component, select one of the options to set the mesh for the children.
	</div></li>
	<li><div align="justify"><b>Adjust the Mesh Size</b><br>
		Change the size of the mesh by modifying the <code>Size</code> field in the 
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Features/RaymarchedVolumeMeshConfigurator.cs">
			<code>RaymarchedVolumeMeshConfigurator</code></a> component.
	</div></li>
</ol>
<div align="justify">
	Optionally, you can change the original mesh itself to create a different shape for the raymarching volume. The 
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Art/Models/PentakisDodecahedron.fbx">
		<code>PentakisDodecahedron</code></a> is used as the default mesh due to its efficiency in defining an accurate 
	sphere shape with minimal vertices and triangles. Additionally, the package includes the 
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Art/Models/PentakisIcosahedron.obj">
		<code>Icosahedron</code></a> mesh. Moreover, the regular <code>Unity</code> 
	cube mesh can also work well in many cases.
</div>
<p>

> [!TIP]
> <div align="justify">
> 	It’s worth mentioning that when configuring a volume for an object, you should account for its parts that become
> 	visible when combined with other objects.
> </div>


## Samples
<div align="justify">
	To demonstrate the capabilities of the <a href="#RBV"><code>RBV</code></a>, the following samples have been created.
</div>

### Showcase
<div align="justify">
	This sample contains only one scene named
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Samples~/RBV/Showcase/Cover.unity"><code>Cover</code></a>,
	where you can observe how numerous
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedObject.prefab">
			<code>RaymarchedObjects</code></a> smoothly unite to create a cohesive composition.
<p>
</div>

<img src="https://github.com/user-attachments/assets/4d3405fa-2a78-4129-aa30-d93748f10725" alt="Cover scene preview" />
<p align="center"><a href="#RBV"><code>RBV</code></a> cover scene preview</p>

> [!WARNING]
> <div align="justify">
> 	This represents a rather unconventional usage of <a href="#RBV"><code>RBV</code></a> and raymarching in
> 	general, as this scene has many
> 	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedObject.prefab">
> 		<code>RaymarchedObjects</code></a> concentrated in one area. Since they occupy the entire camera
> 	rendering screen space, we do not gain any performance benefits from utilizing bounding volumes. Given that
> 	raymarching is inherently slow, this scene may even lead to performance issues for all the aforementioned reasons.
> </div>


## Contribution
<div align="justify">
	Feel free to submit issues or pull requests to improve <a href="#RBV"><code>RBV</code></a> package.
</div>
<br><br><br><br><br>






# RBV.Heatmapping
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
<div align="justify">
	You can install the <a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> package using one of the following methods:
</div>
<p>
<ul>
	<li><div align="justify"><b>Unity Package Manager</b><ol>
		<li>Open <code>Unity</code>, then go to <code>Window -> Package Manager</code>.</li>
		<li>Click on the drop-down and select <code>Add package from git URL...</code>.</li>
		<li>Paste the following <code>URL</code> and click <code>Add</code>:
			<pre><code>https://github.com/danliukuri/RaymarchedBoundingVolumes.git?path=RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.Heatmapping</code></pre>
		</li>
	</ol></div></li>
	<li><div align="justify"><b>Download From Releases</b><ol>
		<li>Go to the <a href="https://github.com/danliukuri/RaymarchedBoundingVolumes/releases"><code>Releases Page</code></a>.</li>
		<li>Download the desired package: <code>RBV.Heatmapping@vX.X.X.unitypackage</code>.</li>
		<li>Import the package into your <code>Unity</code> project.</li>
	</ol></div></li>
</ul>



## Usage
### Enabling Heatmapping in Preferences
<div align="justify">
	To see the <a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> package in action, it must first be enabled
	in the settings for all	materials that utilize the
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Shaders/RaymarchedVolume.shader">
		<code>RaymarchedVolume</code></a>
	shader.
</div>
<p>

1. Open the [`Unity Editor Preferences`](https://docs.unity3d.com/Manual/Preferences.html) and navigate to the
`Raymarched Bounding Volumes/Heatmapping` settings.
2. Enable the heatmapping feature using `Enabled` toggle field.

<div align="justify">
	The texture specified in the <code>Texture</code> field will be used for the heatmap visualization. By default, the
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.Heatmapping/Art/Sprites/Heatmaps/HitmapMagma.png">
		<code>HitmapMagma</code></a>
	texture is specified.
<p>
</div>

<img src="https://github.com/user-attachments/assets/3e698fed-5c47-43be-90e0-d513cc5a58b6" alt="Heatmapping settings in Unity Editor Preferences" />
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
<p>
</div>

<img src="https://github.com/user-attachments/assets/5f754d66-6a50-4bc8-9903-67af9e4d501c" alt="Scene heatmap example" />
<p align="center">Scene heatmap example with texture, showcasing the corresponding color for each iteration</p>

For more details, refer to the
[`RaymarchingRenderingSpacePerformanceTesting`](https://github.com/danliukuri/RaymarchingRenderingSpacePerformanceTesting)


## Contribution
<div align="justify">
	Feel free to submit issues or pull requests to improve <a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a>
	package.
</div>