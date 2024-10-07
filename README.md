# Raymarched Bounding Volumes
<div align="justify">
	<a href="#Raymarched-Bounding-Volumes"><code>Raymarched Bounding Volumes</code></a> is a collection of 
	<code>Unity</code> packages designed to enhance the visualization and manipulation of complex geometries through
	raymarching techniques. This repository provides the following packages:
	<p>
	<ul>
		<li><a href="#RBV"><code>RBV</code></a>:
			The core package that introduces the fundamental capabilities for raymarched bounding volumes, enabling
			efficient rendering and manipulation of <code>3D</code> objects.
		</li>
		<li><a href="#RBV4D"><code>RBV.4D</code></a>:
			An extension package that introduces four-dimensional features, allowing for dynamic exploration of
			<code>4D</code> geometries in a familiar <code>3D</code> space, with full compatibility with
			<a href="#Features"><code>RBV Features</code></a>.
		</li>
		<li><a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a>:
			A specialized extension designed to visualize raymarched bounding volumes using heatmaps, providing
			intuitive exploration, debugging, and performance analysis by mapping raymarching iterations to color
			gradients.
		</li>
	</ul>
	By leveraging advanced rendering methods, these packages allow for the dynamic exploration of geometrical forms. The
	modular architecture ensures seamless integration, enabling users to customize workflows to meet specific needs
	while maintaining compatibility with existing Unity objects and shaders. Whether creating stunning visual effects or
	conducting intricate simulations, <a href="#Raymarched-Bounding-Volumes"><code>Raymarched Bounding Volumes</code></a>
	equips developers with essential tools for achieving advanced graphical results.
</div>
<br><br><br><br><br>





# RBV
<div align="justify">
	<a href="#RBV"><code>RBV</code></a> (short for <a href="#Raymarched-Bounding-Volumes">
		<code>Raymarched Bounding Volumes</code></a>) is a core package for	raymarching	in Unity. It is designed for the
	effective visualization and manipulation of 3D objects and their combinations.
	<p><p>
	The name <a href="#Raymarched-Bounding-Volumes"><code>Raymarched Bounding Volumes</code></a> comes from the idea of 
	using bounding volumes to define the	rendering space for raymarching, which has been proven to be more effective
	than rendering in the entire screen space
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
- **Editor and Runtime Object Creation/Deletion Support**  
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
- **Shared Shader Depth** 
	<div align="justify">
	Utilizing the same pixel depth system as <code>Unity</code>, <a href="#RBV"><code>RBV</code></a> ensures accurate 
	depth calculations and allows for correct intersections and overlapping with standard <code>Unity</code> objects,
	enhancing the	integration between raymarched and conventional elements in your scene.
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
	</ol>
	To install samples, navigate to <code>RBV/Samples</code> in the <code>Package Manager</code>
	and click <code>Import</code> for the desired ones.
	</div></li>
	<p>
	<li><div align="justify"><b>Download From Releases</b><ol>
		<li>Go to the <a href="https://github.com/danliukuri/RaymarchedBoundingVolumes/releases"
			><code>Releases Page</code></a>.</li>
		<li>Download the desired package(s):<ul>
			<li>Core package: <code>RBV@vX.X.X.unitypackage</code>.</li>
			<li>Samples (optional): <code>RBV.Samples@vX.X.X.unitypackage</code>.</li>
		</ul></li>
		<li>Import the package(s) into your <code>Unity</code> project.</li>
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
				The main loop of the package lifecycle, ensuring proper shader data updates and managing the
				construction of	the raymarching scene.
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
		Last step is to configure the visualization space for the raymarching volume. For more details on how to do
		this, refer to the <a href="#Volume-Mesh-Configuration"><code>Volume Mesh Configuration</code></a> section.
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
	<p>
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
			<code>RaymarchedVolumeMeshConfigurator</code></a> component, select one of the options to set the mesh for
		the children.
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
	To demonstrate the capabilities of <a href="#RBV"><code>RBV</code></a>, the following samples have been created.
</div>

### Showcase
<div align="justify">
	This sample contains only one scene named
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Samples~/RBV/Showcase/Cover.unity"><code>Cover</code></a>,
	where you can observe how numerous
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedObject.prefab">
			<code>RaymarchedObject</code></a> instances smoothly unite to create a cohesive composition.
<p>
</div>

<img src="https://github.com/user-attachments/assets/4d3405fa-2a78-4129-aa30-d93748f10725" alt="Cover scene preview" />
<p align="center"><a href="#RBV"><code>RBV</code></a> cover scene preview</p>

