# MURAL_MeshBuilder_Unity
This repository is a collection of components for generating mesh from vertex information. The generated mesh can be procedurally changed both in the editor and runtime.
The vertex information of this component is organized in a class called VertexCache that stores the cache information of the vertex information, so it can be handled together with other effects.
In addition, the following are the references I used to create this repository with great gratitude.

[NoiseBall](https://github.com/keijiro/NoiseBall)

![Plane](https://user-images.githubusercontent.com/63334692/128618762-3e15db2a-1991-4aaa-ba92-b3a954a61802.PNG)

![ICOSphere](https://user-images.githubusercontent.com/63334692/128618766-ba174fe4-4a2a-4a62-85ff-8a50a0ccd2ac.PNG)

## Environments
### Unity
Unity 2021.1.9f1
### Dependencies
None

## How To Install
Please Add to manifest.json
```
"com.mural-inc.mesh-builder": "https://{Your Access Token}:x-oauth-basic@github.com/mur4l-inc/MURAL_MeshBuilder_Unity.git?path=/Packages/Mural_MeshBuilder/",
```
In result, your manifest.json will become
```
{
  "dependencies": {
    "com.mural-inc.mesh-builder": "https://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:x-oauth-basic@github.com/mur4l-inc/MURAL_MeshBuilder_Unity.git?path=/Packages/Mural_MeshBuilder/",
    ...
  }
```

## Usage
### Create Scriptable Object
Create the relevant scriptable object in the Project Window.
ex) Create > Mural > MeshBuilder > PlaneMesh
This scriptable object is a data container that holds the mesh and the data it needs.

### Create Empty Object and Attach Mesh Rendere Component
Create an empty object in the hierarchy and add a Renderer component to it that corresponds to the scriptable object.
<br>
![Inspector](https://user-images.githubusercontent.com/63334692/128618737-4fa2843e-d02b-451d-a33d-cf879d9a3c0a.PNG)
