# SOFAUnity-Renderer

## Description
SOFA-Renderer asset for Unity

## Installation guide
1. Install Unity engine version > 2020.3.x
2. (optionnal) Create a new project

### Installation from source:
3. clone the repo inside your unity project: 
```git clone git@github.com:InfinyTech3D/SofaUnity-Renderer.git /myUnityProject/Assets/SofaUnity```
5. Copy the **SOFA dll libraries** inside: ```/myUnityProject/Assets/SofaUnity/Plugins/Native/x64```  (for windows)

### Installation from asset:
3. install the unity asset: In Unity3D Editor, use ```Assets -> Import Package -> Custom Package``` and load the package **SofaUnity-Renderer.unitypackage**


## Usage
As soon as the **SofaUnity-Renderer.unitypackage** is loaded inside your Unity project, you will have access to a top menu pannel called `SofaUnity`. 

![](https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/menu_01.jpg)
This pannel allows to **load a SOFA scene** in only 2 steps:
1. **Click on SofaContext** to add a `GameObject` into your unity scene graph with a `SofaContext component`. 
This component correspond to the SOFA world 3D frame and provides a simple API to load a SOFA scene, change the Time Stepping and the Gravity. 

![](https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/menu_02.jpg)

2. **Click on the button** ```Load SOFA Scene (.scn) file```  to load a SOFA scene. This will create a `GameObject` with a `Unity MeshFilter` for each SOFA ```VisualModel```

![](https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/menu_03.jpg)

## Examples
Three examples corresponding to SOFA Demo folder are providen inside the package:
They are available in the folder [/Scenes/Demos/](https://github.com/InfinyTech3D/SofaUnity-Renderer/tree/main/Scenes/Demos)
- Demo_01_SimpleLiver -> Integration of  [Demos/liver.scn](https://github.com/sofa-framework/sofa/blob/master/examples/Demos/liver.scn)
- Demo_02_Caduceus -> Integration of  [Demos/caduceus.scn](https://github.com/sofa-framework/sofa/blob/master/examples/Demos/caduceus.scn)
- Demo_03_Tissue -> Integration of  [Demos/TriangleSurfaceCutting.scn](https://github.com/sofa-framework/sofa/blob/master/examples/Demos/TriangleSurfaceCutting.scn)

Here are a result of the integration:

![](https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/Doc/img/interface_01.jpg)

## License
This work is dual-licensed under either [GPL](https://github.com/InfinyTech3D/SofaUnity-Renderer/blob/main/LICENSE.md) or Commercial License. 
For commercial license request, please contact us by email at contact@infinytech3d.com

