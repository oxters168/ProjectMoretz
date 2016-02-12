using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OxMath;
using KUtility;

public class BSPMap
{
    private static List<string> undesiredTextures = new List<string>(new string[] { "TOOLS/TOOLSTRIGGER", "TOOLS/TOOLSBLACK", "TOOLS/CLIMB", "TOOLS/CLIMB_ALPHA", "TOOLS/FOGVOLUME", "TOOLS/TOOLSAREAPORTAL-DX10", "TOOLS/TOOLSBLACK", "TOOLS/TOOLSBLOCK_LOS",
                "TOOLS/TOOLSBLOCK_LOS-DX10", "TOOLS/TOOLSBLOCKBOMB", "TOOLS/TOOLSBLOCKBULLETS", "TOOLS/TOOLSBLOCKBULLETS-DX10", "TOOLS/TOOLSBLOCKLIGHT", "TOOLS/TOOLSCLIP", "TOOLS/TOOLSCLIP-DX10", "TOOLS/TOOLSDOTTED", "TOOLS/TOOLSFOG", "TOOLS/TOOLSFOG-DX10",
                "TOOLS/TOOLSHINT", "TOOLS/TOOLSHINT-DX10", "TOOLS/TOOLSINVISIBLE", "TOOLS/TOOLSINVISIBLE-DX10", "TOOLS/TOOLSINVISIBLELADDER", "TOOLS/TOOLSNODRAW", "TOOLS/TOOLSNPCCLIP", "TOOLS/TOOLSOCCLUDER", "TOOLS/TOOLSOCCLUDER-DX10", "TOOLS/TOOLSORIGIN",
                "TOOLS/TOOLSPLAYERCLIP", "TOOLS/TOOLSPLAYERCLIP-DX10", "TOOLS/TOOLSSKIP", "TOOLS/TOOLSSKIP-DX10", "TOOLS/TOOLSSKYBOX2D", "TOOLS/TOOLSSKYFOG", "TOOLS/TOOLSTRIGGER", "TOOLS/TOOLSTRIGGER-DX10" });

    #region Map Variables
    public static Dictionary<string, BSPMap> loadedMaps = new Dictionary<string, BSPMap>();
    public string mapName;

    public string mapLocation;
    //private static List<Texture2D> mapTextures;
    private static List<string> textureLocations;

    //private Material mainSurfaceMaterial = Resources.Load<Material>("Materials/MapMaterial");

    private Vector3[] vertices;
    private dedge_t[] edges;
    private dface_t[] faces;
    private int[] surfedges;

    private texinfo_t[] texInfo;
    private dtexdata_t[] texData;
    private int[] texStringTable;
    private string textureStringData;

    private FaceMesh[] faceMeshes;
    public DDSImage overviewImage;

    public bool alreadyLoaded = false;
    #endregion

    public BSPMap(string mName)
    {
        mapName = mName;
        if (mapName.LastIndexOf("/") > -1)
        {
            mapLocation = mapName.Substring(0, mapName.LastIndexOf("/") + 1);
            mapName = mapName.Substring(mapName.LastIndexOf("/") + 1);
        }
        else if (mapName.LastIndexOf("\\") > -1)
        {
            mapLocation = mapName.Substring(0, mapName.LastIndexOf("\\") + 1);
            mapName = mapName.Substring(mapName.LastIndexOf("\\") + 1);
        }
        if (mapName.LastIndexOf(".") == mapName.Length - 4) mapName = mapName.Substring(0, mapName.LastIndexOf("."));
        if (!loadedMaps.ContainsKey(mapName)) loadedMaps.Add(mapName, this);
        else alreadyLoaded = true;
    }

