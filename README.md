# SOFAUnity-Renderer

## Description
This repository is a **Unity3D package allowing to render a SOFA simulation scene inside Unity3D**. 
All SOFA `VisualModel` components present in the loaded simulation will be rendered inside Unity3D engine as `GameObject` with a `MeshFilter`.  Thus, it is possible to apply Unity3D Materials to each visual model and also to combine Unity3D assets with the SOFA simulation.
<br>

<table>
<tr>
    <td style="text-align: center; vertical-align: middle;"><strong>Important:</strong> This version allows only to change the Gravity and the TimeStep of the SOFA simulation and only the <i>VisualModel</i> are mapped into Unity3D. <br>A <strong>full integration of SOFA</strong> components with a two-way communication can be requested here:<br> https://infinytech3d.com/sapapi-unity3d/</td>
</tr>
</table>


#### Compatibility:
- Unity version > 2020.3.x
- SOFA version 22.12 with SofaPhysicsAPI activated (23.06 support will be tested soon)
- Only tested on Windows

## Installation guide
1. Install Unity engine version > 2020.3.x
2. (optionnal) Create a new project

#### Installation from asset:
3. Download the unity asset provided in the release page:
   [For the SofaUnity-Renderer_v22.12_Win64](https://github.com/InfinyTech3D/SofaUnity-Renderer/releases/download/v22.12/SofaUnity-Renderer_v22.12_Win64.unitypackage) 
5. Load the unity asset inside Unity3D Editor, use ```Assets -> Import Package -> Custom Package``` and load the package `SofaUnity-Renderer_v22.12_Win64.unitypackage`

#### Installation from source:
3. Clone this repository inside your unity project: 
```git clone git@github.com:InfinyTech3D/SofaUnity-Renderer.git /myUnityProject/Assets/SofaUnity```
4. Download the release v22.12 of SOFA (either using the installer or the zip file) provided here: https://github.com/sofa-framework/sofa/releases/tag/v22.12.00
5. Copy the SOFA .dll from the folder ```/SOFA_v22.12.00_Win64\bin\ ``` inside: ```/myUnityProject/Assets/SofaUnity/Plugins/Native/x64```  (for windows) | ```/myUnityProject/Assets/SofaUnity/Plugins/Native/x86_64``` (for linux)


## Usage
As soon as the `SofaUnity-Renderer.unitypackage` is loaded inside your Unity project, you will have access to a top menu panel called `SofaUnity`. 

<p align="center">
	<img src="https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/menu_01.jpg" style="width:400px;"/>
</p>


This pannel allows to **load a SOFA scene** in only 2 steps:
1. **Click on SofaContext** to add a `GameObject` into your unity scene graph with a `SofaContext component`. 
This component correspond to the SOFA world 3D frame and provides a simple API to load a SOFA scene, change the Time Stepping and the Gravity. 

<p align="center">
	<img src="https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/menu_02.jpg" style="width:400px;"/>
</p>

2. **Click on the button** ```Load SOFA Scene (.scn) file```  to load a SOFA scene. This will create a `GameObject` with a `Unity MeshFilter` for each SOFA ```VisualModel```

<p align="center">
	<img src="https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/menu_03.jpg"/>
</p>

SOFA simulation will be performed as soon as Unity is playing.

## Examples
Three examples corresponding to SOFA Demo folder are provided inside the package:
They are available in the folder [/Scenes/Demos/](https://github.com/InfinyTech3D/SofaUnity-Renderer/tree/main/Scenes/Demos)
- Demo_01_SimpleLiver -> Integration of  [Demos/liver.scn](https://github.com/sofa-framework/sofa/blob/master/examples/Demos/liver.scn)
- Demo_02_Caduceus -> Integration of  [Demos/caduceus.scn](https://github.com/sofa-framework/sofa/blob/master/examples/Demos/caduceus.scn)
- Demo_03_Tissue -> Integration of  [Demos/TriangleSurfaceCutting.scn](https://github.com/sofa-framework/sofa/blob/master/examples/Demos/TriangleSurfaceCutting.scn)

Here are a some results of the integration:
<p align="center">
	<img src="https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/interface_01.jpg" style="width:1000px;"/>
</p>
<p align="center">
	<img src="https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/interface_02.jpg" style="width:1000px;"/>
</p>

## License
This Unity asset is under GPL license. 
<br>
Other license formats can be provided for commercial use. For more information check [InfinyTech3D license page](https://infinytech3d.com/licenses/).


