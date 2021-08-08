# MURAL_MeshBuilder_Unity
This repository is a collection of components for generating mesh from vertex information. The generated mesh can be procedurally changed both in the editor and runtime.
The vertex information of this component is organized in a class called VertexCache that stores the cache information of the vertex information, so it can be handled together with other effects.
In addition, the following are the references I used to create this repository with great gratitude.

[NoiseBall](https://github.com/keijiro/NoiseBall)

## Environments
### Unity
Unity 2021.1.9f1
### Dependencies
None

## Usage
### Create Scriptable Object
Create the relevant scriptable object in the Project Window.
ex) Create > Mural > MeshBuilder > PlaneMesh
This scriptable object is a data container that holds the mesh and the data it needs.

### Create Empty Object and Attach Mesh Rendere Component

