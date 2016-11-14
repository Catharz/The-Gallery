using UnityEngine;

/*
	This is an examle of how to use the procedural cubemap generator and the cubemap it generates.
*/
public class ExmpleCubemapGeneratorClient : MonoBehaviour
{
	//this is the procedural material being used in this example
	public ProceduralMaterial inputMaterial;

	//this is the material used for the skybox in this example
	public Material skyboxMaterial;

	public void generateNewCubemap ()
	{
		//set the procedural material's properties if needed, before rendering
		inputMaterial.SetProceduralFloat ("$randomseed", Random.Range (0.0f, 10000.0f));

		//have the procedural material render a new texture
		inputMaterial.RebuildTexturesImmediately ();

		/* render the cubemap
			This takes the input material and renders it to a cubemap of the specified size. 

			Remember, this is not increasing the resolution at which the procedural material is rendering the texture, 
			that must be set in the procedural material itself. You will likely want the cubemap to be the same size
			or larger than the procedural material's size to retain all the quality.
		*/
		Cubemap cm = CubemapGeneratorHelper.instance.generateCubemap (inputMaterial, CubemapGenerator.Sizes._512);

		//set the skybox material to use the cubemap which was just rendered
		skyboxMaterial.SetTexture ("_Tex", cm);
	}
}