    public void LoadMap()
    {
        ReadMap();
        MakeFaceMeshes();
        LoadOverview();
    }
    private void ReadMap()
    {
        System.IO.FileStream mapFile = null;
        try
        {
            mapFile = new System.IO.FileStream(mapLocation + mapName + ".bsp", System.IO.FileMode.Open);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if (mapFile != null)
        {
            #region Read map
            BSPParser bsp = new BSPParser(mapFile);

            string entities = bsp.GetEntities();
            //Console.WriteLine("Map Entities: " + entities);
            vertices = bsp.GetVertices();
            edges = bsp.GetEdges();
            faces = bsp.GetFaces();
            surfedges = bsp.GetSurfedges();
            texInfo = bsp.GetTextureInfo();
            texData = bsp.GetTextureData();
            texStringTable = bsp.GetTextureStringTable();
            textureStringData = bsp.GetTextureStringData();

            mapFile.Close();
            #endregion
        }
    }
    private void MakeFaceMeshes()
    {
        if (faces != null)
        {
            List<FaceMesh> desiredFaceMeshes = new List<FaceMesh>();
            foreach (dface_t face in faces)
            {
                FaceMesh currentFace = new FaceMesh();
                currentFace.face = face;
                currentFace.mesh = MakeFaceMesh(face);
                currentFace.s = new Vector3(texInfo[face.texinfo].textureVecs[0][0], texInfo[face.texinfo].textureVecs[0][2], texInfo[face.texinfo].textureVecs[0][1]);
                currentFace.t = new Vector3(texInfo[face.texinfo].textureVecs[1][0], texInfo[face.texinfo].textureVecs[1][2], texInfo[face.texinfo].textureVecs[1][1]);
                currentFace.xOffset = texInfo[face.texinfo].textureVecs[0][3];
                currentFace.yOffset = texInfo[face.texinfo].textureVecs[1][3];

                currentFace.rawTexture = textureStringData.Substring(Math.Abs(texStringTable[Math.Abs(texData[Math.Abs(texInfo[Math.Abs(face.texinfo)].texdata)].nameStringTableID)]));
                currentFace.rawTexture = currentFace.rawTexture.Substring(0, currentFace.rawTexture.IndexOf(BSPParser.TEXTURE_STRING_DATA_SPLITTER));
                currentFace.rawTexture = RemoveMisleadingPath(currentFace.rawTexture);

                #region Desirabilities
                texflags textureFlag = texflags.SURF_NODRAW;
                try { textureFlag = ((texflags)texInfo[face.texinfo].flags); }
                catch (System.Exception) { }

                bool undesired = false;
                foreach (string undesiredTexture in undesiredTextures)
                {
                    if (currentFace.rawTexture.Equals(undesiredTexture, StringComparison.InvariantCultureIgnoreCase)) { undesired = true; break; }
                }
                #endregion

                if (!undesired && (textureFlag & texflags.SURF_SKY2D) != texflags.SURF_SKY2D && (textureFlag & texflags.SURF_SKY) != texflags.SURF_SKY && (textureFlag & texflags.SURF_NODRAW) != texflags.SURF_NODRAW && (textureFlag & texflags.SURF_SKIP) != texflags.SURF_SKIP) desiredFaceMeshes.Add(currentFace);
            }

            faceMeshes = desiredFaceMeshes.ToArray();
        }
    }
    private void LoadOverview()
    {
        try
        {
            overviewImage = new DDSImage(File.ReadAllBytes(ProjectMoretz.MainForm.overviewsPath + "\\" + mapName + "_radar.dds"));
        }
        catch (Exception e) { Console.WriteLine("Error loading overview: " + e.Message); }
    }

    private string RemoveMisleadingPath(string original)
    {
        string goodPath = original.Substring(0);
        if (goodPath.IndexOf("maps/") > -1)
        {
            goodPath = goodPath.Substring(goodPath.IndexOf("maps/") + ("maps/").Length);
            goodPath = goodPath.Substring(goodPath.IndexOf("/") + 1);
            while (goodPath.LastIndexOf("_") > -1 && (goodPath.Substring(goodPath.LastIndexOf("_") + 1).StartsWith("-") || char.IsDigit(goodPath.Substring(goodPath.LastIndexOf("_") + 1)[0])))
            {
                goodPath = goodPath.Substring(0, goodPath.LastIndexOf("_"));
            }
        }

        return goodPath.ToString();
    }
    private Mesh MakeFaceMesh(dface_t face)
    {
        Mesh mesh = null;

        //texflags textureFlag = texflags.SURF_NODRAW;
        //try { textureFlag = ((texflags)texInfo[face.texinfo].flags); }
        //catch (System.Exception) { }

        #region Get all vertices of face
        List<Vector3> surfaceVertices = new List<Vector3>();
        List<Vector3> originalVertices = new List<Vector3>();
        for (int i = 0; i < face.numedges; i++)
        {
            ushort[] currentEdge = edges[Math.Abs(surfedges[face.firstedge + i])].v;
            Vector3 point1 = vertices[currentEdge[0]], point2 = vertices[currentEdge[1]];
            point1 = new Vector3(point1.x, point1.z, point1.y);
            point2 = new Vector3(point2.x, point2.z, point2.y);

            if (surfedges[face.firstedge + i] >= 0)
            {
                if (surfaceVertices.IndexOf(point1) < 0) surfaceVertices.Add(point1);
                originalVertices.Add(point1);
                if (surfaceVertices.IndexOf(point2) < 0) surfaceVertices.Add(point2);
                originalVertices.Add(point2);
            }
            else
            {
                if (surfaceVertices.IndexOf(point2) < 0) surfaceVertices.Add(point2);
                originalVertices.Add(point2);
                if (surfaceVertices.IndexOf(point1) < 0) surfaceVertices.Add(point1);
                originalVertices.Add(point1);
            }
        }
        #endregion

        #region Triangulate
        List<int> triangleIndices = new List<int>();

        for (int i = 0; i < (originalVertices.Count / 2) - 0; i++)
        {
            int firstOrigIndex = (i * 2), secondOrigIndex = (i * 2) + 1, thirdOrigIndex = 0;
            int firstIndex = surfaceVertices.IndexOf(originalVertices[firstOrigIndex]);
            int secondIndex = surfaceVertices.IndexOf(originalVertices[secondOrigIndex]);
            int thirdIndex = surfaceVertices.IndexOf(originalVertices[thirdOrigIndex]);

            triangleIndices.Add(firstIndex);
            triangleIndices.Add(secondIndex);
            triangleIndices.Add(thirdIndex);
        }
        #endregion

        #region Get UV Points
        Vector3 s = Vector3.zero, t = Vector3.zero;
        float xOffset = 0, yOffset = 0;

        try
        {
            s = new Vector3(texInfo[face.texinfo].textureVecs[0][0], texInfo[face.texinfo].textureVecs[0][2], texInfo[face.texinfo].textureVecs[0][1]);
            t = new Vector3(texInfo[face.texinfo].textureVecs[1][0], texInfo[face.texinfo].textureVecs[1][2], texInfo[face.texinfo].textureVecs[1][1]);
            xOffset = texInfo[face.texinfo].textureVecs[0][3];
            yOffset = texInfo[face.texinfo].textureVecs[1][3];
        }
        catch (System.Exception) { }

        Vector2[] uvPoints = new Vector2[surfaceVertices.Count];
        int textureWidth = 0, textureHeight = 0;

        try { textureWidth = texData[texInfo[face.texinfo].texdata].width; textureHeight = texData[texInfo[face.texinfo].texdata].height; }
        catch (System.Exception) { }

        for (int i = 0; i < uvPoints.Length; i++)
        {
            uvPoints[i] = new Vector2((Vector3.Dot(surfaceVertices[i], s) + xOffset) / textureWidth, (textureHeight - (Vector3.Dot(surfaceVertices[i], t) + yOffset)) / textureHeight);
        }
        #endregion

        #region Make Mesh
        mesh = new Mesh();
        mesh.name = "Custom Mesh";
        mesh.vertices = surfaceVertices.ToArray();
        mesh.triangles = triangleIndices.ToArray();
        mesh.uv = uvPoints;
        #endregion

        return mesh;
    }

    public bool PlayerSight(Vector3 positionA, Vector3 positionB)
    {

        return false;
    }

    public override string ToString()
    {
        return mapName;
    }
}

public class FaceMesh
{
    public dface_t face;
    public Mesh mesh;
    public Vector3 s, t;
    public float xOffset, yOffset;
    public string rawTexture;
}