> [!WARNING]
> <div align="justify">
> 	This represents a rather unconventional usage of <a href="#RBV"><code>RBV</code></a> and raymarching in
> 	general, as this scene has many
> 	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Prefabs/RaymarchedObject.prefab">
> 		<code>RaymarchedObject</code></a> instances concentrated in one area. Since they occupy the entire camera
> 	rendering screen space, we do not gain any performance benefits from utilizing bounding volumes. Given that
> 	raymarching is inherently slow, this scene may even lead to performance issues for all the aforementioned reasons.
> </div>


## Contribution
<div align="justify">
	Feel free to submit issues or pull requests to improve <a href="#RBV"><code>RBV</code></a> package.
</div>
<p>

> [!NOTE]
> <div align="justify">
> 	To simplify working with <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Samples~/RBV">
> 		<code>RBV/Samples</code></a>, symbolic inks were utilized. If you're using <code>Windows</code>, refer to
> 	<a href="https://stackoverflow.com/questions/5917249/git-symbolic-links-in-windows">
> 		<code>Symbolic Links in Windows</code></a> for more information.
> </div>
<br><br><br><br><br>





# RBV.4D
<div align="justify">
	<a href="#RBV4D"><code>RBV.4D</code></a> is an extension package for <a href="#RBV"><code>RBV</code></a> that
	introduce four-dimensional features. It enables the visualization and manipulation of <code>4D</code> objects within
	raymarched volumes by slicing through these objects along a fourth spatial dimension and projecting them into
	<code>3D</code>, while leveraging all the techniques provided by the core <a href="#RBV"><code>RBV</code></a> package.
	This functionality allows for dynamic	exploration of <code>4D</code> geometry in a familiar <code>3D</code> space.
</div>


## Features
<ul>
	<li><div align="justify"><b>Support for All RBV Features</b><br>
		<a href="#RBV4D"><code>RBV.4D</code></a> fully supports all <a href="#Features"><code>Features</code></a> of the
		core <a href="#RBV"><code>RBV</code></a> package. By utilizing the same shader, <code>4D</code> objects can
		seamlessly coexist with <code>3D</code> raymarched objects as well as standard <code>Unity</code> objects.
	</div></li>
	<li><div align="justify"><b>Ready-to-Use Configurable 4D Objects</b><br>
 		Provides pre-configured <code>4D</code> objects that are easily customizable and ready for immediate use in your
 		scene.
	</div></li>
	<li><div align="justify"><b>4D Transformations</b><br>
		Enables movement, rotation, and scaling of <code>4D</code> objects along the fourth spatial dimension, while
		remain supporting such transformations in the standard three dimensions.
	</div></li>
</ul>


## Installation
> [!Note]
> <div align="justify">
> 	As <a href="#RBV4D"><code>RBV.4D</code></a> explicitly depends on <a href="#RBV"><code>RBV</code></a>, ensure the
> 	base package is installed before proceeding.
> 	See <a href="#Installation"><code>RBV Installation</code></a> for more details.
> </div>
<div align="justify">
	You can install the <a href="#RBV4D"><code>RBV.4D</code></a> package using one of the following methods:
</div>
<p>
<ul>
	<li><div align="justify"><b>Unity Package Manager</b><ol>
		<li>Open <code>Unity</code>, then go to <code>Window -> Package Manager</code>.</li>
		<li>Click on the drop-down and select <code>Add package from git URL...</code>.</li>
		<li>Paste the following <code>URL</code> and click <code>Add</code>:
			<pre><code>https://github.com/danliukuri/RaymarchedBoundingVolumes.git?path=RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D</code></pre>
		</li>
	</ol>
	To install samples, navigate to <code>RBV.4D/Samples</code> in the
	<code>Package Manager</code> and click <code>Import</code> for the desired ones.
	<p>
	</div></li>
	<li><div align="justify"><b>Download From Releases</b><ol>
		<li>Go to the <a href="https://github.com/danliukuri/RaymarchedBoundingVolumes/releases">
			<code>Releases Page</code></a>.</li>
		<li>Download the desired package(s):<ul>
			<li>Core package: <code>RBV.4D@vX.X.X.unitypackage</code>.</li>
			<li>Samples (optional): <code>RBV.4D.Samples@vX.X.X.unitypackage</code>.</li>
		</ul></li>
		<li>Import the package into your <code>Unity</code> project.</li>
	</ol></div></li>
</ul>


## Usage
### Scene Construction
<div align="justify">
	To start using the <a href="#RBV4D"><code>RBV.4D</code></a> package, follow the same steps for
	<a href="#Scene-Construction"><code>Scene Construction</code></a> as in the <a href="#RBV"><code>RBV</code></a>
	package to set up your scene. The only differences are:
</div>
<p>
<ul>
	<li><div align="justify"><b>Step 3: Add the Raymarching Engine</b><br>
		Add the <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Prefabs/RaymarchingEngine4D.prefab">
			<code>RaymarchingEngine4D</code></a> prefab, which contains the same
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Features/RaymarchingSceneBuilding/RaymarchingSceneUpdater.cs">
			<code>RaymarchingSceneUpdater</code></a> component but includes the
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Scripts/Runtime/Infrastructure/Raymarching4DServicesRegister.cs">
			<code>Raymarching4DServicesRegister</code></a>. This component extends
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV/Scripts/Runtime/Infrastructure/RaymarchingServicesRegister.cs">
			<code>RaymarchingServicesRegister</code></a> and additionally handling the creation of 4D-related services
		and	their dependencies.
	</div></li>
	<li><div align="justify"><b>Step 4: Add Raymarched Objects and Operations</b><br>
		With the <a href="#RBV4D"><code>RBV.4D</code></a> package installed, you can use <code>4D</code> objects by
		dragging and dropping the
		<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Prefabs/RaymarchedObject4D.prefab">
			<code>RaymarchedObject4D</code></a> into the scene.
	</div></li>
</ul>
<div align="justify">
	The aforementioned prefabs are located at the path
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Prefabs"><code>RBV.4D/Prefabs</code></a>.
</div>


## Samples
<div align="justify">
	To demonstrate the capabilities of <a href="#RBV4D"><code>RBV.4D</code></a>, the following samples have been created.
</div>

### Showcase
<div align="justify">
	This sample contains only one scene named
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Samples~/RBV.4D/Showcase/Cover.unity">
		<code>Cover</code></a>,	where you can observe how numerous
	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Prefabs/RaymarchedObject4D.prefab">
		<code>RaymarchedObject4D</code></a> instances smoothly unite to create a cohesive composition. In this scene,
	you	can also observe how seamlessly <code>4D</code> objects interact with regular raymarched ones.
<p>
</div>

<img src="https://github.com/user-attachments/assets/e5c69488-ed6d-46e8-9d50-7b262506e345" alt="RBV.4D Cover Scene Preview" />
<p align="center"><a href="#RBV4D"><code>RBV.4D</code></a> cover scene preview</p>

> [!WARNING]
> <div align="justify">
> 	This represents a rather unconventional usage of <a href="#RBV4D"><code>RBV.4D</code></a> and raymarching in
> 	general, as this scene has many
> 	<a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Prefabs/RaymarchedObject4D.prefab">
> 		<code>RaymarchedObject4D</code></a> instances concentrated in one area. Since they occupy the entire camera
> 	rendering screen space, we do not gain any performance benefits from utilizing bounding volumes. Given that
> 	raymarching is inherently slow, this scene may even lead to performance issues for all the aforementioned reasons.
> </div>


## Contribution
<div align="justify">
	Feel free to submit issues or pull requests to improve <a href="#RBV4D"><code>RBV.4D</code></a> package.
</div>
<p>

> [!NOTE]
> <div align="justify">
> 	To simplify working with <a href="RaymarchedBoundingVolumes.Unity/Assets/Plugins/RBV.4D/Samples~/RBV.4D">
> 		<code>RBV.4D/Samples</code></a>, symbolic inks were utilized. If you're using <code>Windows</code>, refer to
> 	<a href="https://stackoverflow.com/questions/5917249/git-symbolic-links-in-windows">
> 		<code>Symbolic Links in Windows</code></a> for more information.
> </div>
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
> [!Note]
> <div align="justify">
> 	As <a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> explicitly depends on
> 	<a href="#RBV"><code>RBV</code></a>, ensure the base package is installed before proceeding.
> 	See <a href="#Installation"><code>RBV Installation</code></a> for more details.
> </div>
<div align="justify">
	You can install the <a href="#RBVHeatmapping"><code>RBV.Heatmapping</code></a> package using one of the following
	methods:
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
		<li>Go to the <a href="https://github.com/danliukuri/RaymarchedBoundingVolumes/releases">
			<code>Releases Page</code></a>.</li>
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
		<code>RaymarchedVolume</code></a> shader.
	<p>
	<ol>
		<li>Open the <a href="https://docs.unity3d.com/Manual/Preferences.html">
			<code>Unity Editor Preferences</code></a> and navigate to the
			<code>Raymarched Bounding Volumes/Heatmapping</code> settings.
		</li>
		<li>
			Enable the heatmapping feature using the <code>Enabled</code> toggle field.
		</li>
	</ol>
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